using System.Diagnostics;

namespace Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;

[DebuggerDisplay("Type: {Symbol.FullName, nq}")]
public readonly record struct TypeSymbolArchetype(
   SymbolSpec Symbol,
   TypeSymbolSpec Type,
   NamedTypeSymbolSpec? NamedType);