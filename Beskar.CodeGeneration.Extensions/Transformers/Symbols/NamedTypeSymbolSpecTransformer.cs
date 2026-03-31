using Beskar.CodeGeneration.Extensions.Models.Symbols;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes.Options;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Transformers.Symbols;

public static class NamedTypeSymbolSpecTransformer
{
   public static NamedTypeSymbolSpec Transform(
      INamedTypeSymbol namedTypeSymbol,
      int depth = 1,
      ArchetypeTransformOptions? options = null)
   {
      options ??= new ArchetypeTransformOptions();
      
      var spec = new NamedTypeSymbolSpec()
      {
         IsEnum = namedTypeSymbol.TypeKind == TypeKind.Enum,
         IsFileLocal = namedTypeSymbol.IsFileLocal,
         Arity = namedTypeSymbol.Arity,
      };

      if (depth > options.NamedTypes.Depth)
      {
         return spec;
      }

      if (options.NamedTypes.Load.Methods)
      {
         spec.Methods = [.. 
            namedTypeSymbol
               .GetMembers()
               .OfType<IMethodSymbol>()
               .Where(m => options.NamedTypes.MethodFilter is null || options.NamedTypes.MethodFilter(m))
               .Select(m => MethodSymbolArchetypeTransformer.Transform(m, depth + 1, options))
         ];
      }
      
      if (options.NamedTypes.Load.TypeParameters)
      {
         spec.TypeParameters = [.. 
            namedTypeSymbol
               .TypeParameters
               .Select(tp => TypeParameterSymbolArchetypeTransformer.Transform(tp, depth + 1, options))
         ];
      }

      if (options.NamedTypes.Load.TypeArguments)
      {
         spec.TypeArguments = [.. 
            namedTypeSymbol
               .TypeArguments
               .Select(ta => TypeSymbolArchetypeTransformer.Transform(ta, depth + 1, options))
         ];
      }
      
      if (options.NamedTypes.Load.TypeArgumentNullableAnnotations)
      {
         spec.TypeArgumentNullableAnnotations = [.. 
            namedTypeSymbol
               .TypeArguments
               .Select(ta => ta.NullableAnnotation)
         ];
      }

      return spec;
   }
}