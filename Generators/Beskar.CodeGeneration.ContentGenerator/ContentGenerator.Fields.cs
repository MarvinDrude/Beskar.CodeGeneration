using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.ContentGenerator;

public sealed partial class ContentGenerator
{
   private const string FieldNameSpace = "Beskar.CodeGeneration.ContentGenerator.Marker.Fields";
   
   private const string BooleanFieldName = "BooleanField";
   private const string BooleanFieldFullName = $"{FieldNameSpace}.{BooleanFieldName}";
   
   private const string DateOnlyFieldName = "DateOnlyField";
   private const string DateOnlyFieldFullName = $"{FieldNameSpace}.{DateOnlyFieldName}";
   
   private const string DateTimeFieldName = "DateTimeField";
   private const string DateTimeFieldFullName = $"{FieldNameSpace}.{DateTimeFieldName}";
   
   private const string LocalizedFieldName = "LocalizedField";
   private const string LocalizedFieldFullName = $"{FieldNameSpace}.{LocalizedFieldName}";
   
   private const string MediaFieldName = "MediaField";
   private const string MediaFieldFullName = $"{FieldNameSpace}.{MediaFieldName}";
   
   private const string NumberFieldName = "NumberField";
   private const string NumberFieldFullName = $"{FieldNameSpace}.{NumberFieldName}";
   
   private const string StringFieldName = "StringField";
   private const string StringFieldFullName = $"{FieldNameSpace}.{StringFieldName}";
   
   private const string TimeOnlyFieldName = "TimeOnlyField";
   private const string TimeOnlyFieldFullName = $"{FieldNameSpace}.{TimeOnlyFieldName}";
   
   private static bool IsRelevantField(INamedTypeSymbol symbol)
   {
      return symbol is
      {
         ContainingNamespace:
         {
            Name: "Fields",
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