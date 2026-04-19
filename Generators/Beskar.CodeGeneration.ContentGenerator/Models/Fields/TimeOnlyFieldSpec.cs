namespace Beskar.CodeGeneration.ContentGenerator.Models.Fields;

public sealed record TimeOnlyFieldSpec : FieldSpec
{
   public override string NativePropertyType => "TimeOnly";
}