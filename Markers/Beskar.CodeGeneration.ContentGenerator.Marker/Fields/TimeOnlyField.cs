using Beskar.CodeGeneration.ContentGenerator.Marker.Interfaces;

namespace Beskar.CodeGeneration.ContentGenerator.Marker.Fields;

public sealed class TimeOnlyField : IContentField
{
   public TimeOnly Value { get; set; }
}