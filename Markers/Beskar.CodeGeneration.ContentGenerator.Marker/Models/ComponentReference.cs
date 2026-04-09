namespace Beskar.CodeGeneration.ContentGenerator.Marker.Models;

public sealed class ComponentReference<TComponent>
    where TComponent : ComponentBase
{
   public required TComponent Component { get; set; }
}