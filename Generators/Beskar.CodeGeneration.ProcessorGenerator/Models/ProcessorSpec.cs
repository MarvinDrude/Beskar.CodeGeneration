using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;

namespace Beskar.CodeGeneration.ProcessorGenerator.Models;

public readonly record struct ProcessorSpec(
   NamedTypeSymbolArchetype ProcessorArchetype);