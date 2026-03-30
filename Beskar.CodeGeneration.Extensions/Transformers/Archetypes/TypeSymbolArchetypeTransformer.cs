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
      var symbolSpec = SymbolSpecTransformer.Transform(typeSymbol, depth, options);
      var typeSpec = TypeSymbolSpecTransformer.Transform(typeSymbol, depth, options);

      if (typeSymbol is INamedTypeSymbol namedTypeSymbol)
      {
         var namedSpec = NamedTypeSymbolSpecTransformer.Transform(namedTypeSymbol, depth, options);
         return new TypeSymbolArchetype(symbolSpec, typeSpec, namedSpec);
      }
      
      return new TypeSymbolArchetype(symbolSpec, typeSpec, null);
   }
}