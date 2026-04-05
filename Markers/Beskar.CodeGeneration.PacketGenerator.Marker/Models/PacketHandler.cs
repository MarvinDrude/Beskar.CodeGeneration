using Beskar.CodeGeneration.PacketGenerator.Marker.Interfaces;

namespace Beskar.CodeGeneration.PacketGenerator.Marker.Models;

public delegate ValueTask PacketHandler<TPacket>(ref TPacket packet, CancellationToken cancellationToken)
   where TPacket : IPacket;