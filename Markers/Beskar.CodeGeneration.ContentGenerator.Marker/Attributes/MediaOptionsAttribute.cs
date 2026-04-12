namespace Beskar.CodeGeneration.ContentGenerator.Marker.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public sealed class MediaOptionsAttribute(
   string[]? allowedExtensions,
   int minCount = 0,
   int maxCount = 1) 
   : Attribute
{
   public string[]? AllowedExtensions { get; init; } = allowedExtensions;
   
   public int MinCount { get; init; } = minCount;
   
   public int MaxCount { get; init; } = maxCount;
}