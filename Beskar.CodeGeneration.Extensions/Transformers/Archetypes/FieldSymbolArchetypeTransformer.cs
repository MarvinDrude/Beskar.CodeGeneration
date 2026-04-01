using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes.Options;
using Beskar.CodeGeneration.Extensions.Transformers.Symbols;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Transformers.Archetypes;

public static class FieldSymbolArchetypeTransformer
{
   public static FieldSymbolArchetype Transform(
      IFieldSymbol fieldSymbol,
      int depth = 1,
      ArchetypeTransformOptions? options = null)
   {
      options ??= new ArchetypeTransformOptions();

      if (options.TryGetCached(fieldSymbol, out FieldSymbolArchetype cached))
      {
         return cached;
      }
      
      var symbolSpec = SymbolSpecTransformer.Transform(fieldSymbol, depth, options);
      var fieldSpec = FieldSymbolSpecTransformer.Transform(fieldSymbol, depth, options);
      
      var archetype = new FieldSymbolArchetype(symbolSpec, fieldSpec);
      options.AddToCache(fieldSymbol, archetype);
      
      return archetype;
   }
}