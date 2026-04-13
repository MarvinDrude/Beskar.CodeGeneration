using Beskar.CodeGeneration.ContentGenerator.Models;
using Beskar.CodeGeneration.ContentGenerator.Rendering;
using Beskar.CodeGeneration.Extensions.Common;
using Beskar.CodeGeneration.Extensions.Common.Specs;
using Beskar.CodeGeneration.Extensions.Models.Diagnostics;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.ContentGenerator;

public sealed partial class ContentGenerator
{
   private static void Render(
      SourceProductionContext context,
      string assemblyName,
      MaybeSpec<ContentTypeSpec> maybeSpec)
   {
      context.DispatchDiagnostics(Diagnostics, maybeSpec);
      if (!maybeSpec.HasValue)
      {
         return;
      }
      
      var ct = context.CancellationToken;
      ct.ThrowIfCancellationRequested();
      
      var renderer = new ContentTypeRenderer(context)
      {
         Spec = maybeSpec.Value
      };
      
      renderer.Render(maybeSpec.Value.NamedType.Symbol.GeneratedFilePath);
   }
}