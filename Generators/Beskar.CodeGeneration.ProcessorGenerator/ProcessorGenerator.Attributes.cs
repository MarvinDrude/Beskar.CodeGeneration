using System.Collections.Immutable;
using Beskar.CodeGeneration.Extensions.Common;
using Beskar.CodeGeneration.ProcessorGenerator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Beskar.CodeGeneration.ProcessorGenerator;

public sealed partial class ProcessorGenerator
{
   private const string AttributeNameSpace = "Beskar.CodeGeneration.ProcessorGenerator.Marker.Attributes";
   
   private const string ProcessorAttributeName = "ProcessorAttribute";
   private const string ProcessorAttributeFullName = $"{AttributeNameSpace}.{ProcessorAttributeName}";
   
   private const string ProcessorPipelineAttributeName = "ProcessorPipelineAttribute";
   private const string ProcessorPipelineAttributeFullName = $"{AttributeNameSpace}.{ProcessorPipelineAttributeName}";
   
   private const string TimeoutAttributeName = "TimeoutAttribute";
   private const string TimeoutAttributeFullName = $"{AttributeNameSpace}.{TimeoutAttributeName}";
   
   private const string ContextVariableAttributeName = "ContextVariableAttribute";
   private const string ContextVariableAttributeFullName = $"{AttributeNameSpace}.{ContextVariableAttributeName}";
   
   private const string SettingAttributeName = "SettingAttribute";
   private const string SettingAttributeFullName = $"{AttributeNameSpace}.{SettingAttributeName}";
   
   private const string StepAttributeName = "StepAttribute";
   private const string StepAttributeFullName = $"{AttributeNameSpace}.{StepAttributeName}";

   private static TimeoutSpec? GetTimeoutAttribute(ISymbol symbol, ImmutableArray<AttributeData> attributes)
   {
      return attributes.Where(IsTimeoutAttribute)
         .Select(x => GetTimeoutAttribute(symbol, x))
         .FirstOrDefault();
   }
   
   private static IEnumerable<ContextVariableSpec> GetContextVariableAttributes(ISymbol symbol, ImmutableArray<AttributeData> attributes)
   {
      return attributes.Where(IsContextVariableAttribute)
         .Select(x => GetContextVariableAttributes(symbol, x));
   }

   private static StepSpec? GetStepAttribute(ISymbol symbol, ImmutableArray<AttributeData> attributes)
   {
      return attributes.Where(IsStepAttribute)
         .Select(x => GetStepAttribute(symbol, x))
         .FirstOrDefault();
   }
   
   private static List<SettingSpec> GetSettingAttributes(INamedTypeSymbol symbol, ImmutableArray<AttributeData> attributes)
   {
      Dictionary<string, string> found = [];
      var properties = symbol.GetMembers().OfType<IPropertySymbol>()
         .Where(p => p is { IsStatic: false, IsReadOnly: false });

      foreach (var property in properties)
      {
         var propAttributes = property.GetAttributes();
         var setting = propAttributes.Where(IsSettingAttribute)
            .Select(x => GetSettingAttributes(symbol, x, found))
            .FirstOrDefault();
         
         found[setting?.Name ?? string.Empty] = property.Name;
      }
      
      return attributes.Where(IsSettingAttribute)
         .Select(x => GetSettingAttributes(symbol, x, found))
         .Where(x => found.ContainsKey(x.Name))
         .ToList();
   }
   
   private static SettingSpec GetSettingAttributes(ISymbol symbol, AttributeData attribute, Dictionary<string, string> found)
   {
      var name = attribute.DetermineStringValue("Name", 0) ?? "Unknown";
      
      return new SettingSpec()
      {
         Name = name,
         ValueFullExpression = attribute.GetCSharpString(1) ?? "default",
         PropertyName = found.GetValueOrDefault(name) ?? "Unknown"
      };
   }
   
   private static StepSpec GetStepAttribute(ISymbol symbol, AttributeData attribute)
   {
      return new StepSpec()
      {
         Order = attribute.DetermineIntValue("Order", 0)
      };
   }
   
   private static TimeoutSpec GetTimeoutAttribute(ISymbol symbol, AttributeData attribute)
   {
      return new TimeoutSpec()
      {
         Milliseconds = attribute.DetermineIntValue("Milliseconds", 0)
      };
   }
   
   private static ContextVariableSpec GetContextVariableAttributes(ISymbol symbol, AttributeData attribute)
   {
      var attributeClass = attribute.AttributeClass;
      var typeArgument = attributeClass?.TypeArguments.FirstOrDefault();
      
      return new ContextVariableSpec()
      {
         Name = attribute.DetermineStringValue("Name", 0) ?? "Unknown",
         TypeFullName = typeArgument?.ToDisplayString() ?? "object",
         IsReferenceType = typeArgument?.IsReferenceType ?? true
      };
   }
   
   private static AttributeData? GetProcessorPipelineAttribute(ImmutableArray<AttributeData> attributes)
   {
      return attributes.FirstOrDefault(IsProcessorPipelineAttribute);
   }
   
   private static AttributeData? GetProcessorAttribute(ImmutableArray<AttributeData> attributes)
   {
      return attributes.FirstOrDefault(IsProcessorAttribute);
   }
   
   private static bool IsStepAttribute(AttributeData attribute)
   {
      return attribute.AttributeClass?.Name == StepAttributeName 
         && IsRelevantAttribute(attribute);
   }
   
   private static bool IsSettingAttribute(AttributeData attribute)
   {
      return attribute.AttributeClass?.Name == SettingAttributeName 
         && IsRelevantAttribute(attribute);
   }
   
   private static bool IsContextVariableAttribute(AttributeData attribute)
   {
      return attribute.AttributeClass?.Name == ContextVariableAttributeName 
         && IsRelevantAttribute(attribute);
   }
   
   private static bool IsTimeoutAttribute(AttributeData attribute)
   {
      return attribute.AttributeClass?.Name == TimeoutAttributeName 
         && IsRelevantAttribute(attribute);
   }
   
   private static bool IsProcessorPipelineAttribute(AttributeData attribute)
   {
      return attribute.AttributeClass?.Name == ProcessorPipelineAttributeName 
         && IsRelevantAttribute(attribute);
   }
   
   private static bool IsProcessorAttribute(AttributeData attribute)
   {
      return attribute.AttributeClass?.Name == ProcessorAttributeName 
         && IsRelevantAttribute(attribute);
   }
   
   private static bool IsRelevantAttribute(AttributeData attribute)
   {
      return attribute.AttributeClass is
      {
         ContainingNamespace:
         {
            Name: "Attributes",
            ContainingNamespace:
            {
               Name: "Marker",
               ContainingNamespace:
               {
                  Name: "ProcessorGenerator",
                  ContainingNamespace:
                  {
                     Name: "CodeGeneration",
                     ContainingNamespace:
                     {
                        Name: "Beskar",
                        ContainingNamespace.IsGlobalNamespace: true
                     }
                  }
               }
            }
         },
      };
   }
   
   private static bool IsInInterfaceNamespace(INamedTypeSymbol symbol)
   {
      return symbol is
      {
         ContainingNamespace:
         {
            Name: "Interfaces",
            ContainingNamespace:
            {
               Name: "Marker",
               ContainingNamespace:
               {
                  Name: "ProcessorGenerator",
                  ContainingNamespace:
                  {
                     Name: "CodeGeneration",
                     ContainingNamespace:
                     {
                        Name: "Beskar",
                        ContainingNamespace.IsGlobalNamespace: true
                     }
                  }
               }
            }
         },
      };
   }
}