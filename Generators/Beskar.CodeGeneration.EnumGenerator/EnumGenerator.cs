using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.EnumGenerator;

[Generator]
public sealed partial class EnumGenerator : IIncrementalGenerator
{
   public const string GeneratorName = "EnumGenerator";
   public const string GeneratorVersion = "1.1.7";
   
   public void Initialize(IncrementalGeneratorInitializationContext context)
   {
      
   }
}