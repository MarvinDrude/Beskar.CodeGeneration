namespace Beskar.CodeGeneration.ProcessorGenerator.Marker.Attributes;

/// <summary>
/// Order of the processor step in this pipeline
/// </summary>
/// <param name="order">Lower is earlier in the pipeline</param>
[AttributeUsage(AttributeTargets.Property)]
public sealed class StepAttribute(int order) : Attribute
{
   public int Order { get; } = order;
}