using Beskar.CodeGeneration.ContentGenerator.Models.Attributes;

namespace Beskar.CodeGeneration.ContentGenerator.Models.Fields;

public sealed record StringFieldSpec : FieldSpec
{
   public StringOptionsSpec? Options { get; init; }
   
   public override string NativePropertyType => "string";
}