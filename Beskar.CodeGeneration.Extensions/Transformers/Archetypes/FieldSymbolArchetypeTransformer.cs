using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;
using Beskar.CodeGeneration.Extensions.Transformers.Symbols;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Transformers.Archetypes;

public static class FieldSymbolArchetypeTransformer
{
   public static FieldSymbolArchetype Transform(IFieldSymbol fieldSymbol)
   {
      var symbolSpec = SymbolSpecTransformer.Transform(fieldSymbol);
      var fieldSpec = FieldSymbolSpecTransformer.Transform(fieldSymbol);
      
      return new FieldSymbolArchetype(symbolSpec, fieldSpec);
   }
}