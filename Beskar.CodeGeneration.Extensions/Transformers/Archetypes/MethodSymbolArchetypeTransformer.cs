using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;
using Beskar.CodeGeneration.Extensions.Transformers.Symbols;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Transformers.Archetypes;

public static class MethodSymbolArchetypeTransformer
{
   public static MethodSymbolArchetype Transform(IMethodSymbol methodSymbol)
   {
      var symbolSpec = SymbolSpecTransformer.Transform(methodSymbol);
      var methodSpec = MethodSymbolSpecTransformer.Transform(methodSymbol);
      
      return new MethodSymbolArchetype(symbolSpec, methodSpec);
   }
}