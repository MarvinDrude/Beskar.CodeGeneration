using System.Threading.Tasks;
using Beskar.CodeGeneration.TypeIdGenerator.Marker.Attributes;
using Beskar.CodeGeneration.TypeIdGenerator.Tests.Constants;

namespace Beskar.CodeGeneration.TypeIdGenerator.Tests.Common;

public sealed class SimpleTests
{
   [Test]
   public async Task UnderlyingInt()
   {
      var result = Compilations.Create()
         .AddTestScenario("Scenarios/Simple")
         .WithReferenceByType<TypeSafeIdAttribute>()
         .AddSourceGenerator(new TypeIdGenerator())
         .Create();
      var compilation = result.Compilation;

      await Assert.That(result.Diagnostics).IsEmpty();
      await Assert.That(result.GeneratedDiagnostics).IsEmpty();

      
   }
}