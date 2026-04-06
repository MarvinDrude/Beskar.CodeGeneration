using System.Diagnostics;

namespace Beskar.CodeGeneration.ProcessorGenerator.Marker.Models;

public sealed class ProcessorContext
{
   public required string PipelineName { get; init; }
   
   public long StartTimestamp { get; init; } 
   
   public long CurrentTimestamp => Stopwatch.GetTimestamp();
}