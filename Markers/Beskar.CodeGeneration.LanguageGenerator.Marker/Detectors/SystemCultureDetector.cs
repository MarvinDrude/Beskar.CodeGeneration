using Beskar.CodeGeneration.LanguageGenerator.Marker.Interfaces;

namespace Beskar.CodeGeneration.LanguageGenerator.Marker.Detectors;

/// <summary>
/// Check the OS/Thread culture for language
/// </summary>
public sealed class SystemCultureDetector : ILanguageDetector
{
   public int Priority => 10; // Low priority

   public string? GetLanguageCode(bool isTwoLetterCode)
   {
      return isTwoLetterCode 
         ? System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName 
         : System.Globalization.CultureInfo.CurrentCulture.Name;
   }
}