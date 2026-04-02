using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;

namespace Beskar.CodeGeneration.LanguageGenerator.Models;

public readonly record struct LanguageEnumSpec(
   string GroupName,
   NamedTypeSymbolArchetype EnumTypeArchetype);