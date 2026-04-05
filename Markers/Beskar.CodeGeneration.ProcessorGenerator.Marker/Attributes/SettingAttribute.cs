namespace Beskar.CodeGeneration.ProcessorGenerator.Marker.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public sealed class SettingAttribute(string name, object? value = null) : Attribute
{
   public string Name { get; } = name;
   
   public object? Value { get; } = value;
}