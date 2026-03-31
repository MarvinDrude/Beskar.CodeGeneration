using Beskar.CodeGeneration.Extensions.Interfaces.Specs;
using Me.Memory.Buffers.Dynamic;
using Me.Memory.Collections;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Models.Symbols;

public sealed record SymbolSpec
{
   public required string Name { get; init; }
   public required string MetadataName { get; init; }
   public required string FullName { get; init; }
   
   public string? NameSpace { get; init; }
   
   public required SymbolKind Kind { get; init; }
   public required Accessibility Accessibility { get; init; }
   
   public PackedBools8 Flags { get; init; }
   
   private SymbolLoadFlags _loadedFlags;
   private ref SymbolLoadFlags LoadedFlags => ref _loadedFlags;

   public bool IsStatic
   {
      get => Flags.Get(0);
      set => Flags.Set(0, value);
   }

   public bool IsAbstract
   {
      get => Flags.Get(1);
      set => Flags.Set(1, value);
   }

   public bool IsVirtual
   {
      get => Flags.Get(2);
      set => Flags.Set(2, value);
   }

   public bool IsSealed
   {
      get => Flags.Get(3);
      set => Flags.Set(3, value);
   }

   public bool IsOverride
   {
      get => Flags.Get(4);
      set => Flags.Set(4, value);
   }
   
   public bool IsImplicitlyDeclared
   {
      get => Flags.Get(5);
      set => Flags.Set(5, value);
   }
   
   public bool IsExtern
   {
      get => Flags.Get(6);
      set => Flags.Set(6, value);
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

public record struct SymbolLoadFlags
{
   private PackedBools8 Flags;
   
   public bool Attributes
   {
      get => Flags.Get(0);
      set => Flags.Set(0, value);
   }
}