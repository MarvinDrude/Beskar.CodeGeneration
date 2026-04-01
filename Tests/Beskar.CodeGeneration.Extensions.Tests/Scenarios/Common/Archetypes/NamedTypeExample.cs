using System;

namespace Beskar.CodeGeneration.Extensions.Tests.Scenarios.Common.Archetypes;

[Example]
public sealed class NamedTypeExample
{
   [Example]
   private const string Name = "Example";

   public required int Number { get; set; }
   
   public void Test(int b)
   {
      
   }
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field)]
public sealed class ExampleAttribute : Attribute
{
   
}