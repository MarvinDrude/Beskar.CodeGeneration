using Me.Memory.Buffers.Dynamic;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Models.Symbols;

public sealed record PropertySymbolSpec
{
   public required RefKind RefKind { get; init; }
   public PackedBools8 Flags { get; init; }
   
   public bool HasGetter
   {
      get => Flags.Get(0);
      set => Flags.Set(0, value);
   }
   
   public bool HasSetter
   {
      get => Flags.Get(1);
      set => Flags.Set(1, value);
   }
   
   public bool IsIndexer
   {
      get => Flags.Get(2);
      set => Flags.Set(2, value);
   }

   public bool IsReadOnly
   {
      get => Flags.Get(3);
      set => Flags.Set(3, value);
   }

   public bool IsRequired
   {
      get => Flags.Get(4);
      set => Flags.Set(4, value);
   }
}