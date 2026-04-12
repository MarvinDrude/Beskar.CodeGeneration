namespace Beskar.CodeGeneration.ContentGenerator.Marker.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public sealed class ComponentOptionsAttribute(
   Type? baseType = null)
   : Attribute
{
   public Type? BaseType { get; init; } = baseType;
}