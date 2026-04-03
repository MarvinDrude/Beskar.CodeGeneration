using System.Diagnostics;
using System.Diagnostics.Metrics;
using Beskar.CodeGeneration.LanguageGenerator.Marker.Attributes;
using Beskar.CodeGeneration.LanguageGenerator.Marker.Detectors;
using Beskar.CodeGeneration.LanguageGenerator.Marker.Interfaces;
using Beskar.CodeGeneration.LanguageGenerator.Marker.Providers;
using Beskar.Languages;
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


[ObserveActivity("Test.Activities", "1.0.0")]
[ObserveMeter("Test.Meters", "1.0.0")]
[ObserveInstrument("Counter", Kind: InstrumentKind.Counter)]
[ObserveInstrument("Histogram", Kind: InstrumentKind.Histogram)]
[ObserveInstrument("Gauge", Kind: InstrumentKind.Gauge)]
[ObserveInstrument("TestTest", Kind: InstrumentKind.UpDownCounter)]
public partial class Test
{
   private static ActivitySource ActivitySource = TestInstrumentation.ActivitySource;
   private static Meter MeterSource = TestInstrumentation.MeterSource;
   
   private static Counter<ulong> Counter = TestInstrumentation.Counter;
   private static Histogram<double> Histogram = TestInstrumentation.Histogram;
   private static Gauge<double> Gauge = TestInstrumentation.Gauge;
   private static UpDownCounter<long> TestTest = TestInstrumentation.TestTest;
   
   public void TestMethod()
   {
      using var activity = ActivitySource.StartActivity();

      Counter.Add(1);
   }
}

internal static class TestInstrumentation
{
   public static readonly ActivitySource ActivitySource = new("Test.Activities", "1.0.0");
   public static readonly Meter MeterSource = new("Test.Meters", "1.0.0");
   
   public static readonly Counter<ulong> Counter = MeterSource.CreateCounter<ulong>("test_counter", "Test counter", "test");
   public static readonly Histogram<double> Histogram = MeterSource.CreateHistogram<double>("test_histogram", "Test histogram", "test");
   public static readonly Gauge<double> Gauge = MeterSource.CreateGauge<double>("test_gauge", "Test gauge", "test");
   public static readonly UpDownCounter<long> TestTest = MeterSource.CreateUpDownCounter<long>("test_updowncounter", "Test updowncounter", "test");
}

[AttributeUsage(AttributeTargets.Class)]
public sealed class ObserveActivityAttribute(
   string? Name = null, string? Version = null)
   : Attribute;
   
[AttributeUsage(AttributeTargets.Class)]
public sealed class ObserveMeterAttribute(
   string? Name = null, string? Version = null)
   : Attribute;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class ObserveInstrumentAttribute(
   string PropertyName,
   string? Unit = null,
   string? Description = null,
   InstrumentKind Kind = InstrumentKind.Counter)
   : Attribute;

public enum InstrumentKind
{
   Counter = 1,
   Histogram = 2,
   Gauge = 3,
   UpDownCounter = 4,
}