namespace Beskar.CodeGeneration.ProcessorGenerator.Marker.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class TimeoutAttribute(int milliseconds) : Attribute
{
   public int Milliseconds { get; } = milliseconds;
}