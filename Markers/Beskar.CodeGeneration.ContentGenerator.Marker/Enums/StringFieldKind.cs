using Beskar.CodeGeneration.EnumGenerator.Marker.Attributes;

namespace Beskar.CodeGeneration.ContentGenerator.Marker.Enums;

[FastEnum]
public enum StringFieldKind : byte
{
   RawString = 1,
   Markdown = 2,
   SingleLine = 3,
   MultiLine = 4,
}