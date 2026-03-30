using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes.Options;
using Beskar.CodeGeneration.Extensions.Transformers.Symbols;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Transformers.Archetypes;

public static class ParameterSymbolArchetypeTransformer
{
   public static ParameterSymbolArchetype Transform(
      IParameterSymbol parameterSymbol,
      int depth = 1,
      ArchetypeTransformOptions? options = null)
   {
      var symbolSpec = SymbolSpecTransformer.Transform(parameterSymbol, depth, options);
      var parameterSpec = ParameterSymbolSpecTransformer.Transform(parameterSymbol, depth, options);
      
      return new ParameterSymbolArchetype(symbolSpec, parameterSpec);
   }
}