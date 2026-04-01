using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes.Options;
using Beskar.CodeGeneration.Extensions.Transformers.Symbols;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Transformers.Archetypes;

public static class MethodSymbolArchetypeTransformer
{
   public static MethodSymbolArchetype Transform(
      IMethodSymbol methodSymbol,
      int depth = 1,
      ArchetypeTransformOptions? options = null)
   {
      options ??= new ArchetypeTransformOptions();
      
      if (options.TryGetCached(methodSymbol, out MethodSymbolArchetype cached))
      {
         return cached;
      }
      
      var symbolSpec = SymbolSpecTransformer.Transform(methodSymbol, depth, options);
      var methodSpec = MethodSymbolSpecTransformer.Transform(methodSymbol, depth, options);
      
      var archetype = new MethodSymbolArchetype(symbolSpec, methodSpec);
      options.AddToCache(methodSymbol, archetype);
      
      return archetype;
   }
}