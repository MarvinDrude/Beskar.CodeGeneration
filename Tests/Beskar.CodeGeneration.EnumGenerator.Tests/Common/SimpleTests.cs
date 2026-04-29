using System.Threading.Tasks;
using Beskar.CodeGeneration.EnumGenerator.Marker.Attributes;
using Beskar.CodeGeneration.EnumGenerator.Tests.Constants;
using Me.Memory.Buffers;

namespace Beskar.CodeGeneration.EnumGenerator.Tests.Common;

public sealed class SimpleTests
{
   [Test]
   public async Task RegistryTwoTest()
   {
      var result = Compilations.Create()
         .AddTestScenario("Scenarios/Simple")
         .WithReferenceByType<FastEnumAttribute>()
         .WithReferenceByType<ArrayBuilder<object>>()
         .AddSourceGenerator(new EnumGenerator())
         .Create();
      
      var debugReport = result.GetDebugReport();
      var compilation = result.Compilation;

      await Assert.That(result.Diagnostics).IsEmpty();
      await Assert.That(result.GeneratedDiagnostics).IsEmpty();
   }
}