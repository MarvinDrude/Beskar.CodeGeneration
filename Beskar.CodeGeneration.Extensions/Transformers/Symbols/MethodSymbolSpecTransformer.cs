using Beskar.CodeGeneration.Extensions.Models.Symbols;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Transformers.Symbols;

public static class MethodSymbolSpecTransformer
{
   public static MethodSymbolSpec Transform(IMethodSymbol methodSymbol)
   {
      return new MethodSymbolSpec()
      {
         MethodKind = methodSymbol.MethodKind,
         
         HasVoidReturn = methodSymbol.ReturnsVoid,
         IsAsync = methodSymbol.IsAsync,
         IsIterator = methodSymbol.IsIterator,
         IsReadOnly = methodSymbol.IsReadOnly,
         ReturnsByRef = methodSymbol.ReturnsByRef,
         ReturnsByRefReadonly = methodSymbol.ReturnsByRefReadonly,
      };
   }
}