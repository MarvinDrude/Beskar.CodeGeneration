using Beskar.CodeGeneration.Extensions.Models.Symbols;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Transformers.Symbols.Options;

public sealed class NamedTypeTransformOptions 
   : SymbolBaseTransformOptions<NamedTypeSymbolLoadFlags>
{
   public Func<IMethodSymbol, bool>? MethodFilter { get; set; }
   
   public static NamedTypeTransformOptions Minimal => new()
   {
      Depth = 1,
      Load = new NamedTypeSymbolLoadFlags()
      {
         Methods = false,
         TypeArgumentNullableAnnotations = false,
         TypeArguments = false,
         TypeParameters = false,
         Attributes = false,
         Properties = false,
         Fields = false,
      }
   };

   public static NamedTypeTransformOptions Full => new()
   {
      Depth = 1,
      Load = new NamedTypeSymbolLoadFlags()
      {
         Methods = true,
         TypeArgumentNullableAnnotations = true,
         TypeArguments = true,
         TypeParameters = true,
         Attributes = true,
         Properties = true,
         Fields = true,
      }
   };
}