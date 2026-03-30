using Beskar.CodeGeneration.Extensions.Models.Symbols;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes.Options;
using Beskar.CodeGeneration.Extensions.Transformers.Symbols.Options;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Transformers.Symbols;

public static class NamedTypeSymbolSpecTransformer
{
   public static NamedTypeSymbolSpec Transform(
      INamedTypeSymbol namedTypeSymbol,
      int depth = 1,
      ArchetypeTransformOptions? options = null)
   {
      return new NamedTypeSymbolSpec()
      {
         IsEnum = namedTypeSymbol.TypeKind == TypeKind.Enum,
         IsFileLocal = namedTypeSymbol.IsFileLocal
      };
   }
}