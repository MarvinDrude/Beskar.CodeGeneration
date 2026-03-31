using System.Diagnostics;

namespace Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;

[DebuggerDisplay("Field: {Symbol.FullName, nq}")]
public readonly record struct FieldSymbolArchetype(
   SymbolSpec Symbol,
   FieldSymbolSpec Field);