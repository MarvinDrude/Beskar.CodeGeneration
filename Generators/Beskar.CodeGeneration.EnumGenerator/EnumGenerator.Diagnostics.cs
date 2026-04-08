using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.EnumGenerator;

public sealed partial class EnumGenerator
{
   private static string InvalidTargetDiagnosticId => InvalidTargetRule.Id;
   private static readonly DiagnosticDescriptor InvalidTargetRule = new (
      id: "EG001",
      title: "Invalid [FastEnum] usage",
      messageFormat: "Invalid enum for being decorated with [FastEnum]. Needs to be a enum.",
      category: "EnumGenerator",
      defaultSeverity: DiagnosticSeverity.Error,
      isEnabledByDefault: true);

   private static readonly Dictionary<string, DiagnosticDescriptor> Diagnostics = new()
   {
      [InvalidTargetDiagnosticId] = InvalidTargetRule
   };
}