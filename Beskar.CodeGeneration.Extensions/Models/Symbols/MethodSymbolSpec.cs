using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;
using Me.Memory.Buffers.Dynamic;
using Me.Memory.Collections;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Models.Symbols;

public sealed record MethodSymbolSpec
{
   public required MethodKind MethodKind { get; init; }
   public PackedBools8 Flags { get; init; }
   
   private MethodSymbolLoadFlags _loadedFlags;
   private ref MethodSymbolLoadFlags LoadedFlags => ref _loadedFlags;
   
   public bool HasVoidReturn
   {
      get => Flags.Get(0);
      set => Flags.Set(0, value);
   }
   
   public bool ReturnsByRef
   {
      get => Flags.Get(1);
      set => Flags.Set(1, value);
   }
   
   public bool ReturnsByRefReadonly
   {
      get => Flags.Get(2);
      set => Flags.Set(2, value);
   }
   
   public bool IsReadOnly
   {
      get => Flags.Get(3);
      set => Flags.Set(3, value);
   }

   public bool IsIterator
   {
      get => Flags.Get(4);
      set => Flags.Set(4, value);
   }
   
   public bool IsAsync
   {
      get => Flags.Get(5);
      set => Flags.Set(5, value);
   }

   public TypeSymbolArchetype? ReturnType
   {
      get => LoadedFlags.ReturnType 
         ? field 
         : throw new InvalidOperationException("Return type is not loaded.");
      set
      {
         field = value;
         LoadedFlags.ReturnType = true;
      }
   }

   private SequenceArray<ParameterSymbolArchetype>? _parameters;
   public SequenceArray<ParameterSymbolArchetype> Parameters
   {
      get => LoadedFlags.Parameters 
         ? _parameters ?? throw new InvalidOperationException("Parameters should be loaded but is null.") 
         : throw new InvalidOperationException("Parameters are not loaded.");
      set
      {
         _parameters = value;
         LoadedFlags.Parameters = true;
      }
   }
}

public record struct MethodSymbolLoadFlags
{
   private PackedBools8 Flags;
   
   public bool ReturnType
   {
      get => Flags.Get(0);
      set => Flags.Set(0, value);
   }

   public bool Parameters
   {
      get => Flags.Get(1);
      set => Flags.Set(1, value);
   }
}