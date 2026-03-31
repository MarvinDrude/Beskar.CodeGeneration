using Beskar.CodeGeneration.Extensions.Models.Symbols;

namespace Beskar.CodeGeneration.Extensions.Transformers.Symbols.Options;

public sealed class PropertyTransformOptions 
   : SymbolBaseTransformOptions<PropertySymbolLoadFlags>
{
   public static PropertyTransformOptions Minimal => new()
   {
      Depth = 1,
      Load = new PropertySymbolLoadFlags()
      {
         Type = false,
         Getter = false,
         Setter = false,
         Attributes = false,
      }
   };
   
   public static PropertyTransformOptions Full => new()
   {
      Depth = 1,
      Load = new PropertySymbolLoadFlags()
      {
         Type = true,
         Getter = true,
         Setter = true,
         Attributes = true,
      }
   };
}