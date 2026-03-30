using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;
using Beskar.CodeGeneration.Extensions.Transformers.Symbols;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Transformers.Archetypes;

public static class NamedTypeSymbolArchetypeTransformer
{
   public static NamedTypeSymbolArchetype Transform(INamedTypeSymbol namedTypeSymbol)
   {
      var symbolSpec = SymbolSpecTransformer.Transform(namedTypeSymbol);
      var typeSpec = TypeSymbolSpecTransformer.Transform(namedTypeSymbol);
      var namedSpec = NamedTypeSymbolSpecTransformer.Transform(namedTypeSymbol);
      
      return new NamedTypeSymbolArchetype(symbolSpec, typeSpec, namedSpec);
   }
}