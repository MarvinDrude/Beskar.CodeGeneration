using Beskar.CodeGeneration.Extensions.Common.Symbols;
using Beskar.CodeGeneration.Extensions.Diagnostics;
using Beskar.CodeGeneration.Extensions.Models.Diagnostics;
using Beskar.CodeGeneration.Extensions.Models.Symbols;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes.Options;
using Beskar.CodeGeneration.Extensions.Transformers.Symbols.Options;
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
      var namedType = symbol.CreateNamedArchetype(CreateTransformOptions());
      
      
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

      if (!HasProcessorInterface(symbol))
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

   private static bool HasPostProcessorInterface(INamedTypeSymbol symbol)
   {
      return symbol.AllInterfaces.Any(
         i => i.Name is "IAsyncPostProcessor" or "ISyncPostProcessor"
            && IsInInterfaceNamespace(i));
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
            Depth = 4,
            Load = new TypeSymbolLoadFlags()
            {
               BaseType = true
            }
         }
      };
      
      options.RegisterAttribute($"global::{TimeoutAttributeFullName}", GetTimeoutAttribute);
      options.RegisterAttribute($"global::{ContextVariableAttributeFullName}", GetContextVariableAttribute);
      options.RegisterAttribute($"global::{SettingAttributeFullName}", GetSettingAttribute);
      options.RegisterAttribute($"global::{StepAttributeFullName}", GetStepAttribute);
      
      return options;
   }
}