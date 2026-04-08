using Beskar.CodeGeneration.EnumGenerator.Models;
using Beskar.CodeGeneration.Extensions.Common.Symbols;
using Beskar.CodeGeneration.Extensions.Diagnostics;
using Beskar.CodeGeneration.Extensions.Models.Diagnostics;
using Beskar.CodeGeneration.Extensions.Models.Symbols;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes.Options;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.EnumGenerator;

public sealed partial class EnumGenerator
{
   private static MaybeSpec<FastEnumSpec> Transform(
      GeneratorAttributeSyntaxContext context,
      CancellationToken ct)
   {
      ct.ThrowIfCancellationRequested();

      var symbol = (INamedTypeSymbol)context.TargetSymbol;
      var attributes = symbol.GetAttributes();

      if (GetFastEnumAttribute(attributes) is not { } attribute)
      {
         return DiagnosticBuilder<FastEnumSpec>.CreateEmpty();
      }
      
      ct.ThrowIfCancellationRequested();
      using var builder = DiagnosticBuilder<FastEnumSpec>.Create(8);

      var namedType = symbol.CreateNamedArchetype(CreateOptions());
      return builder.Build(new FastEnumSpec(namedType));
   }

   private static ArchetypeTransformOptions CreateOptions()
   {
      var opts = new ArchetypeTransformOptions()
      {
         NamedTypes =
         {
            Depth = 2,
            FieldFilter = static (field) => field.HasConstantValue,
            Load = new NamedTypeSymbolLoadFlags()
            {
               Fields = true
            }
         }
      };

      return opts;
   }
}