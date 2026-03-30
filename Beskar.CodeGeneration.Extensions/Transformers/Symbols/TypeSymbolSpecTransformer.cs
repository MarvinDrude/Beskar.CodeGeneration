using Beskar.CodeGeneration.Extensions.Models.Symbols;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes.Options;
using Beskar.CodeGeneration.Extensions.Transformers.Symbols.Options;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Transformers.Symbols;

public static class TypeSymbolSpecTransformer
{
   public static TypeSymbolSpec Transform(
      ITypeSymbol typeSymbol,
      int depth = 1,
      ArchetypeTransformOptions? options = null)
   {
      return new TypeSymbolSpec()
      {
         Kind  = typeSymbol.TypeKind,
         SpecialType = typeSymbol.SpecialType,
         NullableAnnotation = typeSymbol.NullableAnnotation,
         
         HasBaseType = typeSymbol.BaseType is not null,
         IsReadOnly = typeSymbol.IsReadOnly,
         IsRecord = typeSymbol.IsRecord,
         IsReferenceType = typeSymbol.IsReferenceType,
         IsRefLikeType = typeSymbol.IsRefLikeType,
         IsTupleType = typeSymbol.IsTupleType,
         IsValueType = typeSymbol.IsValueType,
         IsUnmanagedType = typeSymbol.IsUnmanagedType,
      };
   }
}