using Beskar.CodeGeneration.Extensions.Common.Specs;
using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;

namespace Beskar.CodeGeneration.Extensions.Common.Archetypes;

public static class NamedTypeSymbolArchetypeExtensions
{
   extension(ref NamedTypeSymbolArchetype archetype)
   {
      public bool IsGuid => archetype.Symbol.IsGuid;
   }
}