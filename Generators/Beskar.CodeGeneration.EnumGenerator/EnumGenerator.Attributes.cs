using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.EnumGenerator;

public sealed partial class EnumGenerator
{
   private const string AttributeNameSpace = "Beskar.CodeGeneration.EnumGenerator.Marker.Attributes";
   
   private const string FastEnumAttributeName = "FastEnumAttribute";
   private const string FastEnumAttributeFullName = $"{AttributeNameSpace}.{FastEnumAttributeName}";

   private static AttributeData? GetFastEnumAttribute(ImmutableArray<AttributeData> attributes)
   {
      return attributes.FirstOrDefault(IsFastEnumAttribute);
   }
   
   private static bool IsFastEnumAttribute(AttributeData attribute)
   {
      return attribute.AttributeClass?.Name == FastEnumAttributeName 
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
                  Name: "EnumGenerator",
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