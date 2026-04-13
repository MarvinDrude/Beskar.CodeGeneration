using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.ContentGenerator;

public sealed partial class ContentGenerator
{
   private const string AttributeNameSpace = "Beskar.CodeGeneration.ContentGenerator.Marker.Attributes";
   
   private const string ContentTypeAttributeName = "ContentTypeAttribute";
   private const string ContentTypeAttributeFullName = $"{AttributeNameSpace}.{ContentTypeAttributeName}";
   
   private const string ComponentsOptionsAttributeName = "ComponentsOptionsAttribute";
   private const string ComponentsOptionsAttributeFullName = $"{AttributeNameSpace}.{ComponentsOptionsAttributeName}";
   
   private const string ComponentOptionsAttributeName = "ComponentOptionsAttribute";
   private const string ComponentOptionsAttributeFullName = $"{AttributeNameSpace}.{ComponentOptionsAttributeName}";

   private const string MediaOptionsAttributeName = "MediaOptionsAttribute";
   private const string MediaOptionsAttributeFullName = $"{AttributeNameSpace}.{MediaOptionsAttributeName}";
   
   private const string StringOptionsAttributeName = "StringOptionsAttribute";
   private const string StringOptionsAttributeFullName = $"{AttributeNameSpace}.{StringOptionsAttributeName}";
   
   private const string UniqueIdAttributeName = "UniqueIdAttribute";
   private const string UniqueIdAttributeFullName = $"{AttributeNameSpace}.{UniqueIdAttributeName}";

   private static AttributeData? GetContentTypeAttribute(ImmutableArray<AttributeData> attributes)
   {
      return attributes.FirstOrDefault(IsContentTypeAttribute);
   }
   
   private static bool IsContentTypeAttribute(AttributeData attribute)
   {
      return attribute.AttributeClass?.Name == ContentTypeAttributeName
         && IsRelevantAttribute(attribute);
   }
   
   private static bool IsComponentsOptionsAttribute(AttributeData attribute)
   {
      return attribute.AttributeClass?.Name == ComponentsOptionsAttributeName
         && IsRelevantAttribute(attribute);
   }
   
   private static bool IsComponentOptionsAttribute(AttributeData attribute)
   {
      return attribute.AttributeClass?.Name == ComponentOptionsAttributeName
         && IsRelevantAttribute(attribute);
   }
   
   private static bool IsMediaOptionsAttribute(AttributeData attribute)
   {
      return attribute.AttributeClass?.Name == MediaOptionsAttributeName
         && IsRelevantAttribute(attribute);
   }
   
   private static bool IsStringOptionsAttribute(AttributeData attribute)
   {
      return attribute.AttributeClass?.Name == StringOptionsAttributeName
         && IsRelevantAttribute(attribute);
   }
   
   private static bool IsUniqueIdAttribute(AttributeData attribute)
   {
      return attribute.AttributeClass?.Name == UniqueIdAttributeName
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
                  Name: "ContentGenerator",
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