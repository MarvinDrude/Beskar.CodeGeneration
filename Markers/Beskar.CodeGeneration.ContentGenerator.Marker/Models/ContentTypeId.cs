using Beskar.CodeGeneration.TypeIdGenerator.Marker.Attributes;

namespace Beskar.CodeGeneration.ContentGenerator.Marker.Models;

[TypeSafeId]
public readonly partial record struct ContentTypeId(Guid Value);