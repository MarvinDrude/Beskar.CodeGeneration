using System.Threading.Tasks;
using Beskar.CodeGeneration.Extensions.Common;
using Beskar.CodeGeneration.Extensions.Tests.Constants;

namespace Beskar.CodeGeneration.Extensions.Tests.Common;

public sealed class AttributeDataTests
{
   [Test]
   public async Task DetermineTargetAllParametersSet()
   {
      var result = Compilations.Create()
         .AddTestScenario("Scenarios/Common/AttributeData")
         .Create();
      var compilation = result.Compilation;

      await Assert.That(result.Diagnostics).IsEmpty();
      await Assert.That(result.GeneratedDiagnostics).IsEmpty();

      var classSymbol = compilation.GetTypeByMetadataName(
         "Beskar.CodeGeneration.Extensions.Tests.Scenarios.Common.AttributeData.DetermineTargetAllParametersSet");
      await Assert.That(classSymbol).IsNotNull();

      var attribute = classSymbol.GetAttributes()[0];
      
      await Assert.That(attribute.DetermineStringValue("StringValue", 0)).IsEqualTo("Positional");
      await Assert.That(attribute.DetermineBoolValue("BoolValue", 1)).IsTrue();
      await Assert.That(attribute.DetermineTypeValue("TypeValue", 2)).IsNotNull();
      await Assert.That(attribute.DetermineIntValue("IntValue", 3)).IsEqualTo(42);
      await Assert.That(attribute.DetermineCharValue("CharValue", 4)).IsEqualTo('z');
      await Assert.That(attribute.DetermineEnumFullName("EnumValue", 5))
         .IsEqualTo("Beskar.CodeGeneration.Extensions.Tests.Scenarios.Common.AttributeData.EnumTest.Test");
      
      await Assert.That(attribute.DetermineByteValue("ByteValue", 6)).IsEqualTo((byte)2);
      await Assert.That(attribute.DetermineShortValue("ShortValue", 7)).IsEqualTo((short)10);
      await Assert.That(attribute.DetermineLongValue("LongValue", 8)).IsEqualTo(1000L);
      await Assert.That(attribute.DetermineFloatValue("FloatValue", 9)).IsEqualTo(3.14f);
      await Assert.That(attribute.DetermineDoubleValue("DoubleValue", 10)).IsEqualTo(2.718d);
      await Assert.That(attribute.DetermineUIntValue("UintValue", 11)).IsEqualTo(500u);
      await Assert.That(attribute.DetermineULongValue("UlongValue", 12)).IsEqualTo(1000000ul);
   }
   
   [Test]
   public async Task DetermineTargetNamedProperties()
   {
      var result = Compilations.Create()
         .AddTestScenario("Scenarios/Common/AttributeData")
         .Create();
      var compilation = result.Compilation;

      await Assert.That(result.Diagnostics).IsEmpty();
      
      var classSymbol = compilation.GetTypeByMetadataName(
         "Beskar.CodeGeneration.Extensions.Tests.Scenarios.Common.AttributeData.DetermineTargetNamedProperties");
      await Assert.That(classSymbol).IsNotNull();

      var attribute = classSymbol!.GetAttributes()[0];

      await Assert.That(attribute.DetermineStringValue("StringValue", 0)).IsEqualTo("NamedProperty");
      await Assert.That(attribute.DetermineBoolValue("BoolValue", 1)).IsTrue();
      await Assert.That(attribute.DetermineTypeValue("TypeValue", 2)).IsNotNull();
      await Assert.That(attribute.DetermineIntValue("IntValue", 3)).IsEqualTo(99);
      await Assert.That(attribute.DetermineCharValue("CharValue", 4)).IsEqualTo('b');
      await Assert.That(attribute.DetermineEnumFullName("EnumValue", 5))
         .IsEqualTo("Beskar.CodeGeneration.Extensions.Tests.Scenarios.Common.AttributeData.EnumTest.Test");
      
      await Assert.That(attribute.DetermineByteValue("ByteValue", 6)).IsEqualTo((byte)255);
      await Assert.That(attribute.DetermineShortValue("ShortValue", 7)).IsEqualTo((short)-1);
      await Assert.That(attribute.DetermineLongValue("LongValue", 8)).IsEqualTo(999999999L);
      await Assert.That(attribute.DetermineFloatValue("FloatValue", 9)).IsEqualTo(1.1f);
      await Assert.That(attribute.DetermineDoubleValue("DoubleValue", 10)).IsEqualTo(2.2d);
      await Assert.That(attribute.DetermineUIntValue("UintValue", 11)).IsEqualTo(10u);
      await Assert.That(attribute.DetermineULongValue("UlongValue", 12)).IsEqualTo(20ul);
   }

