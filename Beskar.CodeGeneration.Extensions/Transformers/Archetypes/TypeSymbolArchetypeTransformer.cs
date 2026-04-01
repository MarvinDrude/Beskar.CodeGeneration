using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes.Options;
using Beskar.CodeGeneration.Extensions.Transformers.Symbols;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Transformers.Archetypes;

public static class TypeSymbolArchetypeTransformer
{
   public static TypeSymbolArchetype Transform(
      ITypeSymbol typeSymbol,
      int depth = 1,
      ArchetypeTransformOptions? options = null)
   {
      options ??= new ArchetypeTransformOptions();
      
      if (options.TryGetCached(typeSymbol, out TypeSymbolArchetype cached))
      {
         return cached;
      }
      
      var symbolSpec = SymbolSpecTransformer.Transform(typeSymbol, depth, options);
      var typeSpec = TypeSymbolSpecTransformer.Transform(typeSymbol, depth, options);

      TypeSymbolArchetype archetype;
      
      if (typeSymbol is INamedTypeSymbol namedTypeSymbol)
      {
         var namedSpec = NamedTypeSymbolSpecTransformer.Transform(namedTypeSymbol, depth, options);
         archetype = new TypeSymbolArchetype(symbolSpec, typeSpec, namedSpec);
      }
      else
      {
         archetype = new TypeSymbolArchetype(symbolSpec, typeSpec, null);
      }
      
      options.AddToCache(typeSymbol, archetype);
      return archetype;
   }
}