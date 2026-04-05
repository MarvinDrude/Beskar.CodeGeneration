using Beskar.CodeGeneration.PacketGenerator.Models;
using Me.Memory.Collections;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.PacketGenerator;

[Generator]
public sealed partial class PacketGenerator : IIncrementalGenerator
{
   public const string GeneratorName = "PacketGenerator";
   public const string GeneratorVersion = "1.1.6";
   
   public void Initialize(IncrementalGeneratorInitializationContext context)
   {
      var assemblyNameProvider = context.CompilationProvider
         .Select(static (c, _) => c.AssemblyName?
            .Replace(" ", string.Empty)
            .Replace(".", string.Empty)
            .Trim() ?? "UnknownAssembly");
      
      var maybePacketSpecProvider = context.SyntaxProvider
         .ForAttributeWithMetadataName(
            PacketAttributeFullName,
            predicate: static (_, _) => true,
            transform: Transform);
      
      var maybePacketRegistrySpecProvider = context.SyntaxProvider
         .ForAttributeWithMetadataName(
            PacketRegistryAttributeFullName,
            predicate: static (_, _) => true,
            transform: TransformRegistry);
      
      var packetCombined = maybePacketSpecProvider
         .Collect().Combine(assemblyNameProvider);

      var registryProvider = maybePacketRegistrySpecProvider
         .Combine(packetCombined)
         .Select(static (combined, _) =>
         {
            var registry = combined.Left;
            var packets = combined.Right.Left
               .Where(x => x.HasValue)
               .Select(x => x.Value)
               .ToArray();

            var registryValue = registry.Value;
            var relevantPackets = packets
               .Where(x => x.RegistryFullTypeNames.Array.Contains(
                  registryValue.NamedTypeArchetype.Symbol.FullName))
               .ToArray();

            var specs = new SequenceArray<PacketSpec>(relevantPackets);
            return (registry, specs);
         });
      var registryCombined = registryProvider.Combine(assemblyNameProvider);
      
      context.RegisterSourceOutput(packetCombined, static (ctx, source) 
         => RenderPackets(ctx, source.Right, source.Left));
      
      context.RegisterSourceOutput(registryCombined, static (ctx, source) 
         => RenderRegistry(ctx, source.Right, source.Left.registry, source.Left.specs));
      
      context.RegisterPostInitializationOutput(static ctx =>
      {
         ctx.AddSource($"{GeneratorName}.g.cs", $"// Version {GeneratorVersion}");
      });
   }
}