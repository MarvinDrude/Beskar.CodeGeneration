using Beskar.CodeGeneration.Extensions.Interfaces.Specs;

namespace Beskar.CodeGeneration.ProcessorGenerator.Models;

public sealed record StepSpec : IAttributeSpec
{
   public required int Order { get; init; }
   
   public bool Equals(IAttributeSpec? other)
   {
      return other is StepSpec spec && Equals(spec);
   }
}