using Beskar.CodeGeneration.ProcessorGenerator.Enums;
using Me.Memory.Collections;

namespace Beskar.CodeGeneration.ProcessorGenerator.Models;

public readonly record struct ProcessorRegisterSpec(
   string FullTypeName,
   string PropertyName,
   string InputFullTypeName,
   string OutputFullTypeName,
   ProcessorKind ExecuteKind,
   ProcessorKind? PostKind,
   StepSpec StepSpec,
   SequenceArray<SettingSpec> Settings);