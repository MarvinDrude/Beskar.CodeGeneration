using System.Threading.Tasks;
using Beskar.CodeGeneration.ObserveGenerator.Marker.Attributes;
using Beskar.CodeGeneration.ObserveGenerator.Tests.Constants;

namespace Beskar.CodeGeneration.ObserveGenerator.Tests.Common;

public sealed class SimpleTests
{
   [Test]
   public async Task ObserveSimple()
   {
      var result = Compilations.Create()
         .AddTestScenario("Scenarios/Simple")
         .WithReferenceByType<ObserveAttribute>()
         .AddSourceGenerator(new ObserveGenerator())
         .Create();
      
      var debugReport = result.GetDebugReport();
      var compilation = result.Compilation;

      await Assert.That(result.Diagnostics).IsEmpty();
      await Assert.That(result.GeneratedDiagnostics).IsEmpty();
   }
}