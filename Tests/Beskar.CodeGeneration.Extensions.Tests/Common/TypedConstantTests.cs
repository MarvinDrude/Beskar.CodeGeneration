using System.Threading.Tasks;
using Beskar.CodeGeneration.Extensions.Common;
using Beskar.CodeGeneration.Extensions.Tests.Constants;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Tests.Common;

public sealed class TypedConstantTests
{
   [Test]
   public async Task ReadTypedConstantValue()
   {
      var result = Compilations.Create()
         .AddTestScenario("Scenarios/Common/TypedConstant")
         .Create();
      var compilation = result.Compilation;

      await Assert.That(result.Diagnostics).IsEmpty();
      await Assert.That(result.GeneratedDiagnostics).IsEmpty();

      var classSymbol = compilation.GetTypeByMetadataName(
         "Beskar.CodeGeneration.Extensions.Tests.Scenarios.Common.TypedConstant.TestAllValueTarget");
      await Assert.That(classSymbol).IsNotNull();

      var attributes = classSymbol.GetAttributes();
      await Assert.That(attributes).HasAtLeast(1);

      var args = attributes[0].ConstructorArguments;

      await Assert.That(args[0].StringValue).IsEqualTo("Test");
      await Assert.That(args[1].BoolValue).IsTrue();
      await Assert.That(args[2].IntValue).IsEqualTo(20);
      await Assert.That(args[3].LongValue).IsEqualTo(30L);
      await Assert.That(args[4].FloatValue).IsEqualTo(40f);
      await Assert.That(args[5].DoubleValue).IsEqualTo(50d);
      await Assert.That(args[6].CharValue).IsEqualTo('b');

      var expectedType = compilation.GetTypeByMetadataName("System.String");
      var actualType = args[7].TypeValue;

      await Assert.That(SymbolEqualityComparer.Default.Equals(actualType, expectedType)).IsTrue();
      
      await Assert.That(args[8].EnumFullNameValue).IsEqualTo(
         "Beskar.CodeGeneration.Extensions.Tests.Scenarios.Common.TypedConstant.EnumTarget.Hello");
      await Assert.That(args[8].EnumMember).IsNotNull();
   }
}