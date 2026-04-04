using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Beskar.CodeGeneration.PacketGenerator.Marker.Enums;
using Beskar.CodeGeneration.PacketGenerator.Marker.Interfaces;
using Beskar.CodeGeneration.PacketGenerator.Marker.Models;
using Me.Memory.Buffers;
using Me.Memory.Extensions;

namespace Beskar.CodeGeneration.PacketGenerator.Marker.Common;

public abstract class BasePacketRegistry
{
   public abstract ValueTask<RoutePacketResult> RoutePacket(
      scoped in ReadOnlySequence<byte> sequence,
      CancellationToken cancellationToken = default);
   
   public abstract bool TryDeserialize<T>(
      ref SequenceReader<byte> reader, 
      [MaybeNullWhen(false)] out T packet)
      where T : IPacket;
   
   public abstract void Serialize<T>(
      ref BufferWriter<byte> writer, T packet)
      where T : IPacket;
   
   [MethodImpl(MethodImplOptions.AggressiveInlining)]
   public byte[] Serialize<T>(T packet)
      where T : IPacket
   {
      var writer = new BufferWriter<byte>(512);
      try
      {
         Serialize(ref writer, packet);
         return writer.WrittenSpan.ToArray();
      }
      finally
      {
         writer.Dispose();
      }
   }

   [MethodImpl(MethodImplOptions.AggressiveInlining)]
   public void SerializeWithHeader<T>(
      ref BufferWriter<byte> writer, T packet)
      where T : IPacket
   {
      var packetId = PacketMetadata<T>.Identifier;
      packetId.WriteLittleEndian(ref writer);
      
      Serialize(ref writer, packet);
   }

   [MethodImpl(MethodImplOptions.AggressiveInlining)]
   public byte[] SerializeWithHeader<T>(T packet)
      where T : IPacket
   {
      var packetId = PacketMetadata<T>.Identifier;
      var writer = new BufferWriter<byte>(512);
      try
      {
         packetId.WriteLittleEndian(ref writer);
         Serialize(ref writer, packet);
         
         return writer.WrittenSpan.ToArray();
      }
      finally
      {
         writer.Dispose();
      }
   }

   [MethodImpl(MethodImplOptions.AggressiveInlining)]
   public ValueTask<RoutePacketResult> RoutePacket(
      byte[] bytes, CancellationToken cancellationToken = default)
   {
      var sequence = new ReadOnlySequence<byte>(bytes);
      return RoutePacket(sequence, cancellationToken);
   }
   
   [MethodImpl(MethodImplOptions.AggressiveInlining)]
   public ValueTask<RoutePacketResult> RoutePacket(
      ReadOnlyMemory<byte> memory, CancellationToken cancellationToken = default)
   {
      var sequence = new ReadOnlySequence<byte>(memory);
      return RoutePacket(sequence, cancellationToken);
   }
}
