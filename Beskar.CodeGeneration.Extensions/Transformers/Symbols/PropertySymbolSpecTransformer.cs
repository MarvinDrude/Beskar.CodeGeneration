using Beskar.CodeGeneration.Extensions.Models.Symbols;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes.Options;
using Beskar.CodeGeneration.Extensions.Transformers.Symbols.Options;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Transformers.Symbols;

public static class PropertySymbolSpecTransformer
{
   public static PropertySymbolSpec Transform(
      IPropertySymbol propertySymbol,
      int depth = 1,
      ArchetypeTransformOptions? options = null)
   {
      return new PropertySymbolSpec()
      {
         RefKind = propertySymbol.RefKind,
         
         IsReadOnly = propertySymbol.IsReadOnly,
         HasGetter = propertySymbol.GetMethod is not null,
         HasSetter = propertySymbol.SetMethod is not null,
         IsIndexer = propertySymbol.IsIndexer,
         IsRequired = propertySymbol.IsRequired,
      };
   }
}