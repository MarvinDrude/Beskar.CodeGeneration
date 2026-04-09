using Beskar.CodeGeneration.ContentGenerator.Marker.Attributes;
using Beskar.CodeGeneration.ContentGenerator.Marker.Enums;
using Beskar.CodeGeneration.ContentGenerator.Marker.Fields;
using Beskar.CodeGeneration.ContentGenerator.Marker.Models;

namespace Beskar.CodeGeneration.ContentGenerator.Marker.Internal;

[ContentType(ContentKind.Collection)]
public sealed class ExampleType : ContentTypeBase
{
   [UniqueId]
   public required ContentTypeId Id { get; set; }
   
   [StringOptions(StringFieldKind.Markdown)]
   public required LocalizedField<StringField> Title { get; set; }
   
   [StringOptions(StringFieldKind.SingleLine)]
   public StringField? Slug { get; set; }
   
   
}

[ContentType(ContentKind.Component)]
public sealed class TestComponent : ComponentBase
{
   [UniqueId]
   public required ContentTypeId Id { get; set; }
   
   
}

[ContentType(ContentKind.Component)]
public sealed class AllLangComponent : ComponentBase
{
   [UniqueId]
   public required ContentTypeId Id { get; set; }
   
   public StringField? Title { get; set; }
   
   public StringField? Description { get; set; }
   
   public ComponentReference<AllLangComponent>? Parent { get; set; }
}