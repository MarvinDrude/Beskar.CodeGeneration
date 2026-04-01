using Beskar.CodeGeneration.Extensions.Common;
using Beskar.CodeGeneration.Extensions.Common.Symbols;
using Beskar.CodeGeneration.Extensions.Diagnostics;
using Beskar.CodeGeneration.Extensions.Models.Diagnostics;
using Beskar.CodeGeneration.Extensions.Models.Symbols;
using Beskar.CodeGeneration.Extensions.Transformers.Archetypes.Options;
using Beskar.CodeGeneration.Extensions.Transformers.Symbols.Options;
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

      var namedInfo = symbol.CreateNamedArchetype(_transformOptions);
      ct.ThrowIfCancellationRequested();

      if (namedInfo.NamedType.Methods.Array is not [var constructor])
      {
         return builder.Add(InvalidTargetDiagnosticId).Build();
      }
      
      if (constructor.Method.Parameters.Array is not [var parameter])
      {
         return builder.Add(InvalidTargetDiagnosticId).Build();
      }

      if (parameter.Symbol.Name != "Value")
      {
         return builder.Add(InvalidTargetDiagnosticId).Build();
      }
      
      return builder.Build(new TypeSafeIdSpec(attributeSpec, namedInfo));
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

   private static readonly ArchetypeTransformOptions _transformOptions = CreateTransformOptions();
   private static ArchetypeTransformOptions CreateTransformOptions()
   {
      var options = new ArchetypeTransformOptions
      {
         NamedTypes =
         {
            MethodFilter = static (method) => 
               method.MethodKind is MethodKind.Constructor 
                  && method.Parameters.Length == 1,
            Load = new NamedTypeSymbolLoadFlags()
            {
               Methods = true,
            }
         },
         Methods = new MethodTransformOptions()
         {
            Depth = 2,
            Load = new MethodSymbolLoadFlags()
            {
               Parameters = true,
            }
         },
         Parameters = new ParameterTransformOptions()
         {
            Depth = 3,
            Load = new ParameterSymbolLoadFlags()
            {
               Type = true,
            }
         }
      };

      return options;
   }
}