using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;

namespace Beskar.CodeGeneration.TypeIdGenerator.Models;

public readonly record struct TypeSafeIdSpec(
   TypeSafeIdAttributeSpec AttributeSpec,
   NamedTypeSymbolArchetype NamedTargetArchetype);