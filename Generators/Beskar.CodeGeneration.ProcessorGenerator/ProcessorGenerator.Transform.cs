using Beskar.CodeGeneration.Extensions.Common;
using Beskar.CodeGeneration.Extensions.Common.Symbols;
using Beskar.CodeGeneration.Extensions.Diagnostics;
using Beskar.CodeGeneration.Extensions.Models.Diagnostics;
using Beskar.CodeGeneration.Extensions.Models.Symbols;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes.Options;
using Beskar.CodeGeneration.Extensions.Transformers.Symbols.Options;
using Beskar.CodeGeneration.ProcessorGenerator.Enums;
using Beskar.CodeGeneration.ProcessorGenerator.Models;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.ProcessorGenerator;

public sealed partial class ProcessorGenerator
{
   private static MaybeSpec<ProcessorPipelineSpec> Transform(
      GeneratorAttributeSyntaxContext context,
      CancellationToken ct)
   {
      ct.ThrowIfCancellationRequested();

      var symbol = (INamedTypeSymbol)context.TargetSymbol;
      var attributes = symbol.GetAttributes();
      
      if (GetProcessorPipelineAttribute(attributes) is not { } attribute)
      {
         return DiagnosticBuilder<ProcessorPipelineSpec>.CreateEmpty();
      }
      
      ct.ThrowIfCancellationRequested();
      using var builder = DiagnosticBuilder<ProcessorPipelineSpec>.Create(8);

      var pipelineName = attribute.DetermineStringValue("Name", 0) ?? symbol.Name;
      var pBuilder = new ProcessorPipelineBuilder()
      {
         PipelineName = pipelineName
      };

      TransformPipeline(symbol, pBuilder);
      if (pBuilder.ProcessorRegisters.Count == 0
          || symbol.IsAbstract)
      {
         return builder.Add(InvalidPipelineTargetDiagnosticId).Build();
      }
      
      return builder.Build(pBuilder.Build(symbol));
   }

   private static void TransformPipeline(INamedTypeSymbol symbol, ProcessorPipelineBuilder builder)
   {
      while (true)
      {
         var attributes = symbol.GetAttributes();
         if (builder.TimeoutSpec is null)
         {
            var timeoutAttr = GetTimeoutAttribute(symbol, attributes);
            if (timeoutAttr is not null)
            {
               builder.TimeoutSpec = timeoutAttr;
            }
         }
         
         var variables = GetContextVariableAttributes(symbol, attributes);
         builder.ContextVariables.AddRange(variables);

         foreach (var property in symbol.GetMembers().OfType<IPropertySymbol>()
            .Where(p => p is { IsStatic: false, IsReadOnly: false }))
         {
            var propAttributes = property.GetAttributes();
            if (GetStepAttribute(property, propAttributes) is not { } step
                || property.Type is not INamedTypeSymbol stepType)
            {
               continue;
            }

            if (GetProcessorKind(stepType) is not { } kind)
            {
               continue;
            }

            var settings = GetSettingAttributes(stepType, propAttributes);
            var (input, output) = GetInputOutputTypes(stepType, kind);
            
            builder.ProcessorRegisters.Add(new ProcessorRegisterSpec(
               stepType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
               property.Name,
               input, output,
               kind,
               GetProcessorPostKind(stepType),
               step,
               [.. settings]));
         }
         
         if (symbol.BaseType is not null && symbol.BaseType.SpecialType is not SpecialType.System_Object)
         {
            symbol = symbol.BaseType;
            continue;
         }

         break;
      }
   }

