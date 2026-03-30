using BenchmarkDotNet.Attributes;
using Beskar.Benchmark.Common.Config;

namespace Beskar.Language.Benchmarks.Common;

[Config(typeof(WarmupDetailedConfig))]
public class StructInterfaceExtensionBenchmark
{
   [Benchmark]
   public int[] TestNonGenericStructExt()
   {
      var values = new int[20];
      Span<TestStruct> testStructs = stackalloc TestStruct[20];

      for (var index = 0; index < testStructs.Length; index++)
      {
         var testStruct = testStructs[index];
         
         var value = testStruct.GetValue();
         values[index] = value;
      }

      return values;
   }

   [Benchmark]
   public int[] TestGenericStructExt()
   {
      var values = new int[20];
      Span<TestStruct> testStructs = stackalloc TestStruct[20];

      for (var index = 0; index < testStructs.Length; index++)
      {
         var testStruct = testStructs[index];
         
         var value = testStruct.GetValueGeneric();
         values[index] = value;
      }

      return values;
   }
}

internal struct TestStruct : ITestInterface
{
   public int Value;
   public int ValueProperty => Value;
}

internal interface ITestInterface
{
   int ValueProperty { get; }
}

internal static class TestStructExtensions
{
   extension(ITestInterface testStruct)
   {
      public int GetValue() => testStruct.ValueProperty;
   }

   extension<T>(T testStruct)
      where T : ITestInterface, allows ref struct
   {
      public int GetValueGeneric() => testStruct.ValueProperty;
   }
}