using Beskar.CodeGeneration.Extensions.Models.Symbols;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes.Options;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Transformers.Symbols;

public static class TypeSymbolSpecTransformer
{
   public static TypeSymbolSpec Transform(
      ITypeSymbol typeSymbol,
      int depth = 1,
      ArchetypeTransformOptions? options = null)
   {
      options ??= new ArchetypeTransformOptions();
      
      var spec = new TypeSymbolSpec()
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

      if (options.Types.Load.Attributes)
      {
         spec.Attributes = options.GetAttributes(typeSymbol, typeSymbol.GetAttributes());
      }

      if (depth > options.Types.Depth)
      {
         return spec;
      }
      
      if (options.Types.Load.BaseType)
      {
         spec.BaseType = typeSymbol.BaseType is { SpecialType: not SpecialType.System_Object and not SpecialType.System_ValueType }
            ? NamedTypeSymbolArchetypeTransformer.Transform(typeSymbol.BaseType, depth + 1, options)
            : null;
      }

      if (options.Types.Load.Interfaces)
      {
         spec.Interfaces = [.. 
            typeSymbol
               .Interfaces
               .Select(i => NamedTypeSymbolArchetypeTransformer.Transform(i, depth + 1, options))
         ];
      }

      if (options.Types.Load.AllInterfaces)
      {
         spec.AllInterfaces = [.. 
            typeSymbol
               .AllInterfaces
               .Select(i => NamedTypeSymbolArchetypeTransformer.Transform(i, depth + 1, options))
         ];
      }
      
      return spec;
   }
}