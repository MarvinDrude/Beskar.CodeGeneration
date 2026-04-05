# Beskar.CodeGeneration.PacketGenerator

The Beskar Packet Generator is a high-performance, 
source-generator-based framework for handling binary or 
JSON packet serialization and automated routing in C#. 
It removes boilerplate by automatically generating the necessary 
logic to identify, deserialize, and dispatch packets to their respective 
handlers.

## Core Concepts

### 1. The Packet

A packet is any ``class`` or ``struct`` that implements the ``IPacket`` marker interface. 
You mark it with the ``[Packet]`` attribute to associate it with one or more 
registries.

```csharp
[Packet(typeof(GamePacketRegistry))]
public sealed class PlayerMovePacket : IPacket
{
   public required float X { get; init; }
   public required float Y { get; init; }
}
```
### 2. The Registry

The Registry is the central hub. By inheriting from ``BasePacketRegistry`` 
(for custom binary logic) or ``BaseJsonPacketRegistry`` (for UTF8 JSON), 
the source generator injects the complex RoutePacket logic.

> **Important**: The registry and all its packets must be in the same assembly.

```csharp
// JSON based (not recommended)
[PacketRegistry]
public sealed partial class GamePacketRegistry : BaseJsonPacketRegistry;

// Your binary packet registry
[PacketRegistry] 
public sealed partial class GamePacketRegistry : BasePacketRegistry 
{
    // override Serialize and TryDeserialize
}
```

## Features 

- Zero Reflection: All routing and type identification are resolved at compile-time for maximum performance.
- Flexible Serialization: Use the built-in JSON registry or implement your own binary format by overriding Serialize and TryDeserialize.
- Memory Efficient: Utilizes ReadOnlySequence<byte> and BufferWriter<byte> to minimize allocations and support modern memory primitives.
- Multi-Registry Support: A single packet can be registered across multiple different registries (e.g., a PingPacket used in both AuthRegistry and GameRegistry).

## Usage Example

### Serialization with Headers
The registry can automatically prepend a unique identifier to your payload, allowing for easy identification on the receiving end.

```csharp
var registry = new GamePacketRegistry();
var packet = new PlayerMovePacket { X = 10, Y = 20 };

// Serializes the packet ID + JSON data into a byte array
// Also supports using a BufferWriter<byte> instead of byte[]
byte[] data = registry.SerializeWithHeader(packet);
```

### Automated Routing

When you receive a byte sequence, simply pass it to the registry. The generator-produced code will identify the packet type and invoke the corresponding logic.

```csharp
// Automatically identifies 'PlayerMovePacket' and routes it
await registry.RoutePacket(receivedData, cancellationToken);
```

### Register handlers

```csharp
registry.RegisterHandler<PlayerMovePacket>((ref packet, ct) =>
{
    // if async behavior is required, you need a second method called here,
    // since the packet is by reference (for better large structs)
    Console.WriteLine("Test");
    return ValueTask.CompletedTask;
});
```