using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes.Options;
using Beskar.CodeGeneration.Extensions.Transformers.Symbols;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Transformers.Archetypes;

public static class NamedTypeSymbolArchetypeTransformer
{
   public static NamedTypeSymbolArchetype Transform(
      INamedTypeSymbol namedTypeSymbol,
      int depth = 1,
      ArchetypeTransformOptions? options = null)
   {
      options ??= new ArchetypeTransformOptions();
      
      var symbolSpec = SymbolSpecTransformer.Transform(namedTypeSymbol, depth, options);
      var typeSpec = TypeSymbolSpecTransformer.Transform(namedTypeSymbol, depth, options);
      var namedSpec = NamedTypeSymbolSpecTransformer.Transform(namedTypeSymbol, depth, options);
      
      return new NamedTypeSymbolArchetype(symbolSpec, typeSpec, namedSpec);
   }
}