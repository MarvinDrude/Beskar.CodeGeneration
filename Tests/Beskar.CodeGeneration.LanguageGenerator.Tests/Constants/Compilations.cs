using Basic.Reference.Assemblies;
using Beskar.CodeGeneration.Tests.Utils.Compilations;

namespace Beskar.CodeGeneration.LanguageGenerator.Tests.Constants;

public static class Compilations
{
   public static TestCompilationCreator Create() =>
      new TestCompilationCreator()
         .WithAssemblyName("Test-Assembly")
         .WithReferences(Net100.References.All)
         .SuppressDiagnostics("CS1591", "CS9113");
}