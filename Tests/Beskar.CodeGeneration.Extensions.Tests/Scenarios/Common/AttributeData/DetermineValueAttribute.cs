using System;

namespace Beskar.CodeGeneration.Extensions.Tests.Scenarios.Common.AttributeData;

[AttributeUsage(AttributeTargets.Class)]
public sealed class DetermineValueAttribute(
   string stringValue = "Test",
   bool boolValue = false,
   Type? typeValue = null,
   int intValue = 1,
   char charValue = 'a',
   EnumTest enumValue = EnumTest.Test,
   byte byteValue = 1,
   short shortValue = 1,
   long longValue = 1,
   float floatValue = 1.0f,
   double doubleValue = 1.0d,
   decimal decimalValue = 1.0m,
   uint uintValue = 1,
   ulong ulongValue = 1) 
   : Attribute
{
   public string StringValue { get; init; } = stringValue;
   
   public bool BoolValue { get; init; } = boolValue;
   
   public Type? TypeValue { get; init; } = typeValue;
   
   
}

public enum EnumTest
{
   Test = 10,
   Test2 = 20,
}