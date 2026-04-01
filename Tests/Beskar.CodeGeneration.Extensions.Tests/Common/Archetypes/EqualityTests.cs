using System.Threading.Tasks;
using Beskar.CodeGeneration.Extensions.Common.Symbols;
using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;
using Beskar.CodeGeneration.Extensions.Tests.Constants;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes.Options;
using Beskar.CodeGeneration.Extensions.Transformers.Symbols.Options;

namespace Beskar.CodeGeneration.Extensions.Tests.Common.Archetypes;

public sealed class EqualityTests
{
   [Test]
   public async Task NamedTypeEquality()
   {
      var result = Compilations.Create()
         .AddTestScenario("Scenarios/Common/Archetypes")
         .Create();
      var compilation = result.Compilation;

      await Assert.That(result.Diagnostics).IsEmpty();
      await Assert.That(result.GeneratedDiagnostics).IsEmpty();

      var classSymbol = compilation.GetTypeByMetadataName(
         "Beskar.CodeGeneration.Extensions.Tests.Scenarios.Common.Archetypes.NamedTypeExample");
      await Assert.That(classSymbol).IsNotNull();
      if (classSymbol == null) return;
      
      var a = classSymbol.CreateNamedArchetype(_fullOptions);
      var b = classSymbol.CreateNamedArchetype(_fullOptions);
      
      await Assert.That(a).IsEqualTo(b);
   }

   private readonly ArchetypeTransformOptions _fullOptions = new ()
   {
      Methods = MethodTransformOptions.Full.WithDepth(int.MaxValue),
      Fields = FieldTransformOptions.Full.WithDepth(int.MaxValue),
      Properties = PropertyTransformOptions.Full.WithDepth(int.MaxValue),
      TypeParameters = TypeParameterTransformOptions.Full.WithDepth(int.MaxValue),
      Types = TypeTransformOptions.Full.WithDepth(int.MaxValue),
      NamedTypes = NamedTypeTransformOptions.Full.WithDepth(3),
      Symbols = SymbolTransformOptions.Full.WithDepth(int.MaxValue),
      Parameters = ParameterTransformOptions.Full.WithDepth(int.MaxValue),
   };
}