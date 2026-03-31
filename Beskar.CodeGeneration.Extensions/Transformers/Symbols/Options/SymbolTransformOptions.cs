using Beskar.CodeGeneration.Extensions.Models.Symbols;

namespace Beskar.CodeGeneration.Extensions.Transformers.Symbols.Options;

public sealed class SymbolTransformOptions 
   : SymbolBaseTransformOptions<SymbolLoadFlags>
{
   public static SymbolTransformOptions Minimal => new()
   {
      Depth = 1,
      Load = new SymbolLoadFlags()
      {
         Attributes = false,
      }
   };
   
   public static SymbolTransformOptions Full => new()
   {
      Depth = int.MaxValue,
      Load = new SymbolLoadFlags()
      {
         Attributes = false, // it's a duplication often
      }
   };
}