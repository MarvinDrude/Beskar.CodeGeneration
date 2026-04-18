namespace Beskar.CodeGeneration.ContentGenerator.Models;

public record FieldSpec
{
   public required bool IsLocalized { get; init; }
   
   public required string PropertyName { get; init; }

   public required bool IsRequired { get; init; }
   
   public virtual string NativePropertyType => "object";
}