using Beskar.CodeGeneration.Extensions.Models.Symbols;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes.Options;
using Beskar.CodeGeneration.Extensions.Transformers.Symbols.Options;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Transformers.Symbols;

public static class FieldSymbolSpecTransformer
{
   public static FieldSymbolSpec Transform(
      IFieldSymbol fieldSymbol,
      int depth = 1,
      ArchetypeTransformOptions? options = null)
   {
      options ??= new ArchetypeTransformOptions();
      
      var spec = new FieldSymbolSpec()
      {
         RefKind = fieldSymbol.RefKind,
         
         IsReadOnly = fieldSymbol.IsReadOnly,
         IsRequired = fieldSymbol.IsRequired,
         IsVolatile = fieldSymbol.IsVolatile,
         HasConstantValue = fieldSymbol.HasConstantValue,
         IsConst = fieldSymbol.IsConst,
      };

      if (depth > options.Fields.Depth) 
         return spec;
      
      if (options.Fields.Load.Type)
         spec.Type = TypeSymbolArchetypeTransformer.Transform(
            fieldSymbol.Type, );
      
      return spec;
   }
}