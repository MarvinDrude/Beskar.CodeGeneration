namespace Beskar.CodeGeneration.Extensions.Tests.Scenarios.Common.AttributeData;

[DetermineValues(
   ["A", "B"],
   [true, false],
   [10, 20],
   [100L, 200L],
   [1.1f, 2.2f],
   [3.3d, 4.4d],
   ['x', 'y'],
   [typeof(string), typeof(int)],
   [EnumTest.Test, EnumTest.Test2])]
public sealed class DetermineTargetArraysPositional;

[DetermineValues(
   StringValues = ["C", "D"],
   IntValues = [99, 100])]
public sealed class DetermineTargetArraysNamed;