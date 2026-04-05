using System.Diagnostics;
using System.Diagnostics.Metrics;
using Beskar.CodeGeneration.LanguageGenerator.Marker.Attributes;
using Beskar.CodeGeneration.LanguageGenerator.Marker.Detectors;
using Beskar.CodeGeneration.LanguageGenerator.Marker.Interfaces;
using Beskar.CodeGeneration.LanguageGenerator.Marker.Providers;
using Beskar.CodeGeneration.ObserveGenerator.Marker.Attributes;
using Beskar.CodeGeneration.ObserveGenerator.Marker.Enums;
using Beskar.CodeGeneration.PacketGenerator.Marker.Common;
using Beskar.CodeGeneration.PacketGenerator.Marker.Internal;
using Beskar.CodeGeneration.PacketGenerator.Marker.Models;
using Beskar.Languages;
using BeskarExperiments.ObserveExtensions;
using Microsoft.Extensions.DependencyInjection;

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

var exampleRegistry = new ExamplePacketRegistry(new PacketRegistryOptions()
{
   RunHandlersInParallel = false
});
exampleRegistry.RegisterHandler<PingPacket>((ref packet, ct) =>
{
   var copy = packet;
   
   return WaitCallback();

   async ValueTask WaitCallback()
   {
      await Task.Delay(1400, ct);
      Console.WriteLine("Ping: " + copy.Name);
   }
});
exampleRegistry.RegisterHandler<PingPacket>((ref packet, ct) =>
{
   var copy = packet;
   
   return WaitCallback();

   async ValueTask WaitCallback()
   {
      await Task.Delay(1400, ct);
      Console.WriteLine("Ping: " + copy.Name);
   }
});
exampleRegistry.RegisterHandler<PingPacket>((ref packet, ct) =>
{
   var copy = packet;
   
   return WaitCallback();

   ValueTask WaitCallback()
   {
      Console.WriteLine("Ping: " + copy.Name);
      return ValueTask.CompletedTask;
   }
});
exampleRegistry.RegisterHandler<PongPacket>((ref packet, ct) =>
{
   var copy = packet;
   
   return WaitCallback();
   
   async ValueTask WaitCallback()
   {
      await Task.Delay(2400, ct);
      Console.WriteLine("Pong: " + copy.Number);
   }
});

PacketMetadata<PingPacket>.Identifier = 0;
PacketMetadata<PongPacket>.Identifier = 1;

var data = exampleRegistry.SerializeWithHeader(new PingPacket()
{
   Name = "Test"
});


await exampleRegistry.RoutePacket(data);
await exampleRegistry.RoutePacket(data);

// var t1 = exampleRegistry.RoutePacket(data);
// var t2 = exampleRegistry.RoutePacket(data);
//
// await t1;
// await t2;

//await Task.WhenAll(t1.AsTask(), t2.AsTask());

Console.WriteLine("Done: " + new TimeSpan(Stopwatch.GetTimestamp() - start));

return;



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