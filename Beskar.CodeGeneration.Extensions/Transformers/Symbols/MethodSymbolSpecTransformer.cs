using Beskar.CodeGeneration.Extensions.Models.Symbols;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes.Options;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Transformers.Symbols;

public static class MethodSymbolSpecTransformer
{
   public static MethodSymbolSpec Transform(
      IMethodSymbol methodSymbol,
      int depth = 1,
      ArchetypeTransformOptions? options = null)
   {
      options ??= new ArchetypeTransformOptions();
      
      var spec = new MethodSymbolSpec()
      {
         MethodKind = methodSymbol.MethodKind,
         
         HasVoidReturn = methodSymbol.ReturnsVoid,
         IsAsync = methodSymbol.IsAsync,
         IsIterator = methodSymbol.IsIterator,
         IsReadOnly = methodSymbol.IsReadOnly,
         ReturnsByRef = methodSymbol.ReturnsByRef,
         ReturnsByRefReadonly = methodSymbol.ReturnsByRefReadonly,
      };

      if (options.Methods.Load.Attributes)
      {
         spec.Attributes = options.GetAttributes(methodSymbol, methodSymbol.GetAttributes());
      }

      if (depth > options.Methods.Depth)
      {
         return spec;
      }

      if (options.Methods.Load.ReturnType)
      {
         spec.ReturnType = TypeSymbolArchetypeTransformer.Transform(methodSymbol.ReturnType, depth + 1, options);
      }

      if (options.Methods.Load.Parameters)
      {
         spec.Parameters =
            [.. methodSymbol.Parameters
               .Where(x => options.Methods.ParameterFilter is null || options.Methods.ParameterFilter(x))
               .Select(x => ParameterSymbolArchetypeTransformer.Transform(x, depth + 1, options))];
      }
      
      return spec;
   }
}