using Beskar.CodeGeneration.ProcessorGenerator.Marker.Attributes;
using Beskar.CodeGeneration.ProcessorGenerator.Marker.Interfaces;
using Beskar.CodeGeneration.ProcessorGenerator.Marker.Models;
using Me.Memory.Results;

namespace Beskar.CodeGeneration.ProcessorGenerator.Marker.Internal;

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

   public Task Post(ProcessorContext context, CancellationToken cancellationToken)
   {
      Console.WriteLine($"Async Post: {context.Elapsed}");
      return Task.CompletedTask;
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
   
   public ValueTask Post(
      ProcessorContext context, CancellationToken cancellationToken)
   {
      Console.WriteLine($"Ending Pipeline: {context.PipelineName}, took {context.Elapsed}");
      return ValueTask.CompletedTask;
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
internal sealed class MainPipeline : BasePipeline<ValueProcessor>
{
   [Step(3)]
   public required MainProcessor Third { get; set; }

   public static MainPipeline GetFactory(IServiceProvider provider)
   {
      var pipeline = new MainPipeline()
      {
         Log = provider.GetService(typeof(LogProcessor<int>)) as LogProcessor<int>
            ?? throw new InvalidOperationException("Can't find processor in DI."),
         First = provider.GetService(typeof(ValueProcessor)) as ValueProcessor 
            ?? throw new InvalidOperationException("Can't find processor in DI."),
         Second = provider.GetService(typeof(AsyncProcessor)) as AsyncProcessor 
            ?? throw new InvalidOperationException("Can't find processor in DI."),
         Third = provider.GetService(typeof(MainProcessor)) as MainProcessor
            ?? throw new InvalidOperationException("Can't find processor in DI."),
      };

      pipeline.Second.Delay = 1_000;
      pipeline.Second.PostMessage = "Setting Post Message";
      
      pipeline.First.ArraySetting = ["Test1", "Test2"];
      
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
      
      
      await Log.Post(context, ct);
      
      return r4.Success;
   }
   
   public sealed class MainPipelineContext : ProcessorContext
   {
      public string? MyString { get; set; }
      
      public int MyInt { get; set; }
      
      public object? MyObject { get; set; }
      
      public bool MyBool { get; set; }
   }
}