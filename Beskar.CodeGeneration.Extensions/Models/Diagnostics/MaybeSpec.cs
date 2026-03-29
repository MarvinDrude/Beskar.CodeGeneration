using Me.Memory.Collections;

namespace Beskar.CodeGeneration.Extensions.Models.Diagnostics;

public readonly record struct MaybeSpec<T>(
   bool HasValue,
   T Value,
   SequenceArray<DiagnosticSpec> Diagnostics);