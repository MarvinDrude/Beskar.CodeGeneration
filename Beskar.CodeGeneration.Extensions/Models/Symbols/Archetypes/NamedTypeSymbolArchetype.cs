namespace Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;

public readonly record struct NamedTypeSymbolArchetype(
   SymbolSpec Symbol,
   TypeSymbolSpec Type,
   NamedTypeSymbolSpec NamedType);