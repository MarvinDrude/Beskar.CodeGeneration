using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;
using Me.Memory.Buffers.Dynamic;
using Me.Memory.Collections;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Models.Symbols;

public sealed record TypeSymbolSpec
{
   public required TypeKind Kind { get; init; }
   public required SpecialType SpecialType { get; init; }
   public required NullableAnnotation NullableAnnotation { get; init; }
   
   public PackedBools8 Flags { get; init; }
   
   private TypeSymbolLoadFlags _loadedFlags;
   private ref TypeSymbolLoadFlags LoadedFlags => ref _loadedFlags;

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
   
   private readonly SequenceArray<NamedTypeSymbolArchetype>? _allInterfaces;
   public SequenceArray<NamedTypeSymbolArchetype> AllInterfaces
   {
      get => LoadedFlags.AllInterfaces 
         ? _allInterfaces ?? throw new InvalidOperationException("AllInterfaces should be loaded but is null.") 
         : throw new InvalidOperationException("AllInterfaces is not loaded.");
      init
      {
         _allInterfaces = value;
         LoadedFlags.AllInterfaces = true;
      }
   }
   
   private readonly SequenceArray<NamedTypeSymbolArchetype>? _interfaces;
   public SequenceArray<NamedTypeSymbolArchetype> Interfaces
   {
      get => LoadedFlags.Interfaces 
         ? _interfaces ?? throw new InvalidOperationException("Interfaces should be loaded but is null.") 
         : throw new InvalidOperationException("Interfaces is not loaded.");
      init
      {
         _interfaces = value;
         LoadedFlags.Interfaces = true;
      }
   }
   
   private readonly NamedTypeSymbolArchetype? _baseType;
   public NamedTypeSymbolArchetype BaseType
   {
      get => LoadedFlags.BaseType 
         ? _baseType ?? throw new InvalidOperationException("BaseType should be loaded but is null.") 
         : throw new InvalidOperationException("BaseType is not loaded.");
      init
      {
         _baseType = value;
         LoadedFlags.BaseType = true;
      }
   }
}

public record struct TypeSymbolLoadFlags
{
   private PackedBools8 Flags;

   public bool Interfaces
   {
      get => Flags.Get(0);
      set => Flags.Set(0, value);
   }
   
   public bool BaseType
   {
      get => Flags.Get(1);
      set => Flags.Set(1, value);
   }
   
   public bool AllInterfaces
   {
      get => Flags.Get(0);
      set => Flags.Set(0, value);
   }
}