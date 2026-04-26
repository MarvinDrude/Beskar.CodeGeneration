using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.ProcessorGenerator;

[Generator]
public sealed partial class ProcessorGenerator : IIncrementalGenerator
{
   public const string GeneratorName = "ProcessorGenerator";
   public const string GeneratorVersion = "1.2.0";
   
   public void Initialize(IncrementalGeneratorInitializationContext context)
   {
      var assemblyNameProvider = context.CompilationProvider
         .Select(static (c, _) => c.AssemblyName?
            .Replace(" ", string.Empty)
            .Replace(".", string.Empty)
            .Trim() ?? "UnknownAssembly");

      var maybeProcessorProvider = context.SyntaxProvider
         .ForAttributeWithMetadataName(
            ProcessorAttributeFullName,
            predicate: static (_, _) => true,
            transform: TransformProcessor);
      
      var maybePipelineProvider = context.SyntaxProvider
         .ForAttributeWithMetadataName(
            ProcessorPipelineAttributeFullName,
            predicate: static (_, _) => true,
            transform: Transform);
      
      var combinedProcessors = maybeProcessorProvider
         .Combine(assemblyNameProvider);
      
      var combinedPipeline = maybePipelineProvider
         .Combine(assemblyNameProvider);
      
      context.RegisterSourceOutput(combinedProcessors, static (ctx, source) 
         => RenderProcessor(ctx, source.Right, source.Left));
      
      context.RegisterSourceOutput(combinedPipeline, static (ctx, source) 
         => RenderPipeline(ctx, source.Right, source.Left));
      
      context.RegisterPostInitializationOutput(static ctx =>
      {
         ctx.AddSource($"{GeneratorName}.g.cs", $"// Version {GeneratorVersion}");
      });
   }
}