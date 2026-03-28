using Beskar.CodeGeneration.Extensions.Tests.Scenarios.Common.TypedConstant;

namespace Beskar.CodeGeneration.Extensions.Tests.Scenarios.Common.TypedConstantArray;

[AllArrayValue(
   ["Test1", "Test2"], 
   [true, false], 
   [20, 21], 
   [30L, 31L], 
   [40f, 41f], 
   [50d, 51d], 
   ['b', 'c'], 
   [typeof(string), typeof(int)], 
   [EnumTarget.Hello, EnumTarget.Hello])]
public sealed class TestAllValuesTarget;