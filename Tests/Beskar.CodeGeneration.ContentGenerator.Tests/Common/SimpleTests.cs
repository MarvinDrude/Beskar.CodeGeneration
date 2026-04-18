using System.Threading.Tasks;
using Beskar.CodeGeneration.ContentGenerator.Marker.Attributes;
using Beskar.CodeGeneration.ContentGenerator.Tests.Constants;
using Me.Memory.Buffers;
using Microsoft.EntityFrameworkCore;

namespace Beskar.CodeGeneration.ContentGenerator.Tests.Common;

public sealed class SimpleTests
{
   [Test]
   public async Task RegistryTwoTest()
   {
      var result = Compilations.Create()
         .AddTestScenario("Scenarios/Simple")
         .WithReferenceByType<ContentTypeAttribute>()
         .WithReferenceByType<IEntityTypeConfiguration<object>>()
         .WithReferenceByType<ArrayBuilder<object>>()
         .AddSourceGenerator(new ContentGenerator())
         .Create();
      
      var debugReport = result.GetDebugReport();
      var compilation = result.Compilation;

      await Assert.That(result.Diagnostics).IsEmpty();
      await Assert.That(result.GeneratedDiagnostics).IsEmpty();
   }
}