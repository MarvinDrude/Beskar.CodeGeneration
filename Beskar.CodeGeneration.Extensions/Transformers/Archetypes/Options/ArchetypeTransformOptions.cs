using Beskar.CodeGeneration.Extensions.Interfaces.Specs;
using Beskar.CodeGeneration.Extensions.Transformers.Symbols.Options;
using Me.Memory.Collections;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Transformers.Archetypes.Options;

public sealed class ArchetypeTransformOptions
{
   public FieldTransformOptions Fields { get; set; } = FieldTransformOptions.Minimal;
   
   public MethodTransformOptions Methods { get; set; } = MethodTransformOptions.Minimal;
   
   public PropertyTransformOptions Properties { get; set; } = PropertyTransformOptions.Minimal;
   
   public NamedTypeTransformOptions NamedTypes { get; set; } = NamedTypeTransformOptions.Minimal;
   
   public TypeParameterTransformOptions TypeParameters { get; set; } = TypeParameterTransformOptions.Minimal;
   
   public TypeTransformOptions Types { get; set; } = TypeTransformOptions.Minimal;
   
   public SymbolTransformOptions Symbols { get; set; } = SymbolTransformOptions.Minimal;
   
   public ParameterTransformOptions Parameters { get; set; } = ParameterTransformOptions.Minimal;

   private readonly Dictionary<string, Func<AttributeData, IAttributeSpec>> _attributeFactories = [];

   public bool IsAttributeRelevant(string? fullName)
   {
      return fullName is not null && _attributeFactories.ContainsKey(fullName);
   }
   
   public Func<AttributeData, IAttributeSpec> GetAttributeFactory(string fullName)
   {
      return _attributeFactories.GetValueOrDefault(fullName)
         ?? throw new InvalidOperationException("Attribute factory not found");
   }

   public SequenceArray<IAttributeSpec> GetAttributes(IEnumerable<AttributeData> attributes)
   {
      return [.. 
         attributes
            .Select(x => new { FullName = x.AttributeClass?.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat), Data = x })
            .Where(x => IsAttributeRelevant(x.FullName))
            .Select(x => GetAttributeFactory(x.FullName ?? string.Empty)(x.Data))
      ];
   }
   
   public ArchetypeTransformOptions RegisterAttribute<T>(string fullName, Func<AttributeData, T> factory)
      where T : IAttributeSpec
   {
      _attributeFactories.Add(fullName, data => factory(data));
      return this;
   }
}