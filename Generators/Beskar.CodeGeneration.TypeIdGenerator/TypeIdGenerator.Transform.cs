using Beskar.CodeGeneration.Extensions.Common;
using Beskar.CodeGeneration.TypeIdGenerator.Models;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.TypeIdGenerator;

public sealed partial class TypeIdGenerator
{
   
   
   private static TypeSafeIdAttributeSpec GetAttributeSpec(AttributeData data)
   {
      return new TypeSafeIdAttributeSpec(
         data.DetermineBoolValue("IsOverrideString", 0),
         data.DetermineBoolValue("AddImplicitConversions", 1),
         data.DetermineBoolValue("AddExplicitConversions", 2),
         data.DetermineBoolValue("IsSpanParsable", 3),
         data.DetermineBoolValue("AddJsonConverter", 4));
   }
}