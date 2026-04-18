using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.ContentGenerator;

[Generator]
public sealed partial class ContentGenerator : IIncrementalGenerator
{
   public const string GeneratorName = "ContentGenerator";
   public const string GeneratorVersion = "1.1.8";
   
   public void Initialize(IncrementalGeneratorInitializationContext context)
   {
      throw new NotImplementedException();
   }
}