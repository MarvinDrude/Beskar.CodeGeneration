using Beskar.CodeGeneration.ContentGenerator.Models.Attributes;

namespace Beskar.CodeGeneration.ContentGenerator.Models.Fields;

public sealed record ComponentCollectionSpec : FieldSpec
{
   public ComponentsOptionsSpec? Options { get; init; }
   
   public required string FullName { get; init; }
   
   public override string NativePropertyType => "ContentTypeId";
}