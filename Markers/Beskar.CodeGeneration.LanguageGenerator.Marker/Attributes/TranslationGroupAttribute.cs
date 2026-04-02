namespace Beskar.CodeGeneration.LanguageGenerator.Marker.Attributes;

[AttributeUsage(AttributeTargets.Enum)]
public sealed class TranslationGroupAttribute(
   string? groupName = null)
   : Attribute
{
   public string? GroupName { get; init; } = groupName;
}