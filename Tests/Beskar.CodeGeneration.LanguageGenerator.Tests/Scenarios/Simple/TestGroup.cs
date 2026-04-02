using Beskar.CodeGeneration.LanguageGenerator.Marker.Attributes;

namespace Beskar.CodeGeneration.LanguageGenerator.Tests.Scenarios.Simple;

[TranslationGroup("TestGr")]
public enum TestGroup
{
   [TranslationKey(defaultValue: "Welcome here")]
   Title = 1
}