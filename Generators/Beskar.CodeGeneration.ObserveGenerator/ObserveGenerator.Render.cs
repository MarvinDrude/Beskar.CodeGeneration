using Beskar.CodeGeneration.Extensions.Common;
using Beskar.CodeGeneration.Extensions.Common.Specs;
using Beskar.CodeGeneration.Extensions.Models.Diagnostics;
using Beskar.CodeGeneration.ObserveGenerator.Models;
using Beskar.CodeGeneration.ObserveGenerator.Rendering;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.ObserveGenerator;

public sealed partial class ObserveGenerator
{
   public static void Render(
      SourceProductionContext context,
      string assemblyName,
      MaybeSpec<ObserveSpec> maybeSpec)
   {
      context.DispatchDiagnostics(Diagnostics, maybeSpec);
      if (!maybeSpec.HasValue)
      {
         return;
      }
      
      var ct = context.CancellationToken;
      ct.ThrowIfCancellationRequested();
      
      var renderer = new ObserveRenderer(context)
      {
         Spec = maybeSpec.Value
      };
      
      renderer.Render(maybeSpec.Value.NamedTypeArchetype.Symbol.GeneratedFilePath);
   }
}