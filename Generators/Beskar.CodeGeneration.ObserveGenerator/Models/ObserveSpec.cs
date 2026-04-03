using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;
using Me.Memory.Collections;

namespace Beskar.CodeGeneration.ObserveGenerator.Models;

public readonly record struct ObserveSpec(
   NamedTypeSymbolArchetype NamedTypeArchetype,
   ObserveActivitySpec? ActivitySpec,
   ObserveMeterSpec? MeterSpec,
   SequenceArray<ObserveInstrumentSpec> InsrumentSpecs);