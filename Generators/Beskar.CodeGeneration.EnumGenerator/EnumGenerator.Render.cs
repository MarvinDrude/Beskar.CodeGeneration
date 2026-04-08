using Beskar.CodeGeneration.EnumGenerator.Models;
using Beskar.CodeGeneration.EnumGenerator.Rendering;
using Beskar.CodeGeneration.Extensions.Common;
using Beskar.CodeGeneration.Extensions.Models.Diagnostics;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.EnumGenerator;

public sealed partial class EnumGenerator
{
   private static void Render(
      SourceProductionContext context,
      string assemblyName,
      MaybeSpec<FastEnumSpec> maybeSpec)
   {
      context.DispatchDiagnostics(Diagnostics, maybeSpec);
      if (!maybeSpec.HasValue)
      {
         return;
      }

      var ct = context.CancellationToken;
      ct.ThrowIfCancellationRequested();

      var renderer = new EnumExtensionRenderer(context)
      {
         Spec = maybeSpec.Value
      };
      
      var nameSpace = maybeSpec.Value.NamedType.Symbol.NameSpace
         is { Length: > 0 } ns ? ns : "Global.";
      
      renderer.Render($"{nameSpace}.{maybeSpec.Value.NamedType.Symbol.Name}Extensions.g.cs");
   }
}