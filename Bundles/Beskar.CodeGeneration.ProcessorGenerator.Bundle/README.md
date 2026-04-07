# Beskar.CodeGeneration.ProcessorGenerator

A source generator for creating efficient, type-safe processing pipelines. It automates the boilerplate of chaining processors and managing execution flow.

### What it does
- **Generates Pipeline Execution**: Creates an `Execute` (or `ExecuteAsync`) method that chains processors in a defined order.
- **Dependency Injection Integration**: Generates a `GetFactory(IServiceProvider)` method to resolve and configure the pipeline and its processors from a DI container.
- **Context Management**: Provides a `ProcessorContext` to share state across processors within a single execution.
- **Timeout Support**: Built-in support for execution timeouts via `CancellationToken`.
- **Post-Processing**: Supports cleanup or logging logic via `Post` methods that run after the main execution.

### How it works
1. **Define Processors**: Implement `ISyncProcessor`, `IAsyncProcessor`, or `IValueAsyncProcessor` and mark them with `[Processor]`.
2. **Define a Pipeline**: Create a `partial class` marked with `[ProcessorPipeline]`.
3. **Add Steps**: Add properties to your pipeline class for each processor and mark them with `[Step(order)]`.
4. **Configure Settings**: Use `[Setting]` attributes on steps to inject values into processor properties during initialization.

### Example

```csharp
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

...

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
[Timeout(1000)]
[ContextVariable<bool>("MyBool")]
internal sealed partial class MainPipeline : BasePipeline<ValueProcessor>
{
   [Step(3)]
   public required MainProcessor Third { get; set; }
}
```

How to use it?

```csharp
var serviceCollection = new ServiceCollection();
serviceCollection.AddScoped<MainProcessor>()
   .AddScoped<AsyncProcessor>()
   .AddScoped<ValueProcessor>()
   .AddScoped(typeof(LogProcessor<>));

serviceCollection.AddScoped(MainPipeline.GetFactory);

var provider = serviceCollection.BuildServiceProvider();
var pipeline = provider.GetRequiredService<MainPipeline>();

await pipeline.Execute(123, CancellationToken.None);
```

What is being generated?

```csharp
internal sealed partial class MainPipeline
{
   public static global::Beskar.CodeGeneration.ProcessorGenerator.Tests.Scenarios.Simple.MainPipeline GetFactory(IServiceProvider provider)
   {
      var pipeline = new global::Beskar.CodeGeneration.ProcessorGenerator.Tests.Scenarios.Simple.MainPipeline()
      {
         Third = provider.GetService(typeof(global::Beskar.CodeGeneration.ProcessorGenerator.Tests.Scenarios.Simple.MainProcessor)) as global::Beskar.CodeGeneration.ProcessorGenerator.Tests.Scenarios.Simple.MainProcessor
            ?? throw new InvalidOperationException("Cannot resolve global::Beskar.CodeGeneration.ProcessorGenerator.Tests.Scenarios.Simple.MainProcessor"),
         First = provider.GetService(typeof(global::Beskar.CodeGeneration.ProcessorGenerator.Tests.Scenarios.Simple.ValueProcessor)) as global::Beskar.CodeGeneration.ProcessorGenerator.Tests.Scenarios.Simple.ValueProcessor
            ?? throw new InvalidOperationException("Cannot resolve global::Beskar.CodeGeneration.ProcessorGenerator.Tests.Scenarios.Simple.ValueProcessor"),
         Second = provider.GetService(typeof(global::Beskar.CodeGeneration.ProcessorGenerator.Tests.Scenarios.Simple.AsyncProcessor)) as global::Beskar.CodeGeneration.ProcessorGenerator.Tests.Scenarios.Simple.AsyncProcessor
            ?? throw new InvalidOperationException("Cannot resolve global::Beskar.CodeGeneration.ProcessorGenerator.Tests.Scenarios.Simple.AsyncProcessor"),
         Log = provider.GetService(typeof(global::Beskar.CodeGeneration.ProcessorGenerator.Tests.Scenarios.Simple.LogProcessor<int>)) as global::Beskar.CodeGeneration.ProcessorGenerator.Tests.Scenarios.Simple.LogProcessor<int>
            ?? throw new InvalidOperationException("Cannot resolve global::Beskar.CodeGeneration.ProcessorGenerator.Tests.Scenarios.Simple.LogProcessor<int>"),
      };

      pipeline.First.ArraySetting = ["Test1", "Test2"];

      pipeline.Second.Delay = 1000;
      pipeline.Second.PostMessage = "Setting Post Message";

      return pipeline;
   }

   public async Task<Result<int, ProcessorError>> Execute(int input, CancellationToken ct)
   {
      ProcessorContext context = new MainPipelineContext()
      {
         PipelineName = "Main"
      };

      using var cts = CancellationTokenSource.CreateLinkedTokenSource(ct);
      cts.CancelAfter(1000);
      ct = cts.Token;

      var r1 = await Log.Execute(context, input, ct);
      if (!r1.HasValue) return r1.Error;

      var r2 = await First.Execute(context, r1.Success, ct);
      if (!r2.HasValue) return r2.Error;

      var r3 = await Second.Execute(context, r2.Success, ct);
      if (!r3.HasValue) return r3.Error;

      var r4 = Third.Execute(context, r3.Success, ct);
      if (!r4.HasValue) return r4.Error;

      var error1 = await Second.Post(context, ct);
      if (error1 is not null) return error1;

      var error2 = await Log.Post(context, ct);
      if (error2 is not null) return error2;

      return r4.Success;
   }

   public sealed class MainPipelineContext : ProcessorContext
   {
      public bool MyBool { get; set; }

      public string? MyString { get; set; }

      public int MyInt { get; set; }

      public object? MyObject { get; set; }

   }
}
```
