using Me.Memory.Buffers.Dynamic;

namespace Beskar.CodeGeneration.Extensions.Models.Symbols;

public sealed record NamedTypeSymbolSpec
{
   public PackedBools8 Flags { get; init; }
   
   public bool IsFileLocal
   {
      get => Flags.Get(0);
      set => Flags.Set(0, value);
   }

   public bool IsEnum
   {
      get => Flags.Get(1);
      set => Flags.Set(1, value);
   }
}