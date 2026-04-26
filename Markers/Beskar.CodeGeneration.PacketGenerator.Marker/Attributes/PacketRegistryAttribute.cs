namespace Beskar.CodeGeneration.PacketGenerator.Marker.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class PacketRegistryAttribute<TState> : Attribute;

[AttributeUsage(AttributeTargets.Class)]
public sealed class PacketRegistryAttribute : Attribute;