using Beskar.CodeGeneration.PacketGenerator.Marker.Attributes;
using Beskar.CodeGeneration.PacketGenerator.Marker.Interfaces;

namespace Beskar.CodeGeneration.PacketGenerator.Marker.Internal;

[Packet(typeof(ExamplePacketRegistry))]
internal sealed class PingPacket : IPacket
{
   public required string Name { get; set; }
}

[Packet(typeof(ExamplePacketRegistry))]
internal struct PongPacket : IPacket
{
   public required int Number { get; set; }
}