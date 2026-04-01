using Beskar.CodeGeneration.Extensions.Common.Specs;
using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;

namespace Beskar.CodeGeneration.Extensions.Common.Archetypes;

public static class TypeSymbolArchetypeExtensions
{
   extension(ref TypeSymbolArchetype archetype)
   {
      public bool IsGuid => archetype.Symbol.IsGuid;
   }
}