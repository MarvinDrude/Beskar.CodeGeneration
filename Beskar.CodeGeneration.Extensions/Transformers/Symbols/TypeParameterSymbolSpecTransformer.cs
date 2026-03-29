using Beskar.CodeGeneration.Extensions.Models.Symbols;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Transformers.Symbols;

public static class TypeParameterSymbolSpecTransformer
{
   public static TypeParameterSymbolSpec Transform(ITypeParameterSymbol typeParameterSymbol)
   {
      return new TypeParameterSymbolSpec()
      {
         Ordinal = typeParameterSymbol.Ordinal,
         
         AllowsRefLikeType = typeParameterSymbol.AllowsRefLikeType,
         HasConstructorConstraint = typeParameterSymbol.HasConstructorConstraint,
         HasReferenceTypeConstraint = typeParameterSymbol.HasReferenceTypeConstraint,
         HasValueTypeConstraint = typeParameterSymbol.HasValueTypeConstraint,
         HasUnmanagedTypeConstraint = typeParameterSymbol.HasUnmanagedTypeConstraint,
         HasNotNullConstraint = typeParameterSymbol.HasNotNullConstraint,
      };
   }
}