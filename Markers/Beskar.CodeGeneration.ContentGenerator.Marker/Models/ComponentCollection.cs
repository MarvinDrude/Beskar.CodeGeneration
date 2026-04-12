using Beskar.CodeGeneration.ContentGenerator.Marker.Interfaces;

namespace Beskar.CodeGeneration.ContentGenerator.Marker.Models;

public sealed class ComponentCollection<TComponent> : List<TComponent>, IContentField
{
   
}