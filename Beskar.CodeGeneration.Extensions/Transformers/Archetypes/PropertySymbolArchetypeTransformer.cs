using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes.Options;
using Beskar.CodeGeneration.Extensions.Transformers.Symbols;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Transformers.Archetypes;

public static class PropertySymbolArchetypeTransformer
{
   public static PropertySymbolArchetype Transform(
      IPropertySymbol propertySymbol,
      int depth = 1,
      ArchetypeTransformOptions? options = null)
   {
      options ??= new ArchetypeTransformOptions();
      
      var symbolSpec = SymbolSpecTransformer.Transform(propertySymbol, depth, options);
      var propertySpec = PropertySymbolSpecTransformer.Transform(propertySymbol, depth, options);
      
      return new PropertySymbolArchetype(symbolSpec, propertySpec);
   }
}