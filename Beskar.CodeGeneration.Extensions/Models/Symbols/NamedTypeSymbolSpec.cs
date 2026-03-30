using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;
using Me.Memory.Buffers.Dynamic;
using Me.Memory.Collections;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Models.Symbols;

public sealed record NamedTypeSymbolSpec
{
   public PackedBools8 Flags { get; init; }
   
   private NamedTypeSymbolLoadFlags _loadedFlags;
   private ref NamedTypeSymbolLoadFlags LoadedFlags => ref _loadedFlags;
   
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
   
   private readonly SequenceArray<MethodSymbolArchetype>? _methods;
   public SequenceArray<MethodSymbolArchetype> Methods
   {
      get => LoadedFlags.Methods 
         ? _methods ?? throw new InvalidOperationException("Methods should be loaded but is null.")
         : throw new InvalidOperationException("Methods are not loaded.");
      init
      {
         _methods = value;
         LoadedFlags.Methods = true;
      }
   }
   
   private readonly SequenceArray<TypeParameterArchetype>? _typeParameters;
   public SequenceArray<TypeParameterArchetype> TypeParameters
   {
      get => LoadedFlags.TypeParameters 
         ? _typeParameters ?? throw new InvalidOperationException("Type parameters should be loaded but is null.")
         : throw new InvalidOperationException("Type parameters are not loaded.");
      init
      {
         _typeParameters = value;
         LoadedFlags.TypeParameters = true;
      }
   }
   
   private readonly SequenceArray<TypeSymbolArchetype>? _typeArguments;
   public SequenceArray<TypeSymbolArchetype> TypeArguments
   {
      get => LoadedFlags.TypeArguments 
         ? _typeArguments ?? throw new InvalidOperationException("Type arguments should be loaded but is null.")
         : throw new InvalidOperationException("Type arguments are not loaded.");
      init
      {
         _typeArguments = value;
         LoadedFlags.TypeArguments = true;
      }
   }
   
   private readonly SequenceArray<NullableAnnotation>? _typeArgumentNullableAnnotations;
   public SequenceArray<NullableAnnotation> TypeArgumentNullableAnnotations
   {
      get => LoadedFlags.TypeArgumentNullableAnnotations 
         ? _typeArgumentNullableAnnotations ?? throw new InvalidOperationException("Type argument nullable annotations should be loaded but is null.")
         : throw new InvalidOperationException("Type argument nullable annotations are not loaded.");
      init
      {
         _typeArgumentNullableAnnotations = value;
         LoadedFlags.TypeArgumentNullableAnnotations = true;
      }
   }
}

public record struct NamedTypeSymbolLoadFlags
{
   private PackedBools8 Flags;

   public bool Methods
   {
      get => Flags.Get(0);
      set => Flags.Set(0, value);
   }

   public bool TypeParameters
   {
      get => Flags.Get(1);
      set => Flags.Set(1, value);
   }

   public bool TypeArguments
   {
      get => Flags.Get(2);
      set => Flags.Set(2, value);
   }
 
   public bool TypeArgumentNullableAnnotations
   {
      get => Flags.Get(3);
      set => Flags.Set(3, value);
   }
}