using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;
using Me.Memory.Buffers.Dynamic;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Models.Symbols;

public sealed record ParameterSymbolSpec
{
   public required int Ordinal { get; init; }
   public required ScopedKind ScopeKind { get; init; }
   public required RefKind RefKind { get; init; }
   
   public PackedBools8 Flags { get; init; }
   
   private ParameterSymbolLoadFlags _loadedFlags;
   private ref ParameterSymbolLoadFlags LoadedFlags => ref _loadedFlags;
   
   public bool HasExplicitDefaultValue
   {
      get => Flags.Get(0);
      set => Flags.Set(0, value);
   }
   
   public bool IsParamsArray
   {
      get => Flags.Get(1);
      set => Flags.Set(1, value);
   }
   
   public bool IsParamsCollection
   {
      get => Flags.Get(2);
      set => Flags.Set(2, value);
   }
   
   public bool IsDiscard
   {
      get => Flags.Get(3);
      set => Flags.Set(3, value);
   }
   
   public bool IsOptional
   {
      get => Flags.Get(4);
      set => Flags.Set(4, value);
   }

   private readonly TypeSymbolArchetype? _type;
   public TypeSymbolArchetype Type
   {
      get => LoadedFlags.Type 
         ? _type ?? throw new InvalidOperationException("Type should be loaded but is null.") 
         : throw new InvalidOperationException("Type is not loaded.");
      init
      {
         _type = value;
         LoadedFlags.Type = true;
      }
   }
}

public record struct ParameterSymbolLoadFlags
{
   private PackedBools8 Flags;

   public bool Type
   {
      get => Flags.Get(0);
      set => Flags.Set(0, value);
   }
}