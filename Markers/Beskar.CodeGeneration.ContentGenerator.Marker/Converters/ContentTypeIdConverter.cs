using Beskar.CodeGeneration.ContentGenerator.Marker.Fields;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Beskar.CodeGeneration.ContentGenerator.Marker.Converters;

public sealed class ContentTypeIdConverter() : ValueConverter<ContentTypeId, Guid>(
   v => v.Value,
   v => new ContentTypeId(v));