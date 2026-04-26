using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Beskar.CodeGeneration.PacketGenerator.Marker.Attributes;
using Beskar.CodeGeneration.PacketGenerator.Marker.Common;
using Beskar.CodeGeneration.PacketGenerator.Marker.Interfaces;
using Beskar.CodeGeneration.PacketGenerator.Marker.Models;
using Me.Memory.Buffers;

namespace Beskar.CodeGeneration.PacketGenerator.Tests.Scenarios.Stateful;

public sealed class ClientState
{
    public int ClientId { get; set; }
}

[PacketRegistry<ClientState>]
public sealed partial class StatefulRegistry : BasePacketRegistry<ClientState>
{
   public override ValueTask<RoutePacketResult> RoutePacket(
      ref ClientState state,
      scoped in ReadOnlySequence<byte> sequence, CancellationToken cancellationToken = default)
   {
      throw new System.NotImplementedException();
   }

   public override bool TryDeserialize<T>(ref SequenceReader<byte> reader, [MaybeNullWhen(false)] out T packet)
   {
      throw new System.NotImplementedException();
   }

   public override void Serialize<T>(ref BufferWriter<byte> writer, T packet)
   {
      throw new System.NotImplementedException();
   }
}

[Packet(typeof(StatefulRegistry))]
public sealed class StatefulPacket : IPacket
{
   public required string Data { get; set; }
}
