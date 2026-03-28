namespace Beskar.CodeGeneration.Extensions.Tests.Scenarios.Common.TypedConstant;

[AllValue("Test", true, 20, 30L, 40f, 50d, 'b', typeof(string), EnumTarget.Hello)]
public sealed class TestAllValueTarget;

public enum EnumTarget
{
   Hello = 20,
}