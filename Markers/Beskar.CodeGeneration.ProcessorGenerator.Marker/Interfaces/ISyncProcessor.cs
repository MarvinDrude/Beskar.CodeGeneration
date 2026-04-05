using Beskar.CodeGeneration.ProcessorGenerator.Marker.Models;
using Me.Memory.Results;

namespace Beskar.CodeGeneration.ProcessorGenerator.Marker.Interfaces;

public interface ISyncProcessor<in TIn, TOut>
{
   public Result<TOut, ProcessorError> Execute(TIn input, CancellationToken cancellationToken);
}