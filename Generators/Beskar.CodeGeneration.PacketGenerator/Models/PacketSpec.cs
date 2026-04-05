using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;
using Me.Memory.Collections;

namespace Beskar.CodeGeneration.PacketGenerator.Models;

public readonly record struct PacketSpec(
   SequenceArray<string> RegistryFullTypeNames,
   NamedTypeSymbolArchetype NamedTypeArchetype);