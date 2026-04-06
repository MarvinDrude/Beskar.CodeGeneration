using Beskar.CodeGeneration.ProcessorGenerator.Marker.Models;
using Me.Memory.Results;

namespace Beskar.CodeGeneration.ProcessorGenerator.Marker.Interfaces;

public interface IAsyncProcessor<in TIn, TOut>
{
   public Task<Result<TOut, ProcessorError>> Execute(
      ProcessorContext context, TIn input, CancellationToken cancellationToken);
}

public interface IAsyncPostProcessor
{
   public Task Post(ProcessorContext context, CancellationToken cancellationToken);
}