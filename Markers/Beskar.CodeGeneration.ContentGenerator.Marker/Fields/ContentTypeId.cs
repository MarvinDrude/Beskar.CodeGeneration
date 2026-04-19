using Beskar.CodeGeneration.TypeIdGenerator.Marker.Attributes;

namespace Beskar.CodeGeneration.ContentGenerator.Marker.Fields;

[TypeSafeId]
public readonly partial record struct ContentTypeId(Guid Value);