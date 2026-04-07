using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.ProcessorGenerator;

public sealed partial class ProcessorGenerator
{
   private static string InvalidTargetDiagnosticId => InvalidTargetRule.Id;
   private static readonly DiagnosticDescriptor InvalidTargetRule = new (
      id: "PCG001",
      title: "Invalid [Processor] usage",
      messageFormat: "Processor needs to be a class and implement one of the correct interfaces",
      category: "ProcessorGenerator",
      defaultSeverity: DiagnosticSeverity.Error,
      isEnabledByDefault: true);
   
   private static string InvalidPipelineTargetDiagnosticId => InvalidPipelineTargetRule.Id;
   private static readonly DiagnosticDescriptor InvalidPipelineTargetRule = new (
      id: "PCG002",
      title: "Invalid [ProcessorPipeline] usage",
      messageFormat: "Invalid type: ProcessorPipeline must be a class with processor steps",
      category: "ProcessorGenerator",
      defaultSeverity: DiagnosticSeverity.Error,
      isEnabledByDefault: true);

   private static readonly Dictionary<string, DiagnosticDescriptor> Diagnostics = new()
   {
      [InvalidTargetDiagnosticId] = InvalidTargetRule,
      [InvalidPipelineTargetDiagnosticId] = InvalidPipelineTargetRule,
   };
}