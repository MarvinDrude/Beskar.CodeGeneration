using Beskar.CodeGeneration.ContentGenerator.Marker.Attributes;
using Beskar.CodeGeneration.ContentGenerator.Marker.Enums;
using Beskar.CodeGeneration.ContentGenerator.Marker.Fields;
using Beskar.CodeGeneration.ContentGenerator.Marker.Models;
using ContentTypeId = Beskar.CodeGeneration.ContentGenerator.Marker.Fields.ContentTypeId;

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
   
   public required LocalizedField<ComponentCollection<AllLangComponent>> AllLangComponents { get; set; }
   
   public ComponentCollection<TestComponent>? TestComponents { get; set; }
   
   public required BooleanField IsActive { get; set; }
   
   public required DateTimeField CreatedAt { get; set; }
   
   public required DateOnlyField DateOnly { get; set; }
   
   public TimeOnlyField? TimeOnly { get; set; }
   
   public NumberField<int>? NumberInteger { get; set; }
   
   public required NumberField<double> NumberDouble { get; set; }
   
   [MediaOptions(["jpg", "png", "gif"], maxCount: 2)]
   public MediaField? Images { get; set; }
}

[ContentType(ContentKind.Component)]
public sealed class TestComponent : ComponentBase
{
   [UniqueId]
   public required ContentTypeId Id { get; set; }
   
   public ComponentReference<TestComponent>? Parent { get; set; }
   
   [StringOptions(StringFieldKind.Markdown)]
   public required LocalizedField<StringField> Title { get; set; }
}

[ContentType(ContentKind.Component)]
public sealed class AllLangComponent : ComponentBase
{
   [UniqueId]
   public required ContentTypeId Id { get; set; }
   
   [StringOptions(StringFieldKind.SingleLine)]
   public StringField? Title { get; set; }
   
   [StringOptions(StringFieldKind.Markdown)]
   public StringField? Description { get; set; }
   
   public ComponentReference<AllLangComponent>? Parent { get; set; }
   
   [ComponentOptions(baseType: typeof(SuperComponent))]
   public required ComponentReference<SmallComponent> Super { get; set; }
   
   [ComponentOptions(baseType: typeof(SuperComponent))]
   public required ComponentReference<SuperComponent> SuperSuper { get; set; }
}

[ContentType(ContentKind.Component)]
public class SuperComponent : ComponentBase
{
   [UniqueId]
   public required ContentTypeId Id { get; set; }
   
   public required StringField Title { get; set; }
}

[ContentType(ContentKind.Component)]
public sealed class SmallComponent : SuperComponent
{
   public required StringField Description { get; set; }
}

[ContentType(ContentKind.Component)]
public sealed class SmallTwoComponent : SuperComponent
{
   public required StringField ImageName { get; set; }
}