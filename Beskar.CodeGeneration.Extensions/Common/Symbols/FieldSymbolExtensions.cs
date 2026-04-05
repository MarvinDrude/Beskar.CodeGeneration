using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes.Options;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Common.Symbols;

public static class FieldSymbolExtensions
{
   extension<TSymbol>(TSymbol symbol)
      where TSymbol : IFieldSymbol
   {
      public FieldSymbolArchetype CreateArchetype(ArchetypeTransformOptions? options = null)
      {
         options ??= new ArchetypeTransformOptions();
         options.ClearCache();
         
         return FieldSymbolArchetypeTransformer.Transform(symbol, options: options);
      }
   }
}