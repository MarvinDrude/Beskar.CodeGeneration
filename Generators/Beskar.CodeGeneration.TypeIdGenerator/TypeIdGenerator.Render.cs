using Beskar.CodeGeneration.Extensions.Common;
using Beskar.CodeGeneration.Extensions.Common.Specs;
using Beskar.CodeGeneration.Extensions.Models.Diagnostics;
using Beskar.CodeGeneration.TypeIdGenerator.Models;
using Beskar.CodeGeneration.TypeIdGenerator.Rendering;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.TypeIdGenerator;

public sealed partial class TypeIdGenerator
{
   public static void Render(
      SourceProductionContext context,
      string assemblyName,
      MaybeSpec<TypeSafeIdSpec> maybeSpec)
   {
      context.DispatchDiagnostics(Diagnostics, maybeSpec);
      if (!maybeSpec.HasValue)
      {
         return;
      }
      
      var ct = context.CancellationToken;
      ct.ThrowIfCancellationRequested();

      var renderer = new TypeIdRenderer(context)
      {
         Spec = maybeSpec.Value
      };
      
      renderer.Render(maybeSpec.Value.NamedTargetArchetype.Symbol.GeneratedFilePath);
   }
}