using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Common.Enums;

public static class AccessibilityExtensions
{
   extension(Accessibility access)
   {
      public string ToKeywordString()
      {
         return access switch
         {
            Accessibility.Public => "public",
            Accessibility.Private => "private",
            Accessibility.Protected => "protected",
            Accessibility.Internal => "internal",
            Accessibility.ProtectedOrInternal => "protected internal",
            _ => "private"
         };
      }
   }
}