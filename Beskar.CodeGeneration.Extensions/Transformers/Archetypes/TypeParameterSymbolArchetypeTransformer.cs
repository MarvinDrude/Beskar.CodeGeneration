using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;
using Beskar.CodeGeneration.Extensions.Transformers.Symbols;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Transformers.Archetypes;

public static class TypeParameterSymbolArchetypeTransformer
{
   public static TypeParameterArchetype Transform(ITypeParameterSymbol typeParameterSymbol)
   {
      var symbolSpec = SymbolSpecTransformer.Transform(typeParameterSymbol);
      var typeParameterSpec = TypeParameterSymbolSpecTransformer.Transform(typeParameterSymbol);
      
      return new TypeParameterArchetype(symbolSpec, typeParameterSpec);
   }
}