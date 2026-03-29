using Beskar.CodeGeneration.Extensions.Models.Symbols;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Transformers.Symbols;

public static class PropertySymbolSpecTransformer
{
   public static PropertySymbolSpec Transform(IPropertySymbol propertySymbol)
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