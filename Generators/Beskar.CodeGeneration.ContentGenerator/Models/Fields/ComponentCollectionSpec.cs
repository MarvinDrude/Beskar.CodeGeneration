using Beskar.CodeGeneration.ContentGenerator.Models.Attributes;

namespace Beskar.CodeGeneration.ContentGenerator.Models.Fields;

public sealed record ComponentCollectionSpec : FieldSpec
{
   public ComponentsOptionsSpec? Options { get; init; }
}