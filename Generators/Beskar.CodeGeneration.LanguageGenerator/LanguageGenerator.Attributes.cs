using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.LanguageGenerator;

public sealed partial class LanguageGenerator
{
   private const string AttributeNameSpace = "Beskar.CodeGeneration.LanguageGenerator.Marker.Attributes";

   private const string TranslationKeyAttributeName = "TranslationKeyAttribute";
   private const string TranslationKeyAttributeFullName = $"{AttributeNameSpace}.{TranslationKeyAttributeName}";
   
   private const string TranslationGroupAttributeName = "TranslationGroupAttribute";
   private const string TranslationGroupAttributeFullName = $"{AttributeNameSpace}.{TranslationGroupAttributeName}";

   private static AttributeData? GetTranslationKeyAttribute(ImmutableArray<AttributeData> attributes)
   {
      return attributes.FirstOrDefault(IsTranslationKeyAttribute);
   }
   
   private static AttributeData? GetTranslationGroupAttribute(ImmutableArray<AttributeData> attributes)
   {
      return attributes.FirstOrDefault(IsTranslationGroupAttribute);
   }
   
   private static bool IsTranslationKeyAttribute(AttributeData attribute)
   {
      return attribute.AttributeClass?.Name == TranslationKeyAttributeName 
         && IsRelevantAttribute(attribute);
   }
   
   private static bool IsTranslationGroupAttribute(AttributeData attribute)
   {
      return attribute.AttributeClass?.Name == TranslationGroupAttributeName 
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
                  Name: "LanguageGenerator",
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