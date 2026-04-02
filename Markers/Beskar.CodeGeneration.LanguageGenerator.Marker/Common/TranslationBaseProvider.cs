using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using Beskar.CodeGeneration.LanguageGenerator.Marker.Interfaces;

namespace Beskar.CodeGeneration.LanguageGenerator.Marker.Common;

public abstract class TranslationBaseProvider(IEnumerable<ILanguageDetector> detectors) 
   : ITranslationProvider
{
   public string CurrentLanguageTwoLetterCode => GetCurrentLanguageCode(true);
   public string CurrentLanguageCode => GetCurrentLanguageCode(false);

   private readonly ConcurrentDictionary<string, TranslationKeyValueStore> _languageCache = [];
   private readonly ILanguageDetector[] _detectors = detectors.OrderByDescending(d => d.Priority).ToArray();

   [MethodImpl(MethodImplOptions.AggressiveInlining)]
   public string Get(string fullKey)
   {
      return Get(fullKey.AsSpan());
   }
   
   [MethodImpl(MethodImplOptions.AggressiveInlining)]
   public string Get(scoped in ReadOnlySpan<char> fullKeySpan)
   {
      var store = _languageCache.GetValueOrDefault(CurrentLanguageCode);
      store ??= _languageCache.GetValueOrDefault(CurrentLanguageTwoLetterCode);
      
      if (store is null) 
         return fullKeySpan.ToString();

      var alternateLookup = store.KeyValues.GetAlternateLookup<ReadOnlySpan<char>>();
      
      return alternateLookup.TryGetValue(fullKeySpan, out var value)
         ? value : fullKeySpan.ToString();
   }
   
   public abstract ValueTask PopulateCache(CancellationToken cancellationToken = default);

   protected void AddToCache(string languageCode, Dictionary<string, string> keyValues)
   {
      _languageCache[languageCode] = new TranslationKeyValueStore { KeyValues = keyValues };
   }
   
   protected void AddToCache(string languageCode, TranslationKeyValueStore store)
   {
      _languageCache[languageCode] = store;
   }
   
   private string GetCurrentLanguageCode(bool isTwoLetterCode)
   {
      if (_detectors[0].GetLanguageCode(isTwoLetterCode) is { Length: > 0 } code)
      {
         // first detector fast path
         return code;
      }

      for (var e = 1; e < _detectors.Length; e++)
      {
         if (_detectors[e].GetLanguageCode(isTwoLetterCode) is { Length: > 0 } secondary)
         {
            return secondary;
         }
      }
      
      throw new InvalidOperationException("No language code found");
   }
}