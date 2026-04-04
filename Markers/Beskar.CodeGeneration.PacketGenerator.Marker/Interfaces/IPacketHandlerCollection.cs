using System.Buffers;
using Beskar.CodeGeneration.PacketGenerator.Marker.Enums;
using Beskar.CodeGeneration.PacketGenerator.Marker.Models;

namespace Beskar.CodeGeneration.PacketGenerator.Marker.Interfaces;

public interface IPacketHandlerCollection
{
   public int HandlerCount { get; }
   
   public ValueTask<RoutePacketResult> Handle(ref SequenceReader<byte> reader, CancellationToken cancellationToken);
}

public interface IPacketHandlerCollection<TPacket> : IPacketHandlerCollection
   where TPacket : IPacket
{
   public void RegisterHandler(PacketHandler<TPacket> handler);
}