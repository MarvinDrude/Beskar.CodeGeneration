using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;

namespace Beskar.Benchmark.Common.Config;

public class WarmupDetailedConfig : DetailedConfig
{
   public WarmupDetailedConfig()
   {
      AddJob(Job.Default
         .WithWarmupCount(5)
         .WithIterationCount(10)
         .WithInvocationCount(16 * 2)
         .WithStrategy(RunStrategy.Throughput));
   }
}