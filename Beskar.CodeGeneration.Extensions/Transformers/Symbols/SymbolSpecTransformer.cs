using Beskar.CodeGeneration.Extensions.Models.Symbols;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes.Options;
using Beskar.CodeGeneration.Extensions.Transformers.Symbols.Options;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Transformers.Symbols;

public static class SymbolSpecTransformer
{
   public static SymbolSpec Transform<TSymbol>(
      TSymbol symbol,
      int depth = 1,
      ArchetypeTransformOptions? options = null)
      where TSymbol : ISymbol
   {
      return new SymbolSpec()
      {
         Accessibility = symbol.DeclaredAccessibility,
         Kind = symbol.Kind,
         
         Name = symbol.Name,
         MetadataName = symbol.MetadataName,
         FullName = symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
         
         IsStatic = symbol.IsStatic,
         IsAbstract = symbol.IsAbstract,
         IsSealed = symbol.IsSealed,
         IsVirtual = symbol.IsVirtual,
         IsExtern = symbol.IsExtern,
         IsOverride = symbol.IsOverride,
         IsImplicitlyDeclared = symbol.IsImplicitlyDeclared,
      };
   }
}