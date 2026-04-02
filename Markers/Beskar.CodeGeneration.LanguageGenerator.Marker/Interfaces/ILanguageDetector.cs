namespace Beskar.CodeGeneration.LanguageGenerator.Marker.Interfaces;

public interface ILanguageDetector
{
   public int Priority { get; }

   public string? GetLanguageCode(bool isTwoLetterCode);
}