using Beskar.CodeGeneration.PacketGenerator.Marker.Attributes;
using Beskar.CodeGeneration.PacketGenerator.Marker.Interfaces;
using Beskar.CodeGeneration.PacketGenerator.Tests.Scenarios.Simple;

// ReSharper disable once CheckNamespace
namespace Beskar.Test
{
   [Packet(typeof(ExampleRegistry), typeof(ExampleTwoRegistry))]
   public struct SeparatePacket : IPacket
   {
      
   }
}

[Packet(typeof(ExampleTwoRegistry))]
public struct SeparateGlobalPacket : IPacket
{
   
}
