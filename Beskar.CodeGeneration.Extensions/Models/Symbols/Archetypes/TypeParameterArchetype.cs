using System.Diagnostics;

namespace Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;

[DebuggerDisplay("TypeParameter: {Symbol.FullName, nq}")]
public readonly record struct TypeParameterArchetype(
   SymbolSpec Symbol,
   TypeParameterSymbolSpec TypeParameter);