using Beskar.CodeGeneration.Extensions.Interfaces.Specs;

namespace Beskar.CodeGeneration.ProcessorGenerator.Models;

public sealed record SettingSpec : IAttributeSpec
{
   public required string Name { get; init; }
   
   public required string ValueFullExpression { get; init; }
   
   public required string PropertyName { get; init; }
   
   public bool Equals(IAttributeSpec? other)
   {
      return other is SettingSpec spec && Equals(spec);
   }
}