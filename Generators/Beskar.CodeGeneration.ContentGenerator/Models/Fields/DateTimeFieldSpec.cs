namespace Beskar.CodeGeneration.ContentGenerator.Models.Fields;

public sealed record DateTimeFieldSpec : FieldSpec
{
   public override string NativePropertyType => "DateTimeOffset";
}