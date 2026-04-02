using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Beskar.CodeGeneration.LanguageGenerator.Marker.Common;
using Beskar.CodeGeneration.LanguageGenerator.Marker.Interfaces;

namespace Beskar.CodeGeneration.LanguageGenerator.Marker.Providers;

public sealed class JsonTranslationProvider(
   IEnumerable<ILanguageDetector> detectors) 
   : TranslationBaseProvider(detectors)
{
   private const string JsonFileNameTemplate = "Translations.{0}.json";

   [MemberNotNullWhen(true, nameof(_folderPath))]
   private bool IsInitialized => _folderPath is not null;
   
   private string? _folderPath;
   
   public void Initialize(string folderPath)
   {
      _folderPath = folderPath;
   }
   
   public override async ValueTask PopulateCache(CancellationToken cancellationToken = default)
   {
      if (!IsInitialized)
      {
         throw new InvalidOperationException("JsonTranslationProvider is not initialized.");
      }
      
      var directory = new DirectoryInfo(_folderPath);
      if (!directory.Exists)
      {
         throw new DirectoryNotFoundException($"The path '{_folderPath}' does not exist.");
      }

      var files = directory.GetFiles("*.json");
      foreach (var file in files)
      {
         if (ExtractLanguageCode(file.Name) is not { Length: > 0 } languageCode)
         {
            continue;
         }

         await using var stream = file.OpenRead();
         
         var data = await JsonSerializer.DeserializeAsync<Dictionary<string, string>>(stream, _options, cancellationToken);
         if (data is null) continue;

         AddToCache(languageCode, data);
      }
   }

   private static string? ExtractLanguageCode(string fileName)
   {
      var span = fileName.AsSpan();
      
      var firstDot = span.IndexOf('.');
      if (firstDot == -1) return null;
      
      var lastDot = span.LastIndexOf('.');
      if (lastDot == -1 || lastDot <= firstDot + 1) return null;
      
      var cultureSpan = span.Slice(firstDot + 1, lastDot - firstDot - 1);
      return cultureSpan.ToString();
   }

   private static readonly JsonSerializerOptions _options = new()
   {
      PropertyNameCaseInsensitive = true,
   };
}