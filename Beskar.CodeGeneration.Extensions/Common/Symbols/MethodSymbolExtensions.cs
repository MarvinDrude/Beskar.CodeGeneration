using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes.Options;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Common.Symbols;

public static class MethodSymbolExtensions
{
   extension<TSymbol>(TSymbol symbol)
      where TSymbol : IMethodSymbol
   {
      public MethodSymbolArchetype CreateArchetype(ArchetypeTransformOptions? options = null)
      {
         options ??= new ArchetypeTransformOptions();
         options.ClearCache();
         
         return MethodSymbolArchetypeTransformer.Transform(symbol, options: options);
      }
   }
}