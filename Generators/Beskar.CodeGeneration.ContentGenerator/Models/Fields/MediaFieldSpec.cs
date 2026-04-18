using Beskar.CodeGeneration.ContentGenerator.Models.Attributes;

namespace Beskar.CodeGeneration.ContentGenerator.Models.Fields;

public sealed record MediaFieldSpec : FieldSpec
{
   public MediaOptionsSpec? Options { get; init; }
}