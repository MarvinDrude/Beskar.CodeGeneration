using Beskar.CodeGeneration.Extensions.Common;
using Beskar.CodeGeneration.Extensions.Common.Specs;
using Beskar.CodeGeneration.Extensions.Models.Diagnostics;
using Beskar.CodeGeneration.ProcessorGenerator.Models;
using Beskar.CodeGeneration.ProcessorGenerator.Rendering;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.ProcessorGenerator;

public sealed partial class ProcessorGenerator
{
   private static void RenderPipeline(
      SourceProductionContext context,
      string assemblyName,
      MaybeSpec<ProcessorPipelineSpec> maybeSpec)
   {
      context.DispatchDiagnostics(Diagnostics, maybeSpec);
      if (!maybeSpec.HasValue)
      {
         return;
      }
      
      var ct = context.CancellationToken;
      ct.ThrowIfCancellationRequested();
      
      var renderer = new PipelineRenderer(context)
      {
         Spec = maybeSpec.Value
      };
      renderer.Render(maybeSpec.Value.Archetype.Symbol.GeneratedFilePath);
   }
   
   private static void RenderProcessor(
      SourceProductionContext context,
      string assemblyName,
      MaybeSpec<ProcessorSpec> maybeSpec)
   {
      context.DispatchDiagnostics(Diagnostics, maybeSpec);
      if (!maybeSpec.HasValue)
      {
         return;
      }
      
      // only diagnostics so far for processors themselves
   }
}