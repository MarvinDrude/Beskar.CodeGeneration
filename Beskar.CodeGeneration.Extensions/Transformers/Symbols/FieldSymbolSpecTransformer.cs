using Beskar.CodeGeneration.Extensions.Models.Symbols;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Transformers.Symbols;

public static class FieldSymbolSpecTransformer
{
   public static FieldSymbolSpec Transform(IFieldSymbol fieldSymbol)
   {
      return new FieldSymbolSpec()
      {
         RefKind = fieldSymbol.RefKind,
         
         IsReadOnly = fieldSymbol.IsReadOnly,
         IsRequired = fieldSymbol.IsRequired,
         IsVolatile = fieldSymbol.IsVolatile,
         HasConstantValue = fieldSymbol.HasConstantValue,
         IsConst = fieldSymbol.IsConst,
      };
   }
}