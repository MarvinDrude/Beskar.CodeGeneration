using Beskar.CodeGeneration.Extensions.Common.Symbols;
using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;
using Me.Memory.Collections;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.ProcessorGenerator.Models;

public readonly record struct ProcessorPipelineSpec(
   NamedTypeSymbolArchetype Archetype,
   string Name,
   TimeoutSpec? TimeoutSpec,
   SequenceArray<ContextVariableSpec> ContextVariables,
   SequenceArray<ProcessorRegisterSpec> ProcessorRegisters);

public sealed class ProcessorPipelineBuilder
{
   public string PipelineName { get; set; } = string.Empty;
   
   public TimeoutSpec? TimeoutSpec { get; set; }
   
   public List<ContextVariableSpec> ContextVariables { get; } = [];
   
   public List<ProcessorRegisterSpec> ProcessorRegisters { get; } = [];

   public ProcessorPipelineSpec Build(INamedTypeSymbol archetype)
   {
      return new ProcessorPipelineSpec(
         archetype.CreateNamedArchetype(),
         PipelineName,
         TimeoutSpec,
         [.. ContextVariables],
         [.. ProcessorRegisters]);
   }
}