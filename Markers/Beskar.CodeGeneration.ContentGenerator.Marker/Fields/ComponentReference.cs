using Beskar.CodeGeneration.ContentGenerator.Marker.Interfaces;
using Beskar.CodeGeneration.ContentGenerator.Marker.Models;

namespace Beskar.CodeGeneration.ContentGenerator.Marker.Fields;

public sealed class ComponentReference<TComponent> : IContentField
    where TComponent : ComponentBase
{
   public required TComponent Component { get; set; }
}