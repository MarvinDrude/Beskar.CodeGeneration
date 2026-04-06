namespace Beskar.CodeGeneration.ProcessorGenerator.Marker.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class RetryAttribute(int retries)
   : Attribute
{
   public int Retries { get; } = retries;
}