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
internal class BasePipeline<TValueProcessor>
{
   [Step(1)]
   [Retry(3)]
   public required TValueProcessor First { get; set; }
   
   [Step(2)]
   [Setting(nameof(AsyncProcessor.Delay), 1_000)]
   [Setting(nameof(AsyncProcessor.PostMessage), "Setting Post Message")]
   public required AsyncProcessor Second { get; set; }
}

[ProcessorPipeline("Main")]
[Timeout(1000), Retry(3)]
[ContextVariable<bool>("MyBool")]
internal sealed class MainPipeline : BasePipeline<ValueProcessor>
{
   [Step(3)]
   public required MainProcessor Third { get; set; }
}