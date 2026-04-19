using System.Diagnostics.CodeAnalysis;
using Beskar.CodeGeneration.ContentGenerator.Marker.Interfaces;

namespace Beskar.CodeGeneration.ContentGenerator.Marker.Fields;

public sealed class LocalizedField<T>
   where T : IContentField
{
   public required T Value { get; set; }

   private Dictionary<string, T> LocalizedValues => field ??= [];

   public bool TryGetLocalizedValue(string language, [MaybeNullWhen(false)] out T value)
   {
      if (LocalizedValues.TryGetValue(language, out value))
      {
         return true;
      }

      value = default;
      return false;
   }
}