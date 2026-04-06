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
internal sealed class AsyncProcessor : IAsyncProcessor<string, string>
{
   [Setting(nameof(Delay))]
   public required int Delay { get; set; }
   
   public async Task<Result<string, ProcessorError>> ExecuteAsync(
      ProcessorContext context, string input, CancellationToken cancellationToken)
   {
      await Task.Delay(Delay, cancellationToken);
      return input.ToUpper();
   }
}

[Processor]
internal sealed class ValueProcessor : IValueAsyncProcessor<int, string>
{
   public ValueTask<Result<string, ProcessorError>> ExecuteValueAsync(
      ProcessorContext context, int input, CancellationToken cancellationToken)
   {
      return new ValueTask<Result<string, ProcessorError>>(input.ToString());
   }
}

[Processor]
internal sealed class LogProcessor<TIn> : IValueAsyncProcessor<TIn, TIn>
{
   public async ValueTask<Result<TIn, ProcessorError>> ExecuteValueAsync(
      ProcessorContext context, TIn input, CancellationToken cancellationToken)
   {
      Console.WriteLine($"Starting Pipeline: {context.PipelineName}");
      return input;
   }
   
   
}

internal class LoggablePipeline
{
   [Step(0)]
   public required LogProcessor<int> Log { get; set; }
}

[Timeout(500)]
internal class BasePipeline<TValueProcessor>
{
   [Step(1)]
   public required TValueProcessor First { get; set; }
   
   [Step(2)]
   [Setting(nameof(AsyncProcessor.Delay), 1_000)]
   public required AsyncProcessor Second { get; set; }
}

[ProcessorPipeline("Main")]
[Timeout(1000), Retry(3)]
internal sealed class MainPipeline : BasePipeline<ValueProcessor>
{
   [Step(3)]
   public required MainProcessor Third { get; set; }
}