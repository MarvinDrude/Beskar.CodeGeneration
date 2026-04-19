using Beskar.CodeGeneration.ContentGenerator.Models.Attributes;

namespace Beskar.CodeGeneration.ContentGenerator.Models.Fields;

public sealed record ComponentReferenceSpec : FieldSpec
{
   public ComponentOptionsSpec? Options { get; init; }
   
   public required string FullName { get; init; }
   
   public override string NativePropertyType => "ContentTypeId";
}