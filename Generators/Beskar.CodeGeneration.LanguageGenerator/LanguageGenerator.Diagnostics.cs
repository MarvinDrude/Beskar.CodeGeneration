using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.LanguageGenerator;

public sealed partial class LanguageGenerator
{
   private static string InvalidTargetDiagnosticId => InvalidTargetRule.Id;
   private static readonly DiagnosticDescriptor InvalidTargetRule = new (
      id: "LG001",
      title: "Invalid [TranslationGroup] usage",
      messageFormat: "Target must be a enum",
      category: "LanguageGenerator",
      defaultSeverity: DiagnosticSeverity.Error,
      isEnabledByDefault: true);

   private static readonly Dictionary<string, DiagnosticDescriptor> Diagnostics = new()
   {
      [InvalidTargetDiagnosticId] = InvalidTargetRule
   };
}