using Beskar.CodeGeneration.Extensions.Models.Symbols;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes.Options;
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
      options ??= new ArchetypeTransformOptions();
      
      var spec = new SymbolSpec()
      {
         Accessibility = symbol.DeclaredAccessibility,
         Kind = symbol.Kind,
         
         Name = symbol.Name,
         MetadataName = symbol.MetadataName,
         FullName = symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
         NameSpace = symbol.ContainingNamespace?.ToDisplayString(),
         
         IsStatic = symbol.IsStatic,
         IsAbstract = symbol.IsAbstract,
         IsSealed = symbol.IsSealed,
         IsVirtual = symbol.IsVirtual,
         IsExtern = symbol.IsExtern,
         IsOverride = symbol.IsOverride,
         IsImplicitlyDeclared = symbol.IsImplicitlyDeclared,
      };

      if (options.Symbols.Load.Attributes)
      {
         spec.Attributes = options.GetAttributes(symbol.GetAttributes());
      }
      
      return spec;
   }
}