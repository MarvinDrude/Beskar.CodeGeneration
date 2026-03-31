using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;
using Me.Memory.Buffers.Dynamic;
using Me.Memory.Collections;

namespace Beskar.CodeGeneration.Extensions.Models.Symbols;

public sealed record TypeParameterSymbolSpec
{
   public required int Ordinal { get; init; }
   public PackedBools8 Flags { get; init; }

   private TypeParameterSymbolLoadFlags _loadedFlags;
   private ref TypeParameterSymbolLoadFlags LoadedFlags => ref _loadedFlags;
   
   public bool AllowsRefLikeType
   {
      get => Flags.Get(0);
      set => Flags.Set(0, value);
   }
   
   public bool HasConstructorConstraint
   {
      get => Flags.Get(1);
      set => Flags.Set(1, value);
   }
   
   public bool HasNotNullConstraint
   {
      get => Flags.Get(2);
      set => Flags.Set(2, value);
   }
   
   public bool HasReferenceTypeConstraint
   {
      get => Flags.Get(3);
      set => Flags.Set(3, value);
   }
   
   public bool HasUnmanagedTypeConstraint
   {
      get => Flags.Get(4);
      set => Flags.Set(4, value);
   }
   
   public bool HasValueTypeConstraint
   {
      get => Flags.Get(5);
      set => Flags.Set(5, value);
   }
   
   private SequenceArray<TypeSymbolArchetype>? _constraintTypes;
   public SequenceArray<TypeSymbolArchetype> ConstraintTypes
   {
      get => LoadedFlags.ConstraintTypes 
         ? _constraintTypes ?? throw new InvalidOperationException("Constraint types should be loaded but is null.")
         : throw new InvalidOperationException("Constraint types are not loaded.");
      set
      {
         _constraintTypes = value;
         LoadedFlags.ConstraintTypes = true;
      }
   }
}

public record struct TypeParameterSymbolLoadFlags
{
   private PackedBools8 Flags;

   public bool ConstraintTypes
   {
      get => Flags.Get(0);
      set => Flags.Set(0, value);
   }
}