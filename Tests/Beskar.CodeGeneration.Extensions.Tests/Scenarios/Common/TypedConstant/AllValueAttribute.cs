using System;

namespace Beskar.CodeGeneration.Extensions.Tests.Scenarios.Common.TypedConstant;

[AttributeUsage(AttributeTargets.Class)]
public sealed class AllValueAttribute(
   string str,
   bool b,
   int i,
   long l,
   float f,
   double d,
   char c,
   Type t,
   EnumTarget e) 
   : Attribute;