using Beskar.CodeGeneration.Extensions.Common.Symbols;
using Beskar.CodeGeneration.Extensions.Diagnostics;
using Beskar.CodeGeneration.Extensions.Models.Diagnostics;
using Beskar.CodeGeneration.Extensions.Models.Symbols;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes.Options;
using Beskar.CodeGeneration.ObserveGenerator.Models;
using Me.Memory.Collections;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.ObserveGenerator;

public sealed partial class ObserveGenerator
{
   private static MaybeSpec<ObserveSpec> Transform(
      GeneratorAttributeSyntaxContext context,
      CancellationToken ct)
   {
      ct.ThrowIfCancellationRequested();

      var symbol = (INamedTypeSymbol)context.TargetSymbol;
      var attributes = symbol.GetAttributes();

      var metricSpec = GetMeterAttribute(symbol, attributes);
      var activitySpec = GetActivityAttribute(symbol, attributes);
      var instrumentSpecs = GetInstrumentAttributes(symbol, attributes);
      
      ct.ThrowIfCancellationRequested();

      if (metricSpec is null && activitySpec is null)
      {
         return DiagnosticBuilder<ObserveSpec>.CreateSingle(InvalidTargetDiagnosticId);
      }

      using var builder = DiagnosticBuilder<ObserveSpec>.Create(8);
      var namedInfo = symbol.CreateNamedArchetype(_transformOptions);
      
      return builder.Build(new ObserveSpec(
         namedInfo,
         activitySpec,
         metricSpec,
         new SequenceArray<ObserveInstrumentSpec>(instrumentSpecs)));
   }
   
   private static readonly ArchetypeTransformOptions _transformOptions = CreateTransformOptions();
   private static ArchetypeTransformOptions CreateTransformOptions()
   {
      var options = new ArchetypeTransformOptions
      {
         NamedTypes =
         {
            Load = new NamedTypeSymbolLoadFlags()
            {
               TypeParameters = true
            }
         }
      };

      return options;
   }
}