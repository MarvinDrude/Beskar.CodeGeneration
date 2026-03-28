using System;

namespace Beskar.CodeGeneration.Extensions.Tests.Scenarios.Common.AttributeData;

[AttributeUsage(AttributeTargets.Class)]
public sealed class DetermineValuesAttribute(
   string[]? stringValues = null,
   bool[]? boolValues = null,
   int[]? intValues = null,
   long[]? longValues = null,
   float[]? floatValues = null,
   double[]? doubleValues = null,
   char[]? charValues = null,
   Type[]? typeValues = null,
   EnumTest[]? enumValues = null) 
   : Attribute
{
   public string[]? StringValues { get; init; } = stringValues;
   
   public bool[]? BoolValues { get; init; } = boolValues;
   
   public int[]? IntValues { get; init; } = intValues;
   
   public long[]? LongValues { get; init; } = longValues;
   
   public float[]? FloatValues { get; init; } = floatValues;
   
   public double[]? DoubleValues { get; init; } = doubleValues;
   
   public char[]? CharValues { get; init; } = charValues;
   
   public Type[]? TypeValues { get; init; } = typeValues;
   
   public EnumTest[]? EnumValues { get; init; } = enumValues;
}