namespace Beskar.CodeGeneration.ContentGenerator.Models.Fields;

public sealed record BooleanFieldSpec : FieldSpec
{
   public override string NativePropertyType => "bool";
}