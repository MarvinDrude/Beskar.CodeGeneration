using Beskar.CodeGeneration.ContentGenerator.Models;
using Beskar.CodeGeneration.Extensions.Common;
using Beskar.CodeGeneration.Extensions.Common.Symbols;
using Beskar.CodeGeneration.Extensions.Diagnostics;
using Beskar.CodeGeneration.Extensions.Models.Diagnostics;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.ContentGenerator;

public sealed partial class ContentGenerator
{
   private static MaybeSpec<ContentTypeSpec> Transform(
      GeneratorAttributeSyntaxContext context,
      CancellationToken ct)
   {
      ct.ThrowIfCancellationRequested();

      var symbol = (INamedTypeSymbol)context.TargetSymbol;
      var attributes = symbol.GetAttributes();

      if (GetContentTypeAttribute(attributes) is not { } contentTypeAttribute)
      {
         return DiagnosticBuilder<ContentTypeSpec>.CreateEmpty();
      }
      
      ct.ThrowIfCancellationRequested();
      using var builder = DiagnosticBuilder<ContentTypeSpec>.Create(8);
      var archetype = symbol.CreateNamedArchetype();
      
      
      
      return builder.Build(new ContentTypeSpec(
         archetype,
         contentTypeAttribute.DetermineEnumFullName("Kind", 0) ?? "Unknown"));
   }  
}