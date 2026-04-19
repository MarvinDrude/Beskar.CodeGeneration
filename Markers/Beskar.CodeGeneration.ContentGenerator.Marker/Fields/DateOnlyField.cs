using Beskar.CodeGeneration.ContentGenerator.Marker.Interfaces;

namespace Beskar.CodeGeneration.ContentGenerator.Marker.Fields;

public sealed class DateOnlyField : IContentField
{
   public DateOnly Value { get; set; }
}