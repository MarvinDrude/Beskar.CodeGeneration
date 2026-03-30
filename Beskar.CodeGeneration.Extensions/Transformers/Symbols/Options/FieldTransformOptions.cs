using Beskar.CodeGeneration.Extensions.Models.Symbols;
using Me.Memory.Buffers.Dynamic;

namespace Beskar.CodeGeneration.Extensions.Transformers.Symbols.Options;

public sealed class FieldTransformOptions 
   : SymbolBaseTransformOptions<FieldSymbolLoadFlags>
{
   public static FieldTransformOptions Minimal => new()
   {
      Depth = 1,
      Load = new FieldSymbolLoadFlags()
      {
         Type = false
      }
   };

   public static FieldTransformOptions Full => new()
   {
      Depth = 1,
      Load = new FieldSymbolLoadFlags()
      {
         Type = true
      }
   };
}