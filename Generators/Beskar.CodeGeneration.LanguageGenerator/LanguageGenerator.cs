using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.LanguageGenerator;

[Generator]
public sealed partial class LanguageGenerator : IIncrementalGenerator
{
   public void Initialize(IncrementalGeneratorInitializationContext context)
   {
      var assemblyNameProvider = context.CompilationProvider
         .Select(static (c, _) => c.AssemblyName?
            .Replace(" ", string.Empty)
            .Replace(".", string.Empty)
            .Trim() ?? "UnknownAssembly");
      
      var maybeLanguageEnumSpecProvider = context.SyntaxProvider
         .ForAttributeWithMetadataName(
            TranslationGroupAttributeFullName,
            predicate: static (_, _) => true,
            transform: Transform);
      
      
   }
}