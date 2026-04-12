using Beskar.CodeGeneration.ContentGenerator.Marker.Interfaces;

namespace Beskar.CodeGeneration.ContentGenerator.Marker.Fields;

public sealed class DateTimeField : IContentField
{
   public DateTimeOffset Value { get; set; }
}