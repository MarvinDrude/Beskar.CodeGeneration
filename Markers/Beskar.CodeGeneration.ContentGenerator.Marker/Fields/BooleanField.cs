using Beskar.CodeGeneration.ContentGenerator.Marker.Interfaces;

namespace Beskar.CodeGeneration.ContentGenerator.Marker.Fields;

public sealed class BooleanField : IContentField
{
   public bool Value { get; set; }
}