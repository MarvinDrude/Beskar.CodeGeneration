using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes.Options;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Common.Symbols;

public static class TypeParameterSymbolExtensions
{
   extension<TSymbol>(TSymbol symbol)
      where TSymbol : ITypeParameterSymbol
   {
      public TypeParameterArchetype CreateArchetype(ArchetypeTransformOptions? options = null)
      {
         options ??= new ArchetypeTransformOptions();
         options.ClearCache();
         
         return TypeParameterSymbolArchetypeTransformer.Transform(symbol, options: options);
      }
   }
}