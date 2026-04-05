using System.Collections.Immutable;
using Beskar.CodeGeneration.Extensions.Common;
using Beskar.CodeGeneration.Extensions.Common.Specs;
using Beskar.CodeGeneration.Extensions.Models.Diagnostics;
using Beskar.CodeGeneration.PacketGenerator.Models;
using Beskar.CodeGeneration.PacketGenerator.Rendering;
using Me.Memory.Buffers;
using Me.Memory.Collections;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.PacketGenerator;

public sealed partial class PacketGenerator
{
   private static void RenderRegistry(
      SourceProductionContext context,
      string assemblyName,
      MaybeSpec<PacketRegistrySpec> registrySpec,
      SequenceArray<PacketSpec> packetSpecs,
      SequenceArray<int> packetIndices)
   {
      context.DispatchDiagnostics(Diagnostics, registrySpec);
      if (!registrySpec.HasValue)
      {
         return;
      }
      
      var ct = context.CancellationToken;
      ct.ThrowIfCancellationRequested();

      var renderer = new PacketRegistryRenderer(context)
      {
         PacketSpecs = packetSpecs,
         PacketIndices = packetIndices,
         RegistrySpec = registrySpec.Value,
      };

      renderer.Render(registrySpec.Value.NamedTypeArchetype.Symbol.GeneratedFilePath);
   }

   private static void RenderPackets(
      SourceProductionContext context,
      string assemblyName,
      ImmutableArray<MaybeSpec<PacketSpec>> packetSpecs)
   {
      using var builder = new ArrayBuilder<PacketSpec>(packetSpecs.Length);
      
      foreach (var packetSpec in packetSpecs)
      {
         context.DispatchDiagnostics(Diagnostics, packetSpec);
         if (!packetSpec.HasValue)
         {
            continue;
         }
         
         var ct = context.CancellationToken;
         ct.ThrowIfCancellationRequested();
         
         builder.Add(packetSpec.Value);
      }
      
      var packetRenderer = new PacketRenderer(context)
      {
         PacketSpecs = [..builder.WrittenSpan],
      };
         
      packetRenderer.Render($"{assemblyName}.PacketMetadata.g.cs");
   }
}