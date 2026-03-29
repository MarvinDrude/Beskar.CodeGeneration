namespace Beskar.CodeGeneration.TypeIdGenerator.Models;

public readonly record struct TypeSafeIdAttributeSpec(
   bool IsOverrideString,
   bool AddImplicitConversions,
   bool AddExplicitConversions,
   bool IsSpanParsable,
   bool AddJsonConverter);