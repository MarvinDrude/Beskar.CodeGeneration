# Beskar.CodeGeneration.ObserveGenerator

`Beskar.CodeGeneration.ObserveGenerator` is a source generator designed to simplify **OpenTelemetry** integration in C#. It automatically generates boilerplate for instruments and activity sources while providing a centralized registry for easy registration.

---

## Features

* **Automatic Field Generation:** Generates `ActivitySource`, `Meter`, and various instruments (`Counter`, `Histogram`, etc.) directly from attributes.
* **Static Separation:** Moves implementation details into an internal `{Name}Instrumentation` class to prevent field duplication in generic classes.
* **Centralized Registry:** Automatically populates an `ObserveRegistry` per assembly for bulk registration of telemetry sources.

---

## Usage Example

By decorating a partial class with `[Observe]` and specific instrument attributes, the generator creates all necessary plumbing.

### Source Code
```csharp
[Observe] // Marks this class for generation
[ObserveActivity] // Default name is ClassName
[ObserveMeter("Test.Meters", "2.0.0")]
[ObserveInstrument("Counter", InstrumentKind.Counter, typeof(int))]
[ObserveInstrument("Histogram", InstrumentKind.Histogram, typeof(double))]
[ObserveInstrument("Gauge", InstrumentKind.Gauge, typeof(int))]
[ObserveInstrument("TestTest", InstrumentKind.UpDownCounter, typeof(int))]
public partial class Test
{
   public void TestMethod()
   {
      using var activity = ActivitySource.StartActivity();

      Counter.Add(1);
      Histogram.Record(20d);
   }
}
```

## Generated Code
The generator produces a companion instrumentation class and links it back to your partial class:

```csharp
public partial class Test
{
   private static readonly ActivitySource ActivitySource = TestInstrumentation.ActivitySource;
   private static readonly Meter MeterSource = TestInstrumentation.MeterSource;

   private static readonly Counter<int> Counter = TestInstrumentation.Counter;
   private static readonly Histogram<double> Histogram = TestInstrumentation.Histogram;
   private static readonly Gauge<int> Gauge = TestInstrumentation.Gauge;
   private static readonly UpDownCounter<int> TestTest = TestInstrumentation.TestTest;
}

internal static class TestInstrumentation
{
   public static readonly ActivitySource ActivitySource = new("Test", "1.0.0");
   public static readonly Meter MeterSource = new("Test.Meters", "2.0.0");

   public static readonly Counter<int> Counter = MeterSource.CreateCounter<int>("Counter", null, null);
   public static readonly Histogram<double> Histogram = MeterSource.CreateHistogram<double>("Histogram", null, null);
   public static readonly Gauge<int> Gauge = MeterSource.CreateGauge<int>("Gauge", null, null);
   public static readonly UpDownCounter<int> TestTest = MeterSource.CreateUpDownCounter<int>("TestTest", null, null);
}
```

## Registration

One `ObserveRegistry` is generated per assembly. This allows you to register all telemetry sources in a single line during your application startup.

### The Generated Registry
```csharp
internal static class ObserveRegistry
{
   public static readonly ImmutableArray<string> GeneratedSourceNames = ImmutableArray.CreateRange<string>([
      "Test.Activities",
      "TestTwo",
   ]);

   public static readonly ImmutableArray<string> GeneratedMeterNames = ImmutableArray.CreateRange<string>([
      "Test.Meters",
      "TestTwo",
   ]);
}
```

### Recommended Extension Methods

You can use the registry to create clean extension methods for your `TracerProviderBuilder` and `MeterProviderBuilder`:

```csharp
public static class GeneratedTelemetryExtensions
{
   public static TracerProviderBuilder AddGeneratedSources(this TracerProviderBuilder builder)
   {
      foreach (var activityName in ObserveRegistry.GeneratedSourceNames)
      {
         builder.AddSource(activityName);
      }
      
      return builder;
   }

   public static MeterProviderBuilder AddGeneratedMeters(this MeterProviderBuilder builder)
   {
      foreach (var meterName in ObserveRegistry.GeneratedMeterNames)
      {
         builder.AddMeter(meterName);
      }
      
      return builder;
   }
}
```