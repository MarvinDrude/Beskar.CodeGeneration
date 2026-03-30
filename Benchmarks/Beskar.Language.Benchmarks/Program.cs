

using BenchmarkDotNet.Running;
using Beskar.Language.Benchmarks.Common;

#if DEBUG 

Console.WriteLine("Hello, World!");

#else

BenchmarkRunner.Run<StructInterfaceExtensionBenchmark>();

#endif
