using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Beskar.CodeGeneration.PacketGenerator.Marker.Attributes;
using Beskar.CodeGeneration.PacketGenerator.Marker.Common;
using Beskar.CodeGeneration.PacketGenerator.Marker.Enums;
using Beskar.CodeGeneration.PacketGenerator.Marker.Interfaces;
using Beskar.CodeGeneration.PacketGenerator.Marker.Models;

namespace Beskar.CodeGeneration.PacketGenerator.Marker.Internal;

[PacketRegistry]
public sealed class ExamplePacketRegistry : BaseJsonPacketRegistry
{
   private readonly IPacketHandlerCollection[] _handlers;

   public ExamplePacketRegistry()
   {
      _handlers =
      [
         new PingPacketHandlerCollection(this),
         new PongPacketHandlerCollection(this)
      ];
   }

   public bool RegisterHandler<TPacket>(PacketHandler<TPacket> handler)
      where TPacket : IPacket
   {
      var packetId = PacketMetadata<TPacket>.Identifier;
      var handlerCollection = _handlers[packetId];

      if (handlerCollection is not IPacketHandlerCollection<TPacket> handlerCollectionTyped) 
         return false;
      
      handlerCollectionTyped.RegisterHandler(handler);
      return true;
   }

   public override ValueTask<RoutePacketResult> RoutePacket(
      scoped in ReadOnlySequence<byte> sequence,
      CancellationToken cancellationToken = default)
   {
      var reader = new SequenceReader<byte>(sequence);
      if (!reader.TryReadLittleEndian(out int packetId))
      {
         return ValueTask.FromResult(RoutePacketResult.NotEnoughData);
      }

      if (packetId < 0 || packetId >= _handlers.Length)
      {
         return ValueTask.FromResult(RoutePacketResult.UnknownPacket);
      }
      
      ref var arrayPointer = ref MemoryMarshal.GetArrayDataReference(_handlers);
      var handlerCollection = Unsafe.Add(ref arrayPointer, (nint)packetId);
      
      return handlerCollection.HandlerCount == 0 
         ? ValueTask.FromResult(RoutePacketResult.SuccessNoHandlers) 
         : handlerCollection.Handle(ref reader, cancellationToken);
   }
}

file sealed class PingPacketHandlerCollection(ExamplePacketRegistry registry)
   : BasePacketHandlerCollection<PingPacket>(registry);

file sealed class PongPacketHandlerCollection(ExamplePacketRegistry registry)
   : BasePacketHandlerCollection<PongPacket>(registry);