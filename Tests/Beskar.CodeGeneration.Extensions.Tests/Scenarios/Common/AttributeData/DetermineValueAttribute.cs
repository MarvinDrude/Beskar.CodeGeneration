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
   uint uintValue = 1,
   ulong ulongValue = 1) 
   : Attribute
{
   public string StringValue { get; init; } = stringValue;
   
   public bool BoolValue { get; init; } = boolValue;
   
   public Type? TypeValue { get; init; } = typeValue;
   
   public int IntValue { get; init; } = intValue;

   public char CharValue { get; init; } = charValue;

   public EnumTest EnumValue { get; init; } = enumValue;

   public byte ByteValue { get; init; } = byteValue;

   public short ShortValue { get; init; } = shortValue;

   public long LongValue { get; init; } = longValue;

   public float FloatValue { get; init; } = floatValue;

   public double DoubleValue { get; init; } = doubleValue;

   public uint UintValue { get; init; } = uintValue;

   public ulong UlongValue { get; init; } = ulongValue;
}

public enum EnumTest
{
   Test = 10,
   Test2 = 20,
}