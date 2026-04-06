namespace Beskar.CodeGeneration.ProcessorGenerator.Marker.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class ContextVariableAttribute<TType>(
   string name) 
   : Attribute
{
   public string Name { get; } = name;
}