using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes.Options;
using Beskar.CodeGeneration.Extensions.Transformers.Symbols;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Transformers.Archetypes;

public static class FieldSymbolArchetypeTransformer
{
   public static FieldSymbolArchetype Transform(
      IFieldSymbol fieldSymbol,
      int depth = 1,
      ArchetypeTransformOptions? options = null)
   {
      var symbolSpec = SymbolSpecTransformer.Transform(fieldSymbol, depth, options);
      var fieldSpec = FieldSymbolSpecTransformer.Transform(fieldSymbol, depth, options);
      
      return new FieldSymbolArchetype(symbolSpec, fieldSpec);
   }
}