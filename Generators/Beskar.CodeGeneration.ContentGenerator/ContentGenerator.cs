using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.ContentGenerator;

[Generator]
public sealed partial class ContentGenerator : IIncrementalGenerator
{
   public void Initialize(IncrementalGeneratorInitializationContext context)
   {
      throw new NotImplementedException();
   }
}