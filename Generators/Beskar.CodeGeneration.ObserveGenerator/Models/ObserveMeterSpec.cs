using Beskar.CodeGeneration.Extensions.Common;
using Beskar.CodeGeneration.Extensions.Interfaces.Specs;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.ObserveGenerator.Models;

public sealed record ObserveMeterSpec : IAttributeSpec
{
   public required string Name { get; init; }
   
   public required string Version { get; init; }
   
   public bool Equals(IAttributeSpec? other)
   {
      return other is ObserveMeterSpec spec && Equals(spec);
   }

   public static ObserveMeterSpec Create(ISymbol symbol, AttributeData attribute)
   {
      return new ObserveMeterSpec()
      {
         Name = attribute.DetermineStringValue("Name", 0) ?? symbol.Name,
         Version = attribute.DetermineStringValue("Version", 1) ?? "1.0.0"
      };
   }
}