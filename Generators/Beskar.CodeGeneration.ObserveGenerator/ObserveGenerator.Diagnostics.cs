using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.ObserveGenerator;

public sealed partial class ObserveGenerator
{
   private static string InvalidTargetDiagnosticId => InvalidTargetRule.Id;
   private static readonly DiagnosticDescriptor InvalidTargetRule = new (
      id: "OBG001",
      title: "Invalid ObserveGenerator Target usage",
      messageFormat: "Invalid target or congifuration for ObserveGenerator. Must be a class.",
      category: "ObserveGenerator",
      defaultSeverity: DiagnosticSeverity.Error,
      isEnabledByDefault: true);

   private static readonly Dictionary<string, DiagnosticDescriptor> Diagnostics = new()
   {
      [InvalidTargetDiagnosticId] = InvalidTargetRule
   };
}