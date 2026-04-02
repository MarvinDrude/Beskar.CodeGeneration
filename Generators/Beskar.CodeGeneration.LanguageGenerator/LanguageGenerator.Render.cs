using Beskar.CodeGeneration.Extensions.Common;
using Beskar.CodeGeneration.Extensions.Common.Specs;
using Beskar.CodeGeneration.Extensions.Models.Diagnostics;
using Beskar.CodeGeneration.LanguageGenerator.Models;
using Beskar.CodeGeneration.LanguageGenerator.Rendering;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.LanguageGenerator;

public sealed partial class LanguageGenerator
{
   public static void Render(
      SourceProductionContext context,
      string assemblyName,
      MaybeSpec<LanguageEnumSpec> maybeSpec)
   {
      context.DispatchDiagnostics(Diagnostics, maybeSpec);
      if (!maybeSpec.HasValue)
      {
         return;
      }
      
      var ct = context.CancellationToken;
      ct.ThrowIfCancellationRequested();
      
      var renderer = new LangGroupRenderer(context)
      {
         Spec = maybeSpec.Value
      };

      renderer.Render(maybeSpec.Value.EnumTypeArchetype.Symbol.GeneratedFilePath);
   }
}