using Beskar.CodeGeneration.ContentGenerator.Enums;

namespace Beskar.CodeGeneration.ContentGenerator.Models.Attributes;

public sealed record StringOptionsSpec
{
   public StringFieldKind Kind { get; init; }
   
   public int MaxLength { get; init; }

   public static StringFieldKind GetKind(string kind)
   {
      return kind switch
      {
         "Beskar.CodeGeneration.ContentGenerator.Marker.Enums.Markdown" => StringFieldKind.Markdown,
         "Beskar.CodeGeneration.ContentGenerator.Marker.Enums.MultiLine" => StringFieldKind.MultiLine,
         "Beskar.CodeGeneration.ContentGenerator.Marker.Enums.SingleLine" => StringFieldKind.SingleLine,
         _ => StringFieldKind.RawString
      };
   }
}