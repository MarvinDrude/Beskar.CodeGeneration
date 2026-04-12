using Beskar.CodeGeneration.ContentGenerator.Marker.Interfaces;

namespace Beskar.CodeGeneration.ContentGenerator.Marker.Models;

public sealed class ComponentReference<TComponent> : IContentField
    where TComponent : ComponentBase
{
   public required TComponent Component { get; set; }
}