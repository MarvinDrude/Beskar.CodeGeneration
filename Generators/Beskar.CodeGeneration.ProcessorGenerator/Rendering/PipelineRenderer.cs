using Beskar.CodeGeneration.Extensions.Rendering;
using Beskar.CodeGeneration.ProcessorGenerator.Models;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.ProcessorGenerator.Rendering;

public sealed class PipelineRenderer(SourceProductionContext ctx) 
   : CodeRenderer(ctx)
{
   public required ProcessorPipelineSpec Spec { get; init; }

   protected override string Render()
   {
      throw new NotImplementedException();
   }
}