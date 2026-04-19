namespace Beskar.CodeGeneration.ContentGenerator.Models.Fields;

public sealed record NumberFieldSpec : FieldSpec
{
   public required string NumberTypeName { get; set; }
   
   public override string NativePropertyType => NumberTypeName;
}