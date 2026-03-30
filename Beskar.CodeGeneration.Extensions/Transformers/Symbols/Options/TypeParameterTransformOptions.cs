using Beskar.CodeGeneration.Extensions.Models.Symbols;

namespace Beskar.CodeGeneration.Extensions.Transformers.Symbols.Options;

public sealed class TypeParameterTransformOptions 
   : SymbolBaseTransformOptions<TypeParameterSymbolLoadFlags>
{
   public static TypeParameterTransformOptions Minimal => new()
   {
      Depth = 1,
      Load = new TypeParameterSymbolLoadFlags()
      {
         ConstraintTypes = false
      }
   };

   public static TypeParameterTransformOptions Full => new()
   {
      Depth = 1,
      Load = new TypeParameterSymbolLoadFlags()
      {
         ConstraintTypes = true
      }
   };
}