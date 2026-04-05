namespace Beskar.CodeGeneration.PacketGenerator.Marker.Common;

public sealed class PacketRegistryOptions
{
   /// <summary>
   /// Whether if more than one handler should be run in parallel for the same packet.
   /// </summary>
   public bool RunHandlersInParallel { get; set; } = true;
}