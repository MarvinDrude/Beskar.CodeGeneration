using System.Diagnostics;
using System.Diagnostics.Metrics;
using Beskar.CodeGeneration.LanguageGenerator.Marker.Attributes;
using Beskar.CodeGeneration.LanguageGenerator.Marker.Detectors;
using Beskar.CodeGeneration.LanguageGenerator.Marker.Interfaces;
using Beskar.CodeGeneration.LanguageGenerator.Marker.Providers;
using Beskar.CodeGeneration.ObserveGenerator.Marker.Attributes;
using Beskar.CodeGeneration.ObserveGenerator.Marker.Enums;
using Beskar.Languages;
using BeskarExperiments.ObserveExtensions;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello World!");

var serviceCollection = new ServiceCollection()
   .AddSingleton<ILanguageDetector, SystemCultureDetector>() // can add multiple detectors of varying priority
   .AddSingleton<ITranslationProvider, JsonTranslationProvider>() // example implementation of json files as base (can be DB too)
   .AddSingleton<TranslationFacade>();
   
var serviceProvider = serviceCollection.BuildServiceProvider();
   
// Json Provider example - can create own providers like DB or Third party api
var provider = (JsonTranslationProvider)serviceProvider.GetRequiredService<ITranslationProvider>();
provider.Initialize("Translations"); // folder path
await provider.PopulateCache(CancellationToken.None); // can be called again if file changed at runtime

var translation = serviceProvider.GetRequiredService<TranslationFacade>();
Console.WriteLine(translation.TestGroup.Test); // calls the translation provider in the background
Console.WriteLine(translation.RegisterGroup.Description);

Console.WriteLine(string.Join(",", ObserveRegistry.GeneratedMeterNames));
Console.WriteLine(string.Join(",", ObserveRegistry.GeneratedSourceNames));

var test = new Test<object, object>();
test.TestMethod();

return;

[TranslationGroup]
public enum TestGroup
{
   [TranslationKey]
   Test = 1,
   [TranslationKey(defaultValue: "Test2-Default")]
   Test2 = 2,
}

[TranslationGroup]
public enum RegisterGroup
{
   [TranslationKey]
   Title = 1,
   [TranslationKey]
   Description = 2,
}

[Observe]
[ObserveActivity("Test.Activities", "1.0.0")]
[ObserveMeter("Test.Meters", "1.0.0")]
[ObserveInstrument("Counter", InstrumentKind.Counter, typeof(int))]
[ObserveInstrument("Histogram", InstrumentKind.Histogram, typeof(double))]
[ObserveInstrument("Gauge", InstrumentKind.Gauge, typeof(int))]
[ObserveInstrument("TestTest", InstrumentKind.UpDownCounter, typeof(int))]
public partial class Test<T1, T2>
   where T1 : class
{
   public void TestMethod()
   {
      using var activity = ActivitySource.StartActivity();
      
      Counter.Add(1);
      Histogram.Record(20d);
   }
}

[Observe]
[ObserveActivity]
[ObserveMeter]
[ObserveInstrument("Counter", InstrumentKind.Counter, typeof(int))]
[ObserveInstrument("Histogram", InstrumentKind.Histogram, typeof(double))]
[ObserveInstrument("Gauge", InstrumentKind.Gauge, typeof(int))]
[ObserveInstrument("TestTest", InstrumentKind.UpDownCounter, typeof(int))]
public partial class TestTwo<T>
{
   public void TestMethod()
   {
      using var activity = ActivitySource.StartActivity();
      
      Counter.Add(1);
      Histogram.Record(20d);
   }
}