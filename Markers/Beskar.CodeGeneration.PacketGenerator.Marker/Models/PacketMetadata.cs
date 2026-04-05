using Beskar.CodeGeneration.PacketGenerator.Marker.Interfaces;

namespace Beskar.CodeGeneration.PacketGenerator.Marker.Models;

public static class PacketMetadata<T>
   where T : IPacket
{
   public static int Identifier { get; set; }
}