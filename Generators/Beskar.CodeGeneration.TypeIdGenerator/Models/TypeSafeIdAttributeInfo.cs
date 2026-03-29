namespace Beskar.CodeGeneration.TypeIdGenerator.Models;

public readonly record struct TypeSafeIdAttributeInfo(
   bool IsOverrideString,
   bool AddImplicitConversions,
   bool AddExplicitConversions,
   bool IsSpanParsable,
   bool AddJsonConverter);