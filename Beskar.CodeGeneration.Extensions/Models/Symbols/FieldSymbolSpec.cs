using Beskar.CodeGeneration.Extensions.Interfaces.Specs;
using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;
using Me.Memory.Buffers.Dynamic;
using Me.Memory.Collections;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Models.Symbols;

public sealed record FieldSymbolSpec
{
   public required RefKind RefKind { get; init; }
   
   public PackedBools8 Flags { get; init; }
   
   private FieldSymbolLoadFlags _loadedFlags;
   private ref FieldSymbolLoadFlags LoadedFlags => ref _loadedFlags;
   
   public bool HasConstantValue
   {
      get => Flags.Get(0);
      set => Flags.Set(0, value);
   }
   
   public bool IsConst
   {
      get => Flags.Get(1);
      set => Flags.Set(1, value);
   }
   
   public bool IsRequired
   {
      get => Flags.Get(2);
      set => Flags.Set(2, value);
   }
   
   public bool IsVolatile
   {
      get => Flags.Get(3);
      set => Flags.Set(3, value);
   }
   
   public bool IsReadOnly
   {
      get => Flags.Get(4);
      set => Flags.Set(4, value);
   }
   
   private TypeSymbolArchetype? _type;
   public TypeSymbolArchetype Type
   {
      get => LoadedFlags.Type 
         ? _type ?? throw new InvalidOperationException("Type should be loaded but is null.") 
         : throw new InvalidOperationException("Type is not loaded.");
      set
      {
         _type = value;
         LoadedFlags.Type = true;
      }
   }
   
   private SequenceArray<IAttributeSpec>? _attributes;
   public SequenceArray<IAttributeSpec> Attributes
   {
      get => LoadedFlags.Attributes 
         ? _attributes ?? throw new InvalidOperationException("Attributes should be loaded but is null.") 
         : throw new InvalidOperationException("Attributes is not loaded.");
      set
      {
         _attributes = value;
         LoadedFlags.Attributes = true;
      }
   }
}

public record struct FieldSymbolLoadFlags
{
   private PackedBools8 Flags;
   
   public bool Type
   {
      get => Flags.Get(0);
      set => Flags.Set(0, value);
   }

   public bool Attributes
   {
      get => Flags.Get(1);
      set => Flags.Set(1, value);
   }
}