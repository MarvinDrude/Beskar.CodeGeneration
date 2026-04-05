using Beskar.CodeGeneration.ProcessorGenerator.Marker.Models;
using Me.Memory.Results;

namespace Beskar.CodeGeneration.ProcessorGenerator.Marker.Interfaces;

public interface IAsyncProcessor<in TIn, TOut>
{
   public Task<Result<TOut, ProcessorError>> ExecuteAsync(TIn input, CancellationToken cancellationToken);
}