using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Rendering;

public abstract class CodeRenderer(SourceProductionContext ctx)
{
   private readonly SourceProductionContext _ctx = ctx;

   protected abstract string Render();

   public void Render(string fileName)
   {
      _ctx.CancellationToken.ThrowIfCancellationRequested();
      _ctx.AddSource(fileName, Render());
   }
}