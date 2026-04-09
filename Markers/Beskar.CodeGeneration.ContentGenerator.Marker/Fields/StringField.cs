using Beskar.CodeGeneration.ContentGenerator.Marker.Enums;
using Beskar.CodeGeneration.ContentGenerator.Marker.Interfaces;

namespace Beskar.CodeGeneration.ContentGenerator.Marker.Fields;

public sealed class StringField : IContentField
{
   public required string Value { get; set; }

   public StringFieldKind Kind { get; set; } = StringFieldKind.RawString;
}