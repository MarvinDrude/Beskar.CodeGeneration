using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;
using Beskar.CodeGeneration.Extensions.Transformers.Symbols;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Transformers.Archetypes;

public static class ParameterSymbolArchetypeTransformer
{
   public static ParameterSymbolArchetype Transform(IParameterSymbol parameterSymbol)
   {
      var symbolSpec = SymbolSpecTransformer.Transform(parameterSymbol);
      var parameterSpec = ParameterSymbolSpecTransformer.Transform(parameterSymbol);
      
      return new ParameterSymbolArchetype(symbolSpec, parameterSpec);
   }
}