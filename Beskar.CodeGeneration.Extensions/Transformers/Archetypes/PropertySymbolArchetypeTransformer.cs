using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;
using Beskar.CodeGeneration.Extensions.Transformers.Symbols;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Transformers.Archetypes;

public static class PropertySymbolArchetypeTransformer
{
   public static PropertySymbolArchetype Transform(IPropertySymbol propertySymbol)
   {
      var symbolSpec = SymbolSpecTransformer.Transform(propertySymbol);
      var propertySpec = PropertySymbolSpecTransformer.Transform(propertySymbol);
      
      return new PropertySymbolArchetype(symbolSpec, propertySpec);
   }
}