   [Test]
   public async Task DetermineTargetMixedParameters()
   {
      var result = Compilations.Create()
         .AddTestScenario("Scenarios/Common/AttributeData")
         .Create();
      var compilation = result.Compilation;

      await Assert.That(result.Diagnostics).IsEmpty();

      var classSymbol = compilation.GetTypeByMetadataName(
         "Beskar.CodeGeneration.Extensions.Tests.Scenarios.Common.AttributeData.DetermineTargetMixedParameters");
      await Assert.That(classSymbol).IsNotNull();

      var attribute = classSymbol!.GetAttributes()[0];

      await Assert.That(attribute.DetermineStringValue("StringValue", 0)).IsEqualTo("Mixed");
      await Assert.That(attribute.DetermineBoolValue("BoolValue", 1)).IsFalse();
      await Assert.That(attribute.DetermineTypeValue("TypeValue", 2)).IsNotNull();
      
      await Assert.That(attribute.DetermineIntValue("IntValue", 3)).IsEqualTo(7);
      await Assert.That(attribute.DetermineCharValue("CharValue", 4)).IsEqualTo('x');
      await Assert.That(attribute.DetermineEnumFullName("EnumValue", 5))
         .IsEqualTo("Beskar.CodeGeneration.Extensions.Tests.Scenarios.Common.AttributeData.EnumTest.Test");
      
      await Assert.That(attribute.DetermineByteValue("ByteValue", 6)).IsEqualTo((byte)4);
      await Assert.That(attribute.DetermineShortValue("ShortValue", 7)).IsEqualTo((short)8);
      await Assert.That(attribute.DetermineLongValue("LongValue", 8)).IsEqualTo(12L);
      await Assert.That(attribute.DetermineFloatValue("FloatValue", 9)).IsEqualTo(0.5f);
      await Assert.That(attribute.DetermineDoubleValue("DoubleValue", 10)).IsEqualTo(0.25d);
      await Assert.That(attribute.DetermineUIntValue("UintValue", 11)).IsEqualTo(100u);
      await Assert.That(attribute.DetermineULongValue("UlongValue", 12)).IsEqualTo(200ul);
   }
   
   [Test]
   public async Task DetermineTargetArraysPositional()
   {
      var result = Compilations.Create()
         .AddTestScenario("Scenarios/Common/AttributeData")
         .Create();
      var compilation = result.Compilation;

      var classSymbol = compilation.GetTypeByMetadataName(
         "Beskar.CodeGeneration.Extensions.Tests.Scenarios.Common.AttributeData.DetermineTargetArraysPositional");
      await Assert.That(classSymbol).IsNotNull();

      var attribute = classSymbol.GetAttributes()[0];

      string?[] expectedStrings = ["A", "B"];
      await Assert.That(attribute.DetermineStringArrayValues("StringValues", 0)).IsEquivalentTo(expectedStrings);
      await Assert.That(attribute.DetermineBoolArrayValues("BoolValues", 1)).IsEquivalentTo([true, false]);
      await Assert.That(attribute.DetermineIntArrayValues("IntValues", 2)).IsEquivalentTo([10, 20]);
      await Assert.That(attribute.DetermineLongArrayValues("LongValues", 3)).IsEquivalentTo([100L, 200L]);
      await Assert.That(attribute.DetermineFloatArrayValues("FloatValues", 4)).IsEquivalentTo([1.1f, 2.2f]);
      await Assert.That(attribute.DetermineDoubleArrayValues("DoubleValues", 5)).IsEquivalentTo([3.3d, 4.4d]);
      await Assert.That(attribute.DetermineCharArrayValues("CharValues", 6)).IsEquivalentTo(['x', 'y']);

      var typeValues = attribute.DetermineTypeArrayValues("TypeValues", 7);
      await Assert.That(typeValues).Count().IsEqualTo(2);
      
      var enumValues = attribute.DetermineEnumFullNameArrayValues("EnumValues", 8);
      var expectedEnumBase = "Beskar.CodeGeneration.Extensions.Tests.Scenarios.Common.AttributeData.EnumTest";

      string?[] enumEquals = [$"{expectedEnumBase}.Test", $"{expectedEnumBase}.Test2"];
      await Assert.That(enumValues).IsEquivalentTo(enumEquals);
   }

   [Test]
   public async Task DetermineTargetArraysNamed()
   {
      var result = Compilations.Create()
         .AddTestScenario("Scenarios/Common/AttributeData")
         .Create();
      var compilation = result.Compilation;

      var classSymbol = compilation.GetTypeByMetadataName(
         "Beskar.CodeGeneration.Extensions.Tests.Scenarios.Common.AttributeData.DetermineTargetArraysNamed");
      await Assert.That(classSymbol).IsNotNull();

      var attribute = classSymbol.GetAttributes()[0];

      string?[] expectedStrings = ["C", "D"];
      await Assert.That(attribute.DetermineStringArrayValues("StringValues", 0)).IsEquivalentTo(expectedStrings);
      await Assert.That(attribute.DetermineIntArrayValues("IntValues", 2)).IsEquivalentTo([99, 100]);
      
      var bools = attribute.DetermineBoolArrayValues("BoolValues", 1);
      await Assert.That(bools).Count().IsEqualTo(0);
   }
}