using Beskar.CodeGeneration.Extensions.Interfaces.Specs;

namespace Beskar.CodeGeneration.ProcessorGenerator.Models;

public sealed record ContextVariableSpec : IAttributeSpec
{
   public required string Name { get; init; }
   
   public required string TypeFullName { get; init; }
   
   public bool Equals(IAttributeSpec? other)
   {
      return other is ContextVariableSpec spec && Equals(spec);
   }
}