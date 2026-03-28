using System;
using System.Threading.Tasks;
using Beskar.CodeGeneration.Extensions.Common;
using Beskar.CodeGeneration.Extensions.Tests.Constants;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Tests.Common;

public sealed class TypedConstantArrayTests
{
   [Test]
   public async Task ReadTypedConstantValues()
   {
      var result = Compilations.Create()
         .AddTestScenario("Scenarios/Common/TypedConstant")
         .AddTestScenario("Scenarios/Common/TypedConstantArray")
         .Create();
      var compilation = result.Compilation;

      await Assert.That(result.Diagnostics).IsEmpty();
      await Assert.That(result.GeneratedDiagnostics).IsEmpty();

      var classSymbol = compilation.GetTypeByMetadataName(
         "Beskar.CodeGeneration.Extensions.Tests.Scenarios.Common.TypedConstantArray.TestAllValuesTarget");
      await Assert.That(classSymbol).IsNotNull();

      var attribute = classSymbol.GetAttributes()[0];
      var args = attribute.ConstructorArguments;

      string?[] strExpected = ["Test1", "Test2"];
      await Assert.That(args[0].StringArrayValues).IsEquivalentTo(strExpected);
      await Assert.That(args[1].BoolArrayValues).IsEquivalentTo([true, false]);
      
      await Assert.That(args[2].IntArrayValues).IsEquivalentTo([20, 21]);
      await Assert.That(args[3].LongArrayValues).IsEquivalentTo([30L, 31L]);
      await Assert.That(args[4].FloatArrayValues).IsEquivalentTo([40f, 41f]);
      await Assert.That(args[5].DoubleArrayValues).IsEquivalentTo([50d, 51d]);
      
      await Assert.That(args[6].CharArrayValues).IsEquivalentTo(['b', 'c']);

      var typeValues = args[7].TypeArrayValues;
      await Assert.That(typeValues).Count().IsEqualTo(2);
      await Assert.That(SymbolEqualityComparer.Default.Equals(typeValues[0], compilation.GetTypeByMetadataName("System.String"))).IsTrue();
      await Assert.That(SymbolEqualityComparer.Default.Equals(typeValues[1], compilation.GetTypeByMetadataName("System.Int32"))).IsTrue();

      var expectedEnumName = "Beskar.CodeGeneration.Extensions.Tests.Scenarios.Common.TypedConstant.EnumTarget.Hello";
      
      string?[] enumExpected = [expectedEnumName, expectedEnumName];
      await Assert.That(args[8].EnumFullNameArrayValues).IsEquivalentTo(enumExpected);
      
      var enumFields = args[8].EnumFieldArrayValues;
      await Assert.That(enumFields).Count().IsEqualTo(2);
      await Assert.That(enumFields[0]).IsNotNull();
   }
}