using System;
using Beskar.CodeGeneration.Extensions.Tests.Scenarios.Common.TypedConstant;

namespace Beskar.CodeGeneration.Extensions.Tests.Scenarios.Common.TypedConstantArray;

[AttributeUsage(AttributeTargets.Class)]
public sealed class AllArrayValueAttribute(
   string[] strings,
   bool[] bools,
   int[] ints,
   long[] longs,
   float[] floats,
   double[] doubles,
   char[] chars,
   Type[] types,
   EnumTarget[] enums) 
   : Attribute;