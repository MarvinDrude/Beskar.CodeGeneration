using System.Diagnostics;

namespace Beskar.CodeGeneration.ProcessorGenerator.Marker.Models;

public class ProcessorContext
{
   public required string PipelineName { get; init; }

   public long StartTimestamp { get; }

   public ProcessorContext()
   {
      StartTimestamp = CurrentTimestamp;
   }
   
   public long CurrentTimestamp => Stopwatch.GetTimestamp();
   public TimeSpan Elapsed => TimeSpan.FromTicks(CurrentTimestamp - StartTimestamp);
}