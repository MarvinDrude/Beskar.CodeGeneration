using Beskar.CodeGeneration.ProcessorGenerator.Enums;
using Me.Memory.Collections;

namespace Beskar.CodeGeneration.ProcessorGenerator.Models;

public readonly record struct ProcessorRegisterSpec(
   string FullTypeName,
   ProcessorKind ExecuteKind,
   ProcessorKind? PostKind,
   StepSpec StepSpec,
   SequenceArray<SettingSpec> Settings);