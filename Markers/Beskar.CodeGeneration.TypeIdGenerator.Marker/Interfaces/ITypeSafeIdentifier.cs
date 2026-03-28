namespace Beskar.CodeGeneration.TypeIdGenerator.Marker.Interfaces;

public interface ITypeSafeIdentifier<TUnderlying>
   : IComparable<ITypeSafeIdentifier<TUnderlying>>, IEquatable<ITypeSafeIdentifier<TUnderlying>>
   where TUnderlying : unmanaged
{
   public TUnderlying Value { get; }
}