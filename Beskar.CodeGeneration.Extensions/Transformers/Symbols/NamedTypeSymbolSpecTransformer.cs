using Beskar.CodeGeneration.Extensions.Models.Symbols;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Transformers.Symbols;

public static class NamedTypeSymbolSpecTransformer
{
   public static NamedTypeSymbolSpec Transform(INamedTypeSymbol namedTypeSymbol)
   {
      return new NamedTypeSymbolSpec()
      {
         IsEnum = namedTypeSymbol.TypeKind == TypeKind.Enum,
         IsFileLocal = namedTypeSymbol.IsFileLocal
      };
   }
}