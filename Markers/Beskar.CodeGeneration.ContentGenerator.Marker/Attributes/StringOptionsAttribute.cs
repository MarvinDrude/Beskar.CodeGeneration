using Beskar.CodeGeneration.ContentGenerator.Marker.Enums;

namespace Beskar.CodeGeneration.ContentGenerator.Marker.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public sealed class StringOptionsAttribute(
   StringFieldKind kind = StringFieldKind.RawString,
   int maxLength = 255) 
   : Attribute
{
   public StringFieldKind Kind { get; init; } = kind;
   
   public int MaxLength { get; init; } = maxLength;
}