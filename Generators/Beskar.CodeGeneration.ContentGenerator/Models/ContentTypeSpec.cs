using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;

namespace Beskar.CodeGeneration.ContentGenerator.Models;

public readonly record struct ContentTypeSpec(
   NamedTypeSymbolArchetype NamedType,
   string ContentTypeFullName);