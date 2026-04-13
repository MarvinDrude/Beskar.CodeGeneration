using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.ContentGenerator;

public sealed partial class ContentGenerator
{
   private static string InvalidTargetDiagnosticId => InvalidTargetRule.Id;
   private static readonly DiagnosticDescriptor InvalidTargetRule = new (
      id: "CG001",
      title: "Invalid [ContentType] usage",
      messageFormat: "Invalid class for being decorated with [ContentType]. Needs to be derived from ComponentBase.",
      category: "ContentGenerator",
      defaultSeverity: DiagnosticSeverity.Error,
      isEnabledByDefault: true);
   
   private static string InvalidPropertyDiagnosticId => InvalidPropertyRule.Id;
   private static readonly DiagnosticDescriptor InvalidPropertyRule = new (
      id: "CG002",
      title: "Invalid property",
      messageFormat: "Invalid property inside of a ContentType class",
      category: "ContentGenerator",
      defaultSeverity: DiagnosticSeverity.Error,
      isEnabledByDefault: true);

   private static readonly Dictionary<string, DiagnosticDescriptor> Diagnostics = new()
   {
      [InvalidTargetDiagnosticId] = InvalidTargetRule,
      [InvalidPropertyDiagnosticId] = InvalidPropertyRule
   };
}