using Beskar.CodeGeneration.Extensions.Models.Symbols;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes.Options;
using Beskar.CodeGeneration.Extensions.Transformers.Symbols;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Common.Symbols;

public static class SymbolExtensions
{
   extension<TSymbol>(TSymbol symbol)
      where TSymbol : ISymbol
   {
      public SymbolSpec CreateSpec(ArchetypeTransformOptions? options = null)
      {
         return SymbolSpecTransformer.Transform(symbol, options: options);
      }
      
      public List<ISymbol> ExplicitOrImplicitInterfaceImplementations()
      {
         if (symbol.Kind != SymbolKind.Method &&
             symbol.Kind != SymbolKind.Property &&
             symbol.Kind != SymbolKind.Event)
         {
            return [];
         }
      
         var containingType = symbol.ContainingType;

         return containingType
            .AllInterfaces
            .SelectMany(iface => iface.GetMembers())
            .Where(interfaceMember =>
               SymbolEqualityComparer.Default.Equals(
                  symbol, 
                  containingType.FindImplementationForInterfaceMember(interfaceMember)))
            .ToList();
      }
   }
}