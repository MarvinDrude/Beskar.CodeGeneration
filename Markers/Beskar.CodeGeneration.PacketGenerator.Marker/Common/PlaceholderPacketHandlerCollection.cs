using System.Buffers;
using Beskar.CodeGeneration.PacketGenerator.Marker.Enums;
using Beskar.CodeGeneration.PacketGenerator.Marker.Interfaces;
using Beskar.CodeGeneration.PacketGenerator.Marker.Models;

namespace Beskar.CodeGeneration.PacketGenerator.Marker.Common;

public sealed class PlaceholderPacketHandlerCollection<TPacket>(BasePacketRegistry registry) 
   : BasePacketHandlerCollection<TPacket>(registry) where TPacket : IPacket
{
   public override void RegisterHandler(PacketHandler<TPacket> handler)
   {
      throw new InvalidOperationException("This packet is not registered in this registry.");
   }

   public override ValueTask<RoutePacketResult> Handle(
      ref SequenceReader<byte> reader, CancellationToken cancellationToken)
   {
      throw new InvalidOperationException("This packet is not registered in this registry.");
   }
}