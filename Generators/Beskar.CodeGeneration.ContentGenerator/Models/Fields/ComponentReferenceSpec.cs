using Beskar.CodeGeneration.ContentGenerator.Models.Attributes;

namespace Beskar.CodeGeneration.ContentGenerator.Models.Fields;

public sealed record ComponentReferenceSpec : FieldSpec
{
   public ComponentOptionsSpec? Options { get; init; }
}