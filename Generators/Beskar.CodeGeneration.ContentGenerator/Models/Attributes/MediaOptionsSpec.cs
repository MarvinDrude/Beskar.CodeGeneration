namespace Beskar.CodeGeneration.ContentGenerator.Models.Attributes;

public sealed record MediaOptionsSpec
{
   public string?[]? AllowedExtensions { get; init; }
   
   public int MinCount { get; init; }
   
   public int MaxCount { get; init; }
}