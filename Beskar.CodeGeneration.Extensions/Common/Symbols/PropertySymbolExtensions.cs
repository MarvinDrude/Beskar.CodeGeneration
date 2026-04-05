using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes.Options;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Common.Symbols;

public static class PropertySymbolExtensions
{
   extension<TSymbol>(TSymbol symbol)
      where TSymbol : IPropertySymbol
   {
      public PropertySymbolArchetype CreateArchetype(ArchetypeTransformOptions? options = null)
      {
         options ??= new ArchetypeTransformOptions();
         options.ClearCache();
         
         return PropertySymbolArchetypeTransformer.Transform(symbol, options: options);
      }
   }
}