   private static MaybeSpec<ProcessorSpec> TransformProcessor(
      GeneratorAttributeSyntaxContext context,
      CancellationToken ct)
   {
      ct.ThrowIfCancellationRequested();

      var symbol = (INamedTypeSymbol)context.TargetSymbol;
      var attributes = symbol.GetAttributes();
      
      if (GetProcessorAttribute(attributes) is not { } attribute)
      {
         return DiagnosticBuilder<ProcessorSpec>.CreateEmpty();
      }
      
      ct.ThrowIfCancellationRequested();
      
      using var builder = DiagnosticBuilder<ProcessorSpec>.Create(8);
      var namedType = symbol.CreateNamedArchetype();

      if (!HasProcessorInterface(symbol)
          || symbol.IsAbstract)
      {
         return builder.Add(InvalidTargetDiagnosticId).Build();
      }
      
      ct.ThrowIfCancellationRequested();
      return builder.Build(new ProcessorSpec(namedType));
   }

   private static bool HasProcessorInterface(INamedTypeSymbol symbol)
   {
      return symbol.AllInterfaces.Any(
         i => i.Name is "IAsyncProcessor" or "ISyncProcessor" or "IValueAsyncProcessor"
            && IsInInterfaceNamespace(i));
   }

   private static (string InputFullTypeName, string OutputFullTypeName) GetInputOutputTypes(
      INamedTypeSymbol symbol, ProcessorKind kind)
   {
      var interfaceName = kind switch
      {
         ProcessorKind.Async => "IAsyncProcessor",
         ProcessorKind.Sync => "ISyncProcessor",
         ProcessorKind.ValueAsync => "IValueAsyncProcessor",
         _ => throw new InvalidOperationException()
      };
      var interfaceType = symbol.AllInterfaces.First(i => i.Name == interfaceName);
      
      return (interfaceType.TypeArguments[0].ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
         interfaceType.TypeArguments[1].ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));
   }

   private static ProcessorKind? GetProcessorKind(INamedTypeSymbol symbol)
   {
      (bool isAsync, bool isSync, bool isValueAsync) flags = (false, false, false);

      foreach (var i in symbol.AllInterfaces)
      {
         if (i.Name is "IAsyncProcessor") flags.isAsync = true;
         if (i.Name is "ISyncProcessor") flags.isSync = true;
         if (i.Name is "IValueAsyncProcessor") flags.isValueAsync = true;
      }

      return flags switch
      {
         { isValueAsync: true } => ProcessorKind.ValueAsync,
         { isAsync: true } => ProcessorKind.Async,
         { isSync: true } => ProcessorKind.Sync,
         _ => null
      };
   }
   
   private static ProcessorKind? GetProcessorPostKind(INamedTypeSymbol symbol)
   {
      (bool isAsync, bool isSync, bool isValueAsync) flags = (false, false, false);

      foreach (var i in symbol.AllInterfaces)
      {
         if (i.Name is "IAsyncPostProcessor") flags.isAsync = true;
         if (i.Name is "ISyncPostProcessor") flags.isSync = true;
         if (i.Name is "IValueAsyncPostProcessor") flags.isValueAsync = true;
      }

      return flags switch
      {
         { isValueAsync: true } => ProcessorKind.ValueAsync,
         { isAsync: true } => ProcessorKind.Async,
         { isSync: true } => ProcessorKind.Sync,
         _ => null
      };
   }
   
   private static ArchetypeTransformOptions CreateTransformOptions()
   {
      var options = new ArchetypeTransformOptions()
      {
         NamedTypes = new NamedTypeTransformOptions()
         {
            Depth = 4,
            Load = new NamedTypeSymbolLoadFlags()
            {
               Attributes = true
            }
         },
         Types = new TypeTransformOptions()
         {
            Depth = 6,
            Load = new TypeSymbolLoadFlags()
            {
               BaseType = true,
               AllInterfaces = true,
            }
         }
      };
      
      options.RegisterAttribute($"global::{TimeoutAttributeFullName}", GetTimeoutAttribute);
      options.RegisterAttribute($"global::{ContextVariableAttributeFullName}", GetContextVariableAttributes);
      options.RegisterAttribute($"global::{StepAttributeFullName}", GetStepAttribute);
      
      return options;
   }
}