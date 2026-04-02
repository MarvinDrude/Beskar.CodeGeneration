using Beskar.CodeGeneration.Extensions.Interfaces.Specs;

namespace Beskar.CodeGeneration.LanguageGenerator.Models;

public sealed record LanguageKeySpec : IAttributeSpec
{
   public required string Name { get; init; }
   
   public required string DefaultValue { get; init; }
   
   public bool Equals(IAttributeSpec? other)
   {
      return other is LanguageKeySpec spec && Equals(spec);
   }
}