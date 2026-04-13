using Beskar.CodeGeneration.ContentGenerator.Enums;
using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;
using Me.Memory.Collections;

namespace Beskar.CodeGeneration.ContentGenerator.Models;

public readonly record struct ContentTypeSpec(
   NamedTypeSymbolArchetype NamedType,
   ContentTypeKind Kind,
   SequenceArray<FieldSpec> Fields);