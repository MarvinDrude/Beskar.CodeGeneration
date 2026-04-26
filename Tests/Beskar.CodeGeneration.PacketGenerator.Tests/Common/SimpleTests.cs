using System.Threading.Tasks;
using Beskar.CodeGeneration.PacketGenerator.Marker.Attributes;
using Beskar.CodeGeneration.PacketGenerator.Tests.Constants;
using Me.Memory.Buffers;

namespace Beskar.CodeGeneration.PacketGenerator.Tests.Common;

public sealed class SimpleTests
{
   [Test]
   public async Task RegistryTwoTest()
   {
      var result = Compilations.Create()
         .AddTestScenario("Scenarios/Simple")
         .WithReferenceByType<PacketAttribute>()
         .WithReferenceByType<ArrayBuilder<object>>()
         .AddSourceGenerator(new PacketGenerator())
         .Create();
      
      var debugReport = result.GetDebugReport();
      var compilation = result.Compilation;

      await Assert.That(result.Diagnostics).Count().IsEqualTo(2);
      await Assert.That(result.GeneratedDiagnostics).IsEmpty();
      await Assert.That(result.GeneratedSyntaxTrees).Count().IsEqualTo(4);
   }
   [Test]
   public async Task StatefulRegistryTest()
   {
      var result = Compilations.Create()
         .AddTestScenario("Scenarios/Stateful")
         .WithReferenceByType<PacketAttribute>()
         .WithReferenceByType<ArrayBuilder<object>>()
         .AddSourceGenerator(new PacketGenerator())
         .Create();
      
      var debugReport = result.GetDebugReport();
      var compilation = result.Compilation;

      await Assert.That(result.Diagnostics).Count().IsEqualTo(1);
      await Assert.That(result.GeneratedDiagnostics).IsEmpty();
      await Assert.That(result.GeneratedSyntaxTrees).Count().IsEqualTo(3);
   }
}