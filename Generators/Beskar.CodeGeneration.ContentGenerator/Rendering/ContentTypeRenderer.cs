using Beskar.CodeGeneration.ContentGenerator.Models;
using Beskar.CodeGeneration.Extensions.Rendering;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.ContentGenerator.Rendering;

public sealed class ContentTypeRenderer(SourceProductionContext ctx) 
   : CodeRenderer(ctx)
{
   public required ContentTypeSpec Spec { get; init; }
   
   protected override string Render()
   {
      return "";
   }
}