namespace Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;

public readonly record struct TypeSymbolArchetype(
   SymbolSpec Symbol,
   TypeSymbolSpec Type,
   NamedTypeSymbolSpec? NamedType);