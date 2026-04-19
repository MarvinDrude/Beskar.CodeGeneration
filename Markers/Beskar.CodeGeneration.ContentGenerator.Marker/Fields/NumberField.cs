using System.Numerics;
using Beskar.CodeGeneration.ContentGenerator.Marker.Interfaces;

namespace Beskar.CodeGeneration.ContentGenerator.Marker.Fields;

public sealed class NumberField<TUnderlying> : IContentField
   where TUnderlying : struct, INumber<TUnderlying>
{
   public TUnderlying Value { get; set; }
}