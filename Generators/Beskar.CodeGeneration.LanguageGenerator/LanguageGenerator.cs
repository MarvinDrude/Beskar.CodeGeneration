using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.LanguageGenerator;

[Generator]
public sealed partial class LanguageGenerator : IIncrementalGenerator
{
   public const string GeneratorName = "LanguageGenerator";
   public const string GeneratorVersion = "1.1.4";
   
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
      
      var combined = maybeLanguageEnumSpecProvider
         .Combine(assemblyNameProvider);

      var collected = assemblyNameProvider
         .Combine(maybeLanguageEnumSpecProvider.Collect());
      
      context.RegisterSourceOutput(combined, static (ctx, source) 
         => Render(ctx, source.Right, source.Left));
      
      context.RegisterSourceOutput(collected, static (ctx, source) 
         => RenderExtensions(ctx, source.Left, source.Right));
      
      context.RegisterPostInitializationOutput(static ctx =>
      {
         ctx.AddSource($"{GeneratorName}.g.cs", $"// Version {GeneratorVersion}");
      });
   }
}