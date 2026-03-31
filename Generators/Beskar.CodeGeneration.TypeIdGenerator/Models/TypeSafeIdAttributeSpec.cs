using Beskar.CodeGeneration.Extensions.Interfaces.Specs;

namespace Beskar.CodeGeneration.TypeIdGenerator.Models;

public readonly record struct TypeSafeIdAttributeSpec(
   bool IsOverrideString,
   bool AddImplicitConversions,
   bool AddExplicitConversions,
   bool IsSpanParsable,
   bool AddJsonConverter)
   : IAttributeSpec
{
   public bool Equals(IAttributeSpec? other)
   {
      return other is TypeSafeIdAttributeSpec spec && Equals(spec);
   }
}