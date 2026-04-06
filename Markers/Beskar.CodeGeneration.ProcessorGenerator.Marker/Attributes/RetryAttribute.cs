namespace Beskar.CodeGeneration.ProcessorGenerator.Marker.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
public sealed class RetryAttribute(int retries)
   : Attribute
{
   public int Retries { get; } = retries;
}