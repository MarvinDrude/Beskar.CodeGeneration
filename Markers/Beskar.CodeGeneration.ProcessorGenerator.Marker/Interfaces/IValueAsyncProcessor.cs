using Beskar.CodeGeneration.ProcessorGenerator.Marker.Models;
using Me.Memory.Results;

namespace Beskar.CodeGeneration.ProcessorGenerator.Marker.Interfaces;

public interface IValueAsyncProcessor<in TIn, TOut>
{
   public ValueTask<Result<TOut, ProcessorError>> ExecuteValueAsync(
      ProcessorContext context, TIn input, CancellationToken cancellationToken);
}

public interface IValueAsyncPostProcessor
{
   public ValueTask Post(ProcessorContext context, CancellationToken cancellationToken);
}