using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.ContentGenerator;

[Generator]
public sealed partial class ContentGenerator : IIncrementalGenerator
{
   public const string GeneratorName = "ContentGenerator";
   public const string GeneratorVersion = "1.2.1";
   
   public void Initialize(IncrementalGeneratorInitializationContext context)
   {
      var assemblyNameProvider = context.CompilationProvider
         .Select(static (c, _) => c.AssemblyName?
            .Replace(" ", string.Empty)
            .Replace(".", string.Empty)
            .Trim() ?? "UnknownAssembly");
      
      var contentTypeProvider = context.SyntaxProvider
         .ForAttributeWithMetadataName(
            ContentTypeAttributeFullName,
            predicate: static (_, _) => true,
            transform: Transform);
      
      var combined = contentTypeProvider.Collect()
         .Combine(assemblyNameProvider);
      
      context.RegisterSourceOutput(combined, static (ctx, source) 
         => Render(ctx, source.Right, source.Left));
      
      context.RegisterPostInitializationOutput(static ctx =>
      {
         ctx.AddSource($"{GeneratorName}.g.cs", $"// Version {GeneratorVersion}");
      });
   }
}