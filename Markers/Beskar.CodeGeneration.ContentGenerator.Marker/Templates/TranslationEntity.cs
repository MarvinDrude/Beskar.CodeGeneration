using Beskar.CodeGeneration.ContentGenerator.Marker.Fields;

namespace Beskar.CodeGeneration.ContentGenerator.Marker.Templates;

public abstract class TranslationEntity<T>
{
   public Guid Id { get; set; }
   public ContentTypeId ParentId { get; set; }
   
   public required string TranslationKey { get; set; }
   public required T Value { get; set; }
}