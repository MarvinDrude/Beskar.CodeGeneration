using System;
using Beskar.CodeGeneration.TypeIdGenerator.Marker.Attributes;

namespace Beskar.CodeGeneration.TypeIdGenerator.Tests.Scenarios.Simple;

[TypeSafeId]
public readonly partial record struct ExampleIntId(int Value);

[TypeSafeId]
public readonly partial record struct ExampleULongId(ulong Value);

[TypeSafeId]
public readonly partial record struct ExampleGuidId(Guid Value);