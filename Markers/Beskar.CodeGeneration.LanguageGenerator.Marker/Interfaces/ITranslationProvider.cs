namespace Beskar.CodeGeneration.LanguageGenerator.Marker.Interfaces;

public interface ITranslationProvider
{
   public string CurrentLanguageTwoLetterCode { get; }
   public string CurrentLanguageCode { get; }

   public string Get(string fullKey);
   public string Get(scoped in ReadOnlySpan<char> fullKeySpan);
   
   public ValueTask PopulateCache(CancellationToken cancellationToken = default);
}