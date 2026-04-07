namespace Beskar.CodeGeneration.ProcessorGenerator.Marker.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class ProcessorPipelineAttribute(
   string name) 
   : Attribute
{
   public string Name { get; } = name;
}