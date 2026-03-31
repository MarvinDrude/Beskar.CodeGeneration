using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes.Options;
using Beskar.CodeGeneration.Extensions.Transformers.Symbols;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Transformers.Archetypes;

public static class TypeParameterSymbolArchetypeTransformer
{
   public static TypeParameterArchetype Transform(
      ITypeParameterSymbol typeParameterSymbol,
      int depth = 1,
      ArchetypeTransformOptions? options = null)
   {
      options ??= new ArchetypeTransformOptions();
      
      var symbolSpec = SymbolSpecTransformer.Transform(typeParameterSymbol, depth, options);
      var typeParameterSpec = TypeParameterSymbolSpecTransformer.Transform(typeParameterSymbol, depth, options);
      
      return new TypeParameterArchetype(symbolSpec, typeParameterSpec);
   }
}