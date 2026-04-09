using Beskar.CodeGeneration.ContentGenerator.Marker.Enums;

namespace Beskar.CodeGeneration.ContentGenerator.Marker.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class ContentTypeAttribute(
   ContentKind kind) 
   : Attribute
{
   public ContentKind Kind { get; init; } = kind;
}