namespace Beskar.CodeGeneration.ContentGenerator.Marker.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public abstract class FieldAttribute(
   string? name = null) 
   : Attribute
{
   public string? Name { get; init; } = name;
}