using System.Collections.Immutable;
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
      ImmutableArray<MaybeSpec<ContentTypeSpec>> maybeSpecs)
   {
      Dictionary<string, ContentTypeSpec> contentTypes = [];
      foreach (var maybeSpec in maybeSpecs)
      {
         context.DispatchDiagnostics(Diagnostics, maybeSpec);
         if (maybeSpec.HasValue)
         {
            contentTypes[maybeSpec.Value.NamedType.Symbol.FullName] = maybeSpec.Value;
         }
      }
      
      var ct = context.CancellationToken;
      ct.ThrowIfCancellationRequested();

      foreach (var (_, spec) in contentTypes)
      {
         var renderer = new ContentTypeRenderer(context)
         {
            ContentTypes = contentTypes,
            Spec = spec,
         };
      
         renderer.Render(spec.NamedType.Symbol.GeneratedFilePath);
      }
   }
}