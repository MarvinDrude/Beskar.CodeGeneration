using Beskar.CodeGeneration.ContentGenerator.Marker.Enums;

namespace Beskar.CodeGeneration.ContentGenerator.Marker.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public sealed class StringOptionsAttribute(
   StringFieldKind kind = StringFieldKind.RawString) 
   : Attribute
{
   public StringFieldKind Kind { get; init; } = kind;
}