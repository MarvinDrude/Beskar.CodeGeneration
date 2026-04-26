using Beskar.CodeGeneration.PacketGenerator.Marker.Interfaces;

namespace Beskar.CodeGeneration.PacketGenerator.Marker.Models;

public delegate ValueTask PacketHandler<TState, TPacket>(ref TState state, ref TPacket packet, CancellationToken cancellationToken)
   where TPacket : IPacket;