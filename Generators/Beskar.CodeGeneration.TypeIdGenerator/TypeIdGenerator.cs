using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.TypeIdGenerator;

public sealed partial class TypeIdGenerator : IIncrementalGenerator
{
   public void Initialize(IncrementalGeneratorInitializationContext context)
   {
      var assemblyNameProvider = context.CompilationProvider
         .Select(static (c, _) => c.AssemblyName?
            .Replace(" ", string.Empty)
            .Replace(".", string.Empty)
            .Trim() ?? "UnknownAssembly");
      
      
   }
}