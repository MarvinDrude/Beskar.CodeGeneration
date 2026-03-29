using Me.Memory.Buffers.Dynamic;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Models.Symbols;

public sealed record TypeSymbolSpec
{
   public required TypeKind Kind { get; init; }
   public required SpecialType SpecialType { get; init; }
   
   public PackedBools8 Flags { get; init; }

   public bool HasBaseType
   {
      get => Flags.Get(0);
      set => Flags.Set(0, value);
   }

   public bool IsReadOnly
   {
      get => Flags.Get(1);
      set => Flags.Set(1, value);
   }
   
   public bool IsRecord
   {
      get => Flags.Get(2);
      set => Flags.Set(2, value);
   }

   public bool IsReferenceType
   {
      get => Flags.Get(3);
      set => Flags.Set(3, value);
   }
   
   public bool IsRefLikeType
   {
      get => Flags.Get(4);
      set => Flags.Set(4, value);
   }

   public bool IsTupleType
   {
      get => Flags.Get(5);
      set => Flags.Set(5, value);
   }
   
   public bool IsUnmanagedType
   {
      get => Flags.Get(6);
      set => Flags.Set(6, value);
   }
   
   public bool IsValueType
   {
      get => Flags.Get(7);
      set => Flags.Set(7, value);
   }
}