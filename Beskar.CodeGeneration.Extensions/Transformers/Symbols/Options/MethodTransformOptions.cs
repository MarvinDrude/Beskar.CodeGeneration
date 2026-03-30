using Beskar.CodeGeneration.Extensions.Models.Symbols;

namespace Beskar.CodeGeneration.Extensions.Transformers.Symbols.Options;

public sealed class MethodTransformOptions
   : SymbolBaseTransformOptions<MethodSymbolLoadFlags>
{
   public static MethodTransformOptions Minimal => new()
   {
      Depth = 1,
      Load = new MethodSymbolLoadFlags()
      {
         Parameters = false,
         ReturnType = false
      }
   };
   
   public static MethodTransformOptions Full => new()
   {
      Depth = 1,
      Load = new MethodSymbolLoadFlags()
      {
         Parameters = true,
         ReturnType = true
      }
   };
}