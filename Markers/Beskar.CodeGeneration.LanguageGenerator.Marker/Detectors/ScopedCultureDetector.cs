using System.Globalization;
using Beskar.CodeGeneration.LanguageGenerator.Marker.Interfaces;

namespace Beskar.CodeGeneration.LanguageGenerator.Marker.Detectors;

public sealed class ScopedCultureDetector : ILanguageDetector
{
   private static readonly AsyncLocal<CultureScope?> _currentScope = new ();
   private static CultureScope? CurrentScope
   {
      get => _currentScope.Value;
      set => _currentScope.Value = value;
   }
   
   public int Priority => 100;
   
   public string? GetLanguageCode(bool isTwoLetterCode)
   {
      var current = CurrentScope;
      if (current is null) return null;

      return isTwoLetterCode ? current.TwoLetterCode : current.LanguageCode;
   }

   public static IDisposable BeginScope(CultureInfo cultureInfo)
   {
      return BeginScope(cultureInfo.Name, cultureInfo.TwoLetterISOLanguageName);
   }

   public static IDisposable BeginScope(string languageCode, string twoLetterCode)
   {
      var previous = CurrentScope;
      CurrentScope = new CultureScope(languageCode, twoLetterCode);
      
      return new CultureScopeDisposer(previous);
   }

   private sealed class CultureScopeDisposer(CultureScope? previous) : IDisposable
   {
      public void Dispose()
      {
         CurrentScope = previous;
      }
   };

   private sealed record CultureScope(string LanguageCode, string TwoLetterCode);
}