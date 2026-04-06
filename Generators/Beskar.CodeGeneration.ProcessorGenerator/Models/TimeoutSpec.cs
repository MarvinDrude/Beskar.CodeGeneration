using Beskar.CodeGeneration.Extensions.Interfaces.Specs;

namespace Beskar.CodeGeneration.ProcessorGenerator.Models;

public sealed record TimeoutSpec : IAttributeSpec
{
   public required int Milliseconds { get; init; }
   
   public bool Equals(IAttributeSpec? other)
   {
      return other is TimeoutSpec spec && Milliseconds == spec.Milliseconds;
   }
}