using System.Buffers;
using System.Collections.Concurrent;
using Beskar.CodeGeneration.PacketGenerator.Marker.Enums;
using Beskar.CodeGeneration.PacketGenerator.Marker.Interfaces;
using Beskar.CodeGeneration.PacketGenerator.Marker.Models;

namespace Beskar.CodeGeneration.PacketGenerator.Marker.Common;

public abstract class BasePacketHandlerCollection<TPacket>(
   BasePacketRegistry registry)
   : IPacketHandlerCollection<TPacket>
   where TPacket : IPacket
{
   public int HandlerCount => _handlers.Count;

   private readonly BasePacketRegistry _registry = registry;
   private readonly ConcurrentStack<PacketHandler<TPacket>> _handlers = [];

   public void RegisterHandler(PacketHandler<TPacket> handler)
   {
      _handlers.Push(handler);
   }

   public ValueTask<RoutePacketResult> Handle(
      ref SequenceReader<byte> reader, CancellationToken cancellationToken)
   {
      if (!_registry.TryDeserialize<TPacket>(ref reader, out var packet))
      {
         return ValueTask.FromResult(RoutePacketResult.InvalidPacket);
      }

      if (_handlers.Count != 1 || !_handlers.TryPeek(out var handler))
         return InvokeIterateAsync(packet, cancellationToken);
      
      var singleTask = handler.Invoke(ref packet, cancellationToken);
      return singleTask.IsCompletedSuccessfully
         ? ValueTask.FromResult(RoutePacketResult.Success)
         : InvokeSingleAsync(singleTask);
   }
   
   private async ValueTask<RoutePacketResult> InvokeSingleAsync(ValueTask task)
   {
      await task;
      return RoutePacketResult.Success;
   }

   private async ValueTask<RoutePacketResult> InvokeIterateAsync(
      TPacket packet, CancellationToken cancellationToken)
   {
      using var enumerator = _handlers.GetEnumerator();
      while (enumerator.MoveNext())
      {
         // one after each other is fine here
         await enumerator.Current.Invoke(ref packet, cancellationToken);
      }
      
      return RoutePacketResult.Success;
   }
}