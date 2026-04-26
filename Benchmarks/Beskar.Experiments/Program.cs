using System.Diagnostics;
using System.Diagnostics.Metrics;
using Beskar.CodeGeneration.EnumGenerator.Marker.Attributes;
using Beskar.CodeGeneration.LanguageGenerator.Marker.Attributes;
using Beskar.CodeGeneration.LanguageGenerator.Marker.Detectors;
using Beskar.CodeGeneration.LanguageGenerator.Marker.Interfaces;
using Beskar.CodeGeneration.LanguageGenerator.Marker.Providers;
using Beskar.CodeGeneration.ObserveGenerator.Marker.Attributes;
using Beskar.CodeGeneration.ObserveGenerator.Marker.Enums;
using Beskar.CodeGeneration.PacketGenerator.Marker.Attributes;
using Beskar.CodeGeneration.PacketGenerator.Marker.Common;
using Beskar.CodeGeneration.PacketGenerator.Marker.Interfaces;
using Beskar.CodeGeneration.PacketGenerator.Marker.Internal;
using Beskar.CodeGeneration.PacketGenerator.Marker.Models;
using Beskar.CodeGeneration.ProcessorGenerator.Marker.Attributes;
using Beskar.CodeGeneration.ProcessorGenerator.Marker.Interfaces;
using Beskar.CodeGeneration.ProcessorGenerator.Marker.Models;
using Beskar.Languages;
using BeskarExperiments.ObserveExtensions;
using Me.Memory.Results;
using Microsoft.Extensions.DependencyInjection;
using TestTest;

Console.WriteLine("Hello World!");

// var serviceCollection = new ServiceCollection()
//    .AddSingleton<ILanguageDetector, SystemCultureDetector>() // can add multiple detectors of varying priority
//    .AddSingleton<ITranslationProvider, JsonTranslationProvider>() // example implementation of json files as base (can be DB too)
//    .AddSingleton<TranslationFacade>();
//    
// var serviceProvider = serviceCollection.BuildServiceProvider();
//    
// // Json Provider example - can create own providers like DB or Third party api
// var provider = (JsonTranslationProvider)serviceProvider.GetRequiredService<ITranslationProvider>();
// provider.Initialize("Translations"); // folder path
// await provider.PopulateCache(CancellationToken.None); // can be called again if file changed at runtime
//
// var translation = serviceProvider.GetRequiredService<TranslationFacade>();
// Console.WriteLine(translation.TestGroup.Test); // calls the translation provider in the background
// Console.WriteLine(translation.RegisterGroup.Description);
//
// Console.WriteLine(string.Join(",", ObserveRegistry.GeneratedMeterNames));
// Console.WriteLine(string.Join(",", ObserveRegistry.GeneratedSourceNames));
//
// var test = new Test<object, object>();
// test.TestMethod();

var start = Stopwatch.GetTimestamp();



Console.WriteLine("Done: " + new TimeSpan(Stopwatch.GetTimestamp() - start));

var registry = new ExampleRegistry();
registry.RegisterHandler<TestPacket>((ref state, ref packet, ct) =>
{
   Console.WriteLine("Test");
   return ValueTask.CompletedTask;
});

var registryState = new Example2Registry();
registryState.RegisterHandler<StructPacket>((ref state, ref packet, ct) =>
{
   Console.WriteLine("TestState: " + state.Number);
   return ValueTask.CompletedTask;
});

var packet = new TestPacket();
packet.Number = 2;

var bytes = registry.SerializeWithHeader(packet);
var ob = new object();
await registry.RoutePacket(ref ob, bytes);

var state = new ClientState()
{
   Number = 1337
};
await registryState.RoutePacket(ref state, bytes);

var serviceCollection = new ServiceCollection();
serviceCollection.AddScoped<MainProcessor>()
   .AddScoped<AsyncProcessor>()
   .AddScoped<ValueProcessor>()
   .AddScoped(typeof(LogProcessor<>));

serviceCollection.AddScoped(MainPipeline.GetFactory);

var provider = serviceCollection.BuildServiceProvider();
var pipeline = provider.GetRequiredService<MainPipeline>();

var result = await pipeline.Execute(123, CancellationToken.None);

var count = Test.ValueCount;
var names = Test.GetAllNames();
var vals = Test.GetAllValues();

var res = Test.TryFastParseExact("Test2", out var testExact);
var tes1 = Test.TryFastParseCase("test2", out var testCase);

var a = testCase.ToFastString();
var b = Test.Test.ToFastString();

return;

[FastEnum]
public enum Test
{
   Test,
   Test2,
}

[Processor]
internal sealed class MainProcessor : ISyncProcessor<string, int>
{
   public Result<int, ProcessorError> Execute(
      ProcessorContext context, string input, CancellationToken cancellationToken)
   {
      return int.TryParse(input, out var result)
         ? result
         : new ProcessorError("Invalid integer input");
   }
}

[Processor]
internal sealed class AsyncProcessor(IServiceProvider provider) 
   : IAsyncProcessor<string, string>, IAsyncPostProcessor
{
   private readonly IServiceProvider _serviceProvider = provider;
   
   [Setting(nameof(Delay))]
   public int Delay { get; set; }
   
   [Setting(nameof(PostMessage))]
   public string PostMessage { get; set; } = "Default";
   
   public async Task<Result<string, ProcessorError>> Execute(
      ProcessorContext context, string input, CancellationToken cancellationToken)
   {
      await Task.Delay(Delay, cancellationToken);
      return input.ToUpper();
   }

   public Task<ProcessorError?> Post(ProcessorContext context, CancellationToken cancellationToken)
   {
      Console.WriteLine($"Async Post: {context.Elapsed}");
      return Task.FromResult<ProcessorError?>(new ProcessorError("Error :/"));
   }
}

[Processor]
internal sealed class ValueProcessor : IValueAsyncProcessor<int, string>
{
   [Setting(nameof(ArraySetting))]
   public string[] ArraySetting { get; set; } = [];
   
   public ValueTask<Result<string, ProcessorError>> Execute(
      ProcessorContext context, int input, CancellationToken cancellationToken)
   {
      return new ValueTask<Result<string, ProcessorError>>(input.ToString());
   }
}

[Processor]
internal sealed class LogProcessor<TIn> : IValueAsyncProcessor<TIn, TIn>, IValueAsyncPostProcessor
{
   public async ValueTask<Result<TIn, ProcessorError>> Execute(
      ProcessorContext context, TIn input, CancellationToken cancellationToken)
   {
      Console.WriteLine($"Starting Pipeline: {context.PipelineName}");
      return input;
   }
   
   public ValueTask<ProcessorError?> Post(
      ProcessorContext context, CancellationToken cancellationToken)
   {
      Console.WriteLine($"Ending Pipeline: {context.PipelineName}, took {context.Elapsed}");
      return ValueTask.FromResult<ProcessorError?>(null);
   }
}

internal class LoggablePipeline
{
   [Step(0)]
   public required LogProcessor<int> Log { get; set; }
}

[Timeout(500)]
[ContextVariable<string>("MyString")]
[ContextVariable<int>("MyInt")]
[ContextVariable<object>("MyObject")]
internal class BasePipeline<TValueProcessor> : LoggablePipeline
{
   [Step(1)]
   [Setting(nameof(ValueProcessor.ArraySetting), new [] { "Test1", "Test2" })]
   public required TValueProcessor First { get; set; }
   
   [Step(2)]
   [Setting(nameof(AsyncProcessor.Delay), 1_000)]
   [Setting(nameof(AsyncProcessor.PostMessage), "Setting Post Message")]
   public required AsyncProcessor Second { get; set; }
}

[ProcessorPipeline("Main")]
[Timeout(5000)]
[ContextVariable<bool>("MyBool")]
internal sealed partial class MainPipeline : BasePipeline<ValueProcessor>
{
   [Step(3)]
   public required MainProcessor Third { get; set; }
}

[PacketRegistry]
public sealed partial class ExampleRegistry : BaseJsonPacketRegistry<object>;

namespace TestTest
{
   public sealed class ClientState
   {
      public int Number { get; set; }
   }
   
   [PacketRegistry<ClientState>]
   public sealed partial class Example2Registry : BaseJsonPacketRegistry<ClientState>;
   
   [Packet(typeof(Example2Registry), typeof(ExampleRegistry))]
   public class TestPacket : IPacket
   {
      public int Number { get; set; }
   }

   [Packet(typeof(ExampleRegistry), typeof(Example2Registry))]
   public struct StructPacket : IPacket
   {
      
   }
}

// [TranslationGroup]
// public enum TestGroup
// {
//    [TranslationKey]
//    Test = 1,
//    [TranslationKey(defaultValue: "Test2-Default")]
//    Test2 = 2,
// }
//
// [TranslationGroup]
// public enum RegisterGroup
// {
//    [TranslationKey]
//    Title = 1,
//    [TranslationKey]
//    Description = 2,
// }
//
// [Observe]
// [ObserveActivity("Test.Activities", "1.0.0")]
// [ObserveMeter("Test.Meters", "1.0.0")]
// [ObserveInstrument("Counter", InstrumentKind.Counter, typeof(int))]
// [ObserveInstrument("Histogram", InstrumentKind.Histogram, typeof(double))]
// [ObserveInstrument("Gauge", InstrumentKind.Gauge, typeof(int))]
// [ObserveInstrument("TestTest", InstrumentKind.UpDownCounter, typeof(int))]
// public partial class Test<T1, T2>
//    where T1 : class
// {
//    public void TestMethod()
//    {
//       using var activity = ActivitySource.StartActivity();
//       
//       Counter.Add(1);
//       Histogram.Record(20d);
//    }
// }
//
// [Observe]
// [ObserveActivity]
// [ObserveMeter]
// [ObserveInstrument("Counter", InstrumentKind.Counter, typeof(int))]
// [ObserveInstrument("Histogram", InstrumentKind.Histogram, typeof(double))]
// [ObserveInstrument("Gauge", InstrumentKind.Gauge, typeof(int))]
// [ObserveInstrument("TestTest", InstrumentKind.UpDownCounter, typeof(int))]
// public partial class TestTwo<T>
// {
//    public void TestMethod()
//    {
//       using var activity = ActivitySource.StartActivity();
//       
//       Counter.Add(1);
//       Histogram.Record(20d);
//    }
// }