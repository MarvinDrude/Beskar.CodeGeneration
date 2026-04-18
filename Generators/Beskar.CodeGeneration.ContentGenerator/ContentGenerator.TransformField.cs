using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Beskar.CodeGeneration.ContentGenerator.Models;
using Beskar.CodeGeneration.ContentGenerator.Models.Attributes;
using Beskar.CodeGeneration.ContentGenerator.Models.Fields;
using Beskar.CodeGeneration.Extensions.Common;
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
         { Name: BooleanFieldName } => CreateBoolean(isLocalized, type, fieldSymbol, attributes),
         { Name: DateOnlyFieldName } => CreateDateOnly(isLocalized, type, fieldSymbol, attributes),
         { Name: DateTimeFieldName } => CreateDateTime(isLocalized, type, fieldSymbol, attributes),
         { Name: MediaFieldName } => CreateMedia(isLocalized, type, fieldSymbol, attributes),
         { Name: NumberFieldName } => CreateNumber(isLocalized, type, fieldSymbol, attributes),
         { Name: StringFieldName } => CreateString(isLocalized, type, fieldSymbol, attributes),
         { Name: TimeOnlyFieldName } => CreateTimeOnly(isLocalized, type, fieldSymbol, attributes),
         { Name: "ComponentCollection" } => CreateComponentCollection(isLocalized, type, fieldSymbol, attributes),
         { Name: "ComponentReference" } => CreateComponentReference(isLocalized, type, fieldSymbol, attributes),
         { Name: "ContentTypeId" } => new IdFieldSpec()
         {
            IsRequired = fieldSymbol.IsRequired,
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
         IsRequired = property.IsRequired,
         PropertyName = property.Name
      };
   }
   
   private static DateOnlyFieldSpec CreateDateOnly(bool isLocalized, INamedTypeSymbol type, IPropertySymbol property,
      ImmutableArray<AttributeData> attributes = default)
   {
      return new DateOnlyFieldSpec()
      {
         IsLocalized = isLocalized,
         IsRequired = property.IsRequired,
         PropertyName = property.Name
      };
   }
   
   private static DateTimeFieldSpec CreateDateTime(bool isLocalized, INamedTypeSymbol type, IPropertySymbol property, 
      ImmutableArray<AttributeData> attributes = default)
   {
      return new DateTimeFieldSpec()
      {
         IsLocalized = isLocalized,
         IsRequired = property.IsRequired,
         PropertyName = property.Name
      };
   }
   
   private static MediaFieldSpec CreateMedia(bool isLocalized, INamedTypeSymbol type, IPropertySymbol property, 
      ImmutableArray<AttributeData> attributes = default)
   {
      var attribute = attributes.FirstOrDefault(IsMediaOptionsAttribute);
      MediaOptionsSpec? options = null;
      if (attribute is not null)
      {
         options = new MediaOptionsSpec()
         {
            AllowedExtensions = attribute.DetermineStringArrayValues("AllowedExtensions", 0),
            MinCount = attribute.DetermineIntValue("MinCount", 1),
            MaxCount = attribute.DetermineIntValue("MaxCount", 2),
         };
      }
      
      return new MediaFieldSpec()
      {
         IsLocalized = isLocalized,
         IsRequired = property.IsRequired,
         PropertyName = property.Name,
         Options = options
      };
   }
   
   private static NumberFieldSpec CreateNumber(bool isLocalized, INamedTypeSymbol type, IPropertySymbol property, 
      ImmutableArray<AttributeData> attributes = default)
   {
      var numberType = type.TypeArguments[0].ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);
      
      return new NumberFieldSpec()
      {
         IsLocalized = isLocalized,
         IsRequired = property.IsRequired,
         PropertyName = property.Name,
         NumberTypeName = numberType
      };
   }
   
   private static StringFieldSpec CreateString(bool isLocalized, INamedTypeSymbol type, IPropertySymbol property, 
      ImmutableArray<AttributeData> attributes = default)
   {
      var attribute = attributes.FirstOrDefault(IsStringOptionsAttribute);
      StringOptionsSpec? options = null;
      if (attribute is not null)
      {
         options = new StringOptionsSpec()
         {
            Kind = StringOptionsSpec.GetKind(attribute.DetermineEnumFullName("Kind", 0) ?? string.Empty),
            MaxLength = attribute.DetermineIntValue("MaxLength", 1),
         };
      }
      
      return new StringFieldSpec()
      {
         IsLocalized = isLocalized,
         IsRequired = property.IsRequired,
         PropertyName = property.Name,
         Options = options
      };
   }
   
   private static TimeOnlyFieldSpec CreateTimeOnly(bool isLocalized, INamedTypeSymbol type, IPropertySymbol property, 
      ImmutableArray<AttributeData> attributes = default)
   {
      return new TimeOnlyFieldSpec()
      {
         IsLocalized = isLocalized,
         IsRequired = property.IsRequired,
         PropertyName = property.Name
      };
   }
   
   private static ComponentCollectionSpec CreateComponentCollection(bool isLocalized, INamedTypeSymbol type, IPropertySymbol property, 
      ImmutableArray<AttributeData> attributes = default)
   {
      var attribute = attributes.FirstOrDefault(IsComponentsOptionsAttribute);
      ComponentsOptionsSpec? options = null;
      if (attribute is not null)
      {
         options = new ComponentsOptionsSpec();
      }
      
      return new ComponentCollectionSpec()
      {
         IsLocalized = isLocalized,
         IsRequired = property.IsRequired,
         PropertyName = property.Name,
         Options = options,
         FullName = type.TypeArguments[0].ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)
      };
   }
   
   private static ComponentReferenceSpec CreateComponentReference(bool isLocalized, INamedTypeSymbol type, IPropertySymbol property, 
      ImmutableArray<AttributeData> attributes = default)
   {
      var attribute = attributes.FirstOrDefault(IsComponentOptionsAttribute);
      ComponentOptionsSpec? options = null;
      if (attribute is not null)
      {
         options = new ComponentOptionsSpec();
      }
      
      return new ComponentReferenceSpec()
      {
         IsLocalized = isLocalized,
         IsRequired = property.IsRequired,
         PropertyName = property.Name,
         Options = options,
         FullName = type.TypeArguments[0].ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)
      };
   }
}