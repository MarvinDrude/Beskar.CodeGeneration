using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes.Options;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Common.Symbols;

public static class ParameterSymbolExtensions
{
   extension<TSymbol>(TSymbol symbol)
      where TSymbol : IParameterSymbol
   {
      public ParameterSymbolArchetype CreateArchetype(ArchetypeTransformOptions? options = null)
      {
         options ??= new ArchetypeTransformOptions();
         options.ClearCache();
         
         return ParameterSymbolArchetypeTransformer.Transform(symbol, options: options);
      }
   }
}