using Beskar.CodeGeneration.ProcessorGenerator.Marker.Attributes;
using Beskar.CodeGeneration.ProcessorGenerator.Marker.Interfaces;
using Beskar.CodeGeneration.ProcessorGenerator.Marker.Models;
using Me.Memory.Results;

namespace Beskar.CodeGeneration.ProcessorGenerator.Marker.Internal;

[Processor]
internal sealed class MainProcessor : ISyncProcessor<string, int>
{
   public Result<int, ProcessorError> Execute(
      string input, CancellationToken cancellationToken)
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
      string input, CancellationToken cancellationToken)
   {
      await Task.Delay(Delay, cancellationToken);
      return input.ToUpper();
   }
}

[Processor]
internal sealed class ValueProcessor : IValueAsyncProcessor<int, string>
{
   public ValueTask<Result<string, ProcessorError>> ExecuteValueAsync(
      int input, CancellationToken cancellationToken)
   {
      return new ValueTask<Result<string, ProcessorError>>(input.ToString());
   }
}

internal class LoggablePipeline
{
   
}

[ProcessorPipeline]
internal class BasePipeline
{
   [Step(1)]
   public required ValueProcessor First { get; set; }
   
   [Step(2)]
   [Setting(nameof(AsyncProcessor.Delay), 1_000)]
   public required AsyncProcessor Second { get; set; }
}

internal sealed class MainPipeline : BasePipeline
{
   
}