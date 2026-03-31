using System.Diagnostics;
using Beskar.CodeGeneration.Extensions.Interfaces.Specs;
using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;
using Me.Memory.Buffers.Dynamic;
using Me.Memory.Collections;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Models.Symbols;

[DebuggerDisplay("NamedType, Arity: {Arity, nq}")]
public sealed record NamedTypeSymbolSpec
{
   public required int Arity { get; init; }
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
   
   private SequenceArray<MethodSymbolArchetype>? _methods;
   public SequenceArray<MethodSymbolArchetype> Methods
   {
      get => LoadedFlags.Methods 
         ? _methods ?? throw new InvalidOperationException("Methods should be loaded but is null.")
         : throw new InvalidOperationException("Methods are not loaded.");
      set
      {
         _methods = value;
         LoadedFlags.Methods = true;
      }
   }
   
   private SequenceArray<TypeParameterArchetype>? _typeParameters;
   public SequenceArray<TypeParameterArchetype> TypeParameters
   {
      get => LoadedFlags.TypeParameters 
         ? _typeParameters ?? throw new InvalidOperationException("Type parameters should be loaded but is null.")
         : throw new InvalidOperationException("Type parameters are not loaded.");
      set
      {
         _typeParameters = value;
         LoadedFlags.TypeParameters = true;
      }
   }
   
   private SequenceArray<TypeSymbolArchetype>? _typeArguments;
   public SequenceArray<TypeSymbolArchetype> TypeArguments
   {
      get => LoadedFlags.TypeArguments 
         ? _typeArguments ?? throw new InvalidOperationException("Type arguments should be loaded but is null.")
         : throw new InvalidOperationException("Type arguments are not loaded.");
      set
      {
         _typeArguments = value;
         LoadedFlags.TypeArguments = true;
      }
   }
   
   private SequenceArray<NullableAnnotation>? _typeArgumentNullableAnnotations;
   public SequenceArray<NullableAnnotation> TypeArgumentNullableAnnotations
   {
      get => LoadedFlags.TypeArgumentNullableAnnotations 
         ? _typeArgumentNullableAnnotations ?? throw new InvalidOperationException("Type argument nullable annotations should be loaded but is null.")
         : throw new InvalidOperationException("Type argument nullable annotations are not loaded.");
      set
      {
         _typeArgumentNullableAnnotations = value;
         LoadedFlags.TypeArgumentNullableAnnotations = true;
      }
   }
   
   private SequenceArray<IAttributeSpec>? _attributes;
   public SequenceArray<IAttributeSpec> Attributes
   {
      get => LoadedFlags.Attributes 
         ? _attributes ?? throw new InvalidOperationException("Attributes should be loaded but is null.")
         : throw new InvalidOperationException("Attributes are not loaded.");
      set
      {
         _attributes = value;
         LoadedFlags.Attributes = true;
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
   
   public bool Attributes
   {
      get => Flags.Get(4);
      set => Flags.Set(4, value);
   }
   
   public bool Properties
   {
      get => Flags.Get(5);
      set => Flags.Set(5, value);
   }
   
   public bool Fields
   {
      get => Flags.Get(6);
      set => Flags.Set(6, value);
   }
}