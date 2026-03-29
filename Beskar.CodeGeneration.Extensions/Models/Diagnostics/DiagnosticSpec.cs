using Me.Memory.Collections;

namespace Beskar.CodeGeneration.Extensions.Models.Diagnostics;

public readonly record struct DiagnosticSpec(
   string DiagnosticId,
   SequenceArray<string> Arguments);