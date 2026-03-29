using Beskar.CodeGeneration.Extensions.Common;
using Beskar.CodeGeneration.Extensions.Diagnostics;
using Beskar.CodeGeneration.Extensions.Models.Diagnostics;
using Beskar.CodeGeneration.TypeIdGenerator.Models;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.TypeIdGenerator;

public sealed partial class TypeIdGenerator
{
   private static MaybeSpec<TypeSafeIdSpec> Transform(
      GeneratorAttributeSyntaxContext context,
      CancellationToken ct)
   {
      ct.ThrowIfCancellationRequested();

      var symbol = (INamedTypeSymbol)context.TargetSymbol;
      var attributes = symbol.GetAttributes();

      if (GetTypeSafeIdAttribute(attributes) is not { } attribute)
      {
         return DiagnosticBuilder<TypeSafeIdSpec>.CreateEmpty();
      }
      
      ct.ThrowIfCancellationRequested();
      
      using var builder = DiagnosticBuilder<TypeSafeIdSpec>.Create(8);
      var attributeSpec = GetAttributeSpec(attribute);
      
      
      
      return builder.Build(new TypeSafeIdSpec(attributeSpec));
   }
   
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