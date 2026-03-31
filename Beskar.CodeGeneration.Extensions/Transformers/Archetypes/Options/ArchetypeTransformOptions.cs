using Beskar.CodeGeneration.Extensions.Transformers.Symbols.Options;

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
}