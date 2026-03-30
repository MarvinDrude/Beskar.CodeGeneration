using Beskar.CodeGeneration.Extensions.Models.Symbols;

namespace Beskar.CodeGeneration.Extensions.Transformers.Symbols.Options;

public sealed class ParameterTransformOptions 
   : SymbolBaseTransformOptions<ParameterSymbolLoadFlags>
{
   public static ParameterTransformOptions Minimal => new()
   {
      Depth = 1,
      Load = new ParameterSymbolLoadFlags()
      {
         Type = false,
      }
   };
   
   public static ParameterTransformOptions Full => new()
   {
      Depth = 1,
      Load = new ParameterSymbolLoadFlags()
      {
         Type = true,
      }
   };
}