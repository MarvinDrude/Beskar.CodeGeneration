using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes.Options;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Common.Symbols;

public static class NamedTypeSymbolExtensions
{
   extension<TSymbol>(TSymbol named)
      where TSymbol : INamedTypeSymbol
   {
      public NamedTypeSymbolArchetype CreateNamedArchetype(ArchetypeTransformOptions? options = null)
      {
         options ??= new ArchetypeTransformOptions();
         options.ClearCache();
         
         return NamedTypeSymbolArchetypeTransformer.Transform(named, options: options);
      }
   }
}