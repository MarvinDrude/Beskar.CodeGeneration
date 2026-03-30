using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;
using Beskar.CodeGeneration.Extensions.Transformers.Symbols;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Transformers.Archetypes;

public static class TypeSymbolArchetypeTransformer
{
   public static TypeSymbolArchetype Transform(ITypeSymbol typeSymbol)
   {
      var symbolSpec = SymbolSpecTransformer.Transform(typeSymbol);
      var typeSpec = TypeSymbolSpecTransformer.Transform(typeSymbol);

      if (typeSymbol is INamedTypeSymbol namedTypeSymbol)
      {
         var namedSpec = NamedTypeSymbolSpecTransformer.Transform(namedTypeSymbol);
         return new TypeSymbolArchetype(symbolSpec, typeSpec, namedSpec);
      }
      
      return new TypeSymbolArchetype(symbolSpec, typeSpec, null);
   }
}