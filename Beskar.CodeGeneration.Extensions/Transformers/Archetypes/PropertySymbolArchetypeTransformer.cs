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
      
      if (options.TryGetCached(propertySymbol, out PropertySymbolArchetype cached))
      {
         return cached;
      }
      
      var symbolSpec = SymbolSpecTransformer.Transform(propertySymbol, depth, options);
      var propertySpec = PropertySymbolSpecTransformer.Transform(propertySymbol, depth, options);
      
      var archetype = new PropertySymbolArchetype(symbolSpec, propertySpec);
      options.AddToCache(propertySymbol, archetype);
      
      return archetype;
   }
}