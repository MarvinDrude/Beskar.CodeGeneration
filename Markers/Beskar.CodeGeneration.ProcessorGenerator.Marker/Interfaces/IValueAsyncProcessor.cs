using Beskar.CodeGeneration.ProcessorGenerator.Marker.Models;
using Me.Memory.Results;

namespace Beskar.CodeGeneration.ProcessorGenerator.Marker.Interfaces;

public interface IValueAsyncProcessor<in TIn, TOut>
{
   public ValueTask<Result<TOut, ProcessorError>> ExecuteValueAsync(TIn input, CancellationToken cancellationToken);
}