using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Beskar.CodeGeneration.ContentGenerator.Models;
using Beskar.CodeGeneration.ContentGenerator.Models.Fields;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.ContentGenerator;

public sealed partial class ContentGenerator
{
   private static FieldSpec? TransformField(IPropertySymbol fieldSymbol)
   {
      if (fieldSymbol.Type is not INamedTypeSymbol type)
      {
         return null;
      }
      
      var isLocalized = false;
      if (IsLocalizedField(fieldSymbol, out var innerSymbol))
      {
         isLocalized = true;
         type = innerSymbol;
      }

      if (!IsRelevantField(type))
      {
         return null;
      }
      
      var attributes = fieldSymbol.GetAttributes();
      FieldSpec? field = type switch
      {
         { Name: "BooleanField" } => CreateBoolean(isLocalized, type, fieldSymbol, attributes),
         { Name: "DateOnlyField" } => CreateDateOnly(isLocalized, type, fieldSymbol, attributes),
         { Name: "DateTimeField" } => CreateDateTime(isLocalized, type, fieldSymbol, attributes),
         { Name: "MediaField" } => CreateMedia(isLocalized, type, fieldSymbol, attributes),
         { Name: "NumberField" } => CreateNumber(isLocalized, type, fieldSymbol, attributes),
         { Name: "StringField" } => CreateString(isLocalized, type, fieldSymbol, attributes),
         { Name: "TimeOnlyField" } => CreateTimeOnly(isLocalized, type, fieldSymbol, attributes),
         { Name: "ComponentCollection" } => CreateComponentCollection(isLocalized, type, fieldSymbol, attributes),
         { Name: "ComponentReference" } => CreateComponentReference(isLocalized, type, fieldSymbol, attributes),
         { Name: "ContentTypeId" } => new IdFieldSpec()
         {
            IsLocalized = isLocalized,
            PropertyName = fieldSymbol.Name
         },
         _ => null
      };

      return field;
   }
   
   private static bool IsLocalizedField(IPropertySymbol fieldSymbol, [MaybeNullWhen(false)] out INamedTypeSymbol innerSymbol)
   {
      if (fieldSymbol.Type is not INamedTypeSymbol namedTypeSymbol)
      {
         innerSymbol = null;
         return false;
      }

      if (IsRelevantField(namedTypeSymbol) && namedTypeSymbol is 
             { Name: "LocalizedField", TypeArguments: [INamedTypeSymbol symbol] })
      {
         innerSymbol = symbol;
         return true;
      }

      innerSymbol = null;
      return false;
   }

   private static BooleanFieldSpec CreateBoolean(bool isLocalized, INamedTypeSymbol type, IPropertySymbol property,
      ImmutableArray<AttributeData> attributes = default)
   {
      return new BooleanFieldSpec()
      {
         IsLocalized = isLocalized,
         PropertyName = property.Name
      };
   }
   
   private static DateOnlyFieldSpec CreateDateOnly(bool isLocalized, INamedTypeSymbol type, IPropertySymbol property,
      ImmutableArray<AttributeData> attributes = default)
   {
      return new DateOnlyFieldSpec()
      {
         IsLocalized = isLocalized,
         PropertyName = property.Name
      };
   }
   
   private static DateTimeFieldSpec CreateDateTime(bool isLocalized, INamedTypeSymbol type, IPropertySymbol property, 
      ImmutableArray<AttributeData> attributes = default)
   {
      return new DateTimeFieldSpec()
      {
         IsLocalized = isLocalized,
         PropertyName = property.Name
      };
   }
   
   private static MediaFieldSpec CreateMedia(bool isLocalized, INamedTypeSymbol type, IPropertySymbol property, 
      ImmutableArray<AttributeData> attributes = default)
   {
      return new MediaFieldSpec()
      {
         IsLocalized = isLocalized,
         PropertyName = property.Name
      };
   }
   
   private static NumberFieldSpec CreateNumber(bool isLocalized, INamedTypeSymbol type, IPropertySymbol property, 
      ImmutableArray<AttributeData> attributes = default)
   {
      return new NumberFieldSpec()
      {
         IsLocalized = isLocalized,
         PropertyName = property.Name
      };
   }
   
   private static StringFieldSpec CreateString(bool isLocalized, INamedTypeSymbol type, IPropertySymbol property, 
      ImmutableArray<AttributeData> attributes = default)
   {
      return new StringFieldSpec()
      {
         IsLocalized = isLocalized,
         PropertyName = property.Name
      };
   }
   
   private static TimeOnlyFieldSpec CreateTimeOnly(bool isLocalized, INamedTypeSymbol type, IPropertySymbol property, 
      ImmutableArray<AttributeData> attributes = default)
   {
      return new TimeOnlyFieldSpec()
      {
         IsLocalized = isLocalized,
         PropertyName = property.Name
      };
   }
   
   private static ComponentCollectionSpec CreateComponentCollection(bool isLocalized, INamedTypeSymbol type, IPropertySymbol property, 
      ImmutableArray<AttributeData> attributes = default)
   {
      var attribute = attributes.FirstOrDefault(IsComponentsOptionsAttribute);
      
      return new ComponentCollectionSpec()
      {
         IsLocalized = isLocalized,
         PropertyName = property.Name
      };
   }
   
   private static ComponentReferenceSpec CreateComponentReference(bool isLocalized, INamedTypeSymbol type, IPropertySymbol property, 
      ImmutableArray<AttributeData> attributes = default)
   {
      var attribute = attributes.FirstOrDefault(IsComponentOptionsAttribute);
      
      
      return new ComponentReferenceSpec()
      {
         IsLocalized = isLocalized,
         PropertyName = property.Name
      };
   }
}