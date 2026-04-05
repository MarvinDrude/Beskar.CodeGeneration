namespace Beskar.CodeGeneration.PacketGenerator.Marker.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class PacketAttribute(
   params Type[] types) 
   : Attribute
{
   public Type[] Types { get; init; } = types;
}