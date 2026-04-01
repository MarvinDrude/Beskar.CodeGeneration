using System.Threading.Tasks;
using Beskar.CodeGeneration.Extensions.Common.Symbols;
using Beskar.CodeGeneration.Extensions.Interfaces.Specs;
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

      _fullOptions.RegisterAttribute("global::Beskar.CodeGeneration.Extensions.Tests.Scenarios.Common.Archetypes.ExampleAttribute", 
         data => new TestAttributeSpec() { Name = data.AttributeClass?.Name ?? "" });
      
      var a = classSymbol.CreateNamedArchetype(_fullOptions);
      var b = classSymbol.CreateNamedArchetype(_fullOptions);
      
      await Assert.That(a).IsEqualTo(b);
   }

   private readonly ArchetypeTransformOptions _fullOptions = new ()
   {
      Methods = MethodTransformOptions.Full.WithDepth(5),
      Fields = FieldTransformOptions.Full.WithDepth(5),
      Properties = PropertyTransformOptions.Full.WithDepth(5),
      TypeParameters = TypeParameterTransformOptions.Full.WithDepth(5),
      Types = TypeTransformOptions.Full.WithDepth(5),
      NamedTypes = NamedTypeTransformOptions.Full.WithDepth(5),
      Symbols = SymbolTransformOptions.Full.WithDepth(5),
      Parameters = ParameterTransformOptions.Full.WithDepth(5),
   };
}

sealed class TestAttributeSpec : IAttributeSpec
{
   public required string Name { get; init; }
   
   public bool Equals(IAttributeSpec? other)
   {
      return other is TestAttributeSpec spec && Name == spec.Name;
   }
}