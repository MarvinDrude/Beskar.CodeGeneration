using Beskar.CodeGeneration.Extensions.Common;
using Beskar.CodeGeneration.Extensions.Diagnostics;
using Beskar.CodeGeneration.Extensions.Models.Diagnostics;
using Beskar.CodeGeneration.Extensions.Models.Symbols;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes.Options;
using Beskar.CodeGeneration.LanguageGenerator.Models;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.LanguageGenerator;

public sealed partial class LanguageGenerator
{
   private static MaybeSpec<LanguageEnumSpec> Transform(
      GeneratorAttributeSyntaxContext context,
      CancellationToken ct)
   {
      ct.ThrowIfCancellationRequested();
      
      var symbol = (INamedTypeSymbol)context.TargetSymbol;
      var attributes = symbol.GetAttributes();

      if (GetTranslationGroupAttribute(attributes) is not { } attribute)
      {
         return DiagnosticBuilder<LanguageEnumSpec>.CreateEmpty();
      }
      
      ct.ThrowIfCancellationRequested();
      using var builder = DiagnosticBuilder<LanguageEnumSpec>.Create(8);

      
      
      return builder.Build(new LanguageEnumSpec(
         attribute.GroupName,
         attribute.EnumTypeArchetype));
   }

   private static LanguageKeySpec TransformKeyAttribute(ISymbol symbol, AttributeData data)
   {
      var keyName = data.DetermineStringValue("KeyName", 0, symbol.Name);
      var defaultValue = data.DetermineStringValue("DefaultValue", 1, symbol.Name);
      
      return new LanguageKeySpec()
      {
         Name = keyName,
         DefaultValue = defaultValue
      };
   }

   private static readonly ArchetypeTransformOptions _transformOptions = CreateTransformOptions();
   private static ArchetypeTransformOptions CreateTransformOptions()
   {
      var options = new ArchetypeTransformOptions()
      {
         NamedTypes =
         {
            Load = new NamedTypeSymbolLoadFlags()
            {
               Fields = true
            }
         },
         Fields =
         {
            Depth = 2,
            Load = new FieldSymbolLoadFlags()
            {
               Attributes = true
            }
         }
      };

      options.RegisterAttribute($"global::{TranslationKeyAttributeFullName}", TransformKeyAttribute);
      return options;
   }
}