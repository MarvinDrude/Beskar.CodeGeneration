using Beskar.CodeGeneration.ContentGenerator.Enums;
using Beskar.CodeGeneration.ContentGenerator.Models;
using Beskar.CodeGeneration.Extensions.Common;
using Beskar.CodeGeneration.Extensions.Common.Symbols;
using Beskar.CodeGeneration.Extensions.Diagnostics;
using Beskar.CodeGeneration.Extensions.Models.Diagnostics;
using Me.Memory.Buffers;
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
      
      var fullKindName = contentTypeAttribute.DetermineEnumFullName("Kind", 0) ?? "Unknown";
      var kind = GetContentTypeKind(fullKindName);
      
      ct.ThrowIfCancellationRequested();
      
      if (!IsContentTypeCorrectDerivedType(symbol, GetDerivedTypeName(kind)))
      {
         return builder.Add(InvalidTargetDiagnosticId).Build();
      }

      using var fields = new ArrayBuilder<FieldSpec>(12);
      var properties = symbol.GetAllMembers()
         .OfType<IPropertySymbol>()
         .Where(p => p is { IsStatic: false, IsReadOnly: false });

      foreach (var property in properties)
      {
         
      }
      
      return builder.Build(new ContentTypeSpec(
         archetype, kind, [.. fields.WrittenSpan]));
   }

   private static bool IsContentTypeCorrectDerivedType(INamedTypeSymbol symbol, string search)
   {
      while (true)
      {
         if (IsRelevantModel(symbol) && symbol.Name == search)
         {
            return true;
         }

         if (symbol.BaseType is { SpecialType: SpecialType.System_Object } or null)
         {
            return false;
         }

         symbol = symbol.BaseType;
      }
   }

   private static string GetDerivedTypeName(ContentTypeKind kind)
   {
      return kind is ContentTypeKind.Component ? "ComponentBase" : "ContentTypeBase";
   }

   private static ContentTypeKind GetContentTypeKind(string fullName)
   {
      return fullName switch
      {
         "Beskar.CodeGeneration.ContentGenerator.Marker.Enums.ContentKind.Single" => ContentTypeKind.Single,
         "Beskar.CodeGeneration.ContentGenerator.Marker.Enums.ContentKind.Component" => ContentTypeKind.Component,
         _ => ContentTypeKind.Collection
      };
   }
}