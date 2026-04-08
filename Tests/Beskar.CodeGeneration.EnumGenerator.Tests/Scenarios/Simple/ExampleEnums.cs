using Beskar.CodeGeneration.EnumGenerator.Marker.Attributes;

namespace Beskar.CodeGeneration.EnumGenerator.Tests.Scenarios.Simple;

[FastEnum]
public enum TestEnum : long
{
   First = 1,
   Second = 2,
   Third,
}

[FastEnum]
public enum ByteEnum : byte
{
   First = 1,
   Example
}