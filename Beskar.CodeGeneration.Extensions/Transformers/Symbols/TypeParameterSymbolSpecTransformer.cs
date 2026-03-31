using Beskar.CodeGeneration.Extensions.Models.Symbols;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes.Options;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Transformers.Symbols;

public static class TypeParameterSymbolSpecTransformer
{
   public static TypeParameterSymbolSpec Transform(
      ITypeParameterSymbol typeParameterSymbol,
      int depth = 1,
      ArchetypeTransformOptions? options = null)
   {
      options ??= new ArchetypeTransformOptions();
      
      var spec = new TypeParameterSymbolSpec()
      {
         Ordinal = typeParameterSymbol.Ordinal,
         
         AllowsRefLikeType = typeParameterSymbol.AllowsRefLikeType,
         HasConstructorConstraint = typeParameterSymbol.HasConstructorConstraint,
         HasReferenceTypeConstraint = typeParameterSymbol.HasReferenceTypeConstraint,
         HasValueTypeConstraint = typeParameterSymbol.HasValueTypeConstraint,
         HasUnmanagedTypeConstraint = typeParameterSymbol.HasUnmanagedTypeConstraint,
         HasNotNullConstraint = typeParameterSymbol.HasNotNullConstraint,
      };

      if (options.TypeParameters.Depth > depth)
      {
         return spec;
      }

      if (options.TypeParameters.Load.ConstraintTypes)
      {
         spec.ConstraintTypes = [..
            typeParameterSymbol.ConstraintTypes
               .Select(constraintType => TypeSymbolArchetypeTransformer.Transform(constraintType, depth + 1, options))
         ];
      }
      
      return spec;
   }
}