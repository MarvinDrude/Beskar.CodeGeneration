using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Beskar.CodeGeneration.PacketGenerator.Marker.Attributes;
using Beskar.CodeGeneration.PacketGenerator.Marker.Common;
using Beskar.CodeGeneration.PacketGenerator.Marker.Enums;
using Beskar.CodeGeneration.PacketGenerator.Marker.Interfaces;
using Me.Memory.Buffers;

namespace Beskar.CodeGeneration.PacketGenerator.Tests.Scenarios.Simple;

[PacketRegistry]
public sealed partial class ExampleRegistry : BaseJsonPacketRegistry
{
   public override ValueTask<RoutePacketResult> RoutePacket(
      scoped in ReadOnlySequence<byte> sequence, CancellationToken cancellationToken = default)
   {
      throw new System.NotImplementedException();
   }
}

[PacketRegistry]
public sealed partial class ExampleTwoRegistry : BasePacketRegistry
{
   public override ValueTask<RoutePacketResult> RoutePacket(
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


[Packet(typeof(ExampleRegistry))]
public sealed class PingPacket : IPacket
{
   public required string Name { get; set; }
}