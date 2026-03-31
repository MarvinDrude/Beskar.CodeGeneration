namespace Beskar.CodeGeneration.Extensions.Common.System;

public static class StringExtensions
{
   extension(string str)
   {
      public string FirstCharToLower()
      {
         if (str.Length == 0 || char.IsLower(str[0])) 
            return str;
         
         return char.ToLowerInvariant(str[0]) + str[1..];
      }
      
      public string FirstCharToUpper()
      {
         if (str.Length == 0 || char.IsUpper(str[0])) 
            return str;
         
         return char.ToUpperInvariant(str[0]) + str[1..];
      }
   }
}