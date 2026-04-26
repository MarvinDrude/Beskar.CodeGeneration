using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;

namespace Beskar.CodeGeneration.PacketGenerator.Models;

public readonly record struct PacketRegistrySpec(
   NamedTypeSymbolArchetype NamedTypeArchetype,
   string? StateTypeFullName);