namespace Beskar.CodeGeneration.Extensions.Tests.Scenarios.Common.AttributeData;

[DetermineValue(
   "Positional", 
   true, 
   typeof(string), 
   42, 
   'z', 
   EnumTest.Test, 
   2, 
   10, 
   1000L, 
   3.14f, 
   2.718d, 
   500u, 
   1000000ul)]
public sealed class DetermineTargetAllParametersSet;

[DetermineValue(
   StringValue = "NamedProperty",
   BoolValue = true,
   TypeValue = typeof(int),
   IntValue = 99,
   CharValue = 'b',
   EnumValue = EnumTest.Test,
   ByteValue = 255,
   ShortValue = -1,
   LongValue = 999999999L,
   FloatValue = 1.1f,
   DoubleValue = 2.2d,
   UintValue = 10u,
   UlongValue = 20ul)]
public sealed class DetermineTargetNamedProperties;

[DetermineValue(
   "Mixed",
   false,
   typeof(double),
   IntValue = 7,
   CharValue = 'x',
   EnumValue = EnumTest.Test,
   ByteValue = 4,
   ShortValue = 8,
   LongValue = 12L,
   FloatValue = 0.5f,
   DoubleValue = 0.25d,
   UintValue = 100u,
   UlongValue = 200ul)]
public sealed class DetermineTargetMixedParameters;

