using Beskar.CodeGeneration.Extensions.Models.Symbols;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes.Options;
using Beskar.CodeGeneration.Extensions.Transformers.Symbols.Options;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Transformers.Symbols;

public static class ParameterSymbolSpecTransformer
{
   public static ParameterSymbolSpec Transform(
      IParameterSymbol parameterSymbol,
      int depth = 1,
      ArchetypeTransformOptions? options = null)
   {
      return new ParameterSymbolSpec()
      {
         Ordinal = parameterSymbol.Ordinal,
         ScopeKind = parameterSymbol.ScopedKind,
         RefKind = parameterSymbol.RefKind,
         
         HasExplicitDefaultValue = parameterSymbol.HasExplicitDefaultValue,
         IsParamsArray = parameterSymbol.IsParamsArray,
         IsParamsCollection = parameterSymbol.IsParamsCollection,
         IsDiscard = parameterSymbol.IsDiscard,
         IsOptional = parameterSymbol.IsOptional
      };
   }
}