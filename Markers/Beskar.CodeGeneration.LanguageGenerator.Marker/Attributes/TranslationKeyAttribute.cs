namespace Beskar.CodeGeneration.LanguageGenerator.Marker.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public sealed class TranslationKeyAttribute(
   string? keyName = null,
   string? defaultValue = null)
   : Attribute
{
   public string? KeyName { get; } = keyName;
   
   public string? DefaultValue { get; } = defaultValue;
}