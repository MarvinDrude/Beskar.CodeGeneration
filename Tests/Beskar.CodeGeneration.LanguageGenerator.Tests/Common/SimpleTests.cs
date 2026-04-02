using System.Threading.Tasks;
using Beskar.CodeGeneration.LanguageGenerator.Marker.Attributes;
using Beskar.CodeGeneration.LanguageGenerator.Tests.Constants;

namespace Beskar.CodeGeneration.LanguageGenerator.Tests.Common;

public class SimpleTests
{
   [Test]
   public async Task SimpleEnum()
   {
      var result = Compilations.Create()
         .AddTestScenario("Scenarios/Simple")
         .WithReferenceByType<TranslationGroupAttribute>()
         .AddSourceGenerator(new LanguageGenerator())
         .Create();

      var debugReport = result.GetDebugReport();
      var compilation = result.Compilation;

      await Assert.That(result.Diagnostics).IsEmpty();
      await Assert.That(result.GeneratedDiagnostics).IsEmpty();
   }
}