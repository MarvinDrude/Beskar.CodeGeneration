using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Beskar.CodeGeneration.TypeIdGenerator.Marker.Interfaces;

namespace Beskar.CodeGeneration.TypeIdGenerator.Marker.Internal;

[StructLayout(LayoutKind.Sequential)]
[DebuggerDisplay("{DebuggerView,nq}")]
internal readonly record struct ExampleNumberId(long Value)
   : IComparable<ExampleNumberId>, ISpanParsable<ExampleNumberId>, ITypeSafeIdentifier<long>
{
   // Static constant like fields
   public static ExampleNumberId Empty { get; } = new (0); 
   public static ExampleNumberId MaxValue { get; } = new (long.MaxValue); 

   // Check properties
   public bool IsEmpty => Value == 0;
   public bool IsMaxValue => Value == long.MaxValue;
   public bool HasValue => !IsEmpty;
   
   // Stringify and debugger view
   public override string ToString() => $"{nameof(ExampleNumberId)}: {Value}";
   internal string DebuggerView => ToString();
   
   public bool Equals(ExampleNumberId other)
   {
      return Value == other.Value;
   }
   
   public override int GetHashCode()
   {
      return Value.GetHashCode();
   }
   
   // Comparable interface
   public int CompareTo(ExampleNumberId other) => Value.CompareTo(other.Value);
   
   // Compare operators
   public static bool operator <(ExampleNumberId left, ExampleNumberId right) => left.Value < right.Value; 
   public static bool operator <=(ExampleNumberId left, ExampleNumberId right) => left.Value <= right.Value; 
   public static bool operator >(ExampleNumberId left, ExampleNumberId right) => left.Value > right.Value; 
   public static bool operator >=(ExampleNumberId left, ExampleNumberId right) => left.Value >= right.Value; 
   
   // Implicit non-nullable conversions
   public static implicit operator long(ExampleNumberId id) => id.Value;
   public static implicit operator ExampleNumberId(long value) => new(value);
   
   // Implicit nullable conversions
   public static implicit operator long?(ExampleNumberId? id) => id?.Value;
   public static implicit operator ExampleNumberId?(long? value) => value.HasValue ? new ExampleNumberId(value.Value) : null;
   
   // Implicit non-nullable to nullable conversions
   public static implicit operator long?(ExampleNumberId id) => id.Value;
   public static implicit operator ExampleNumberId?(long value) => new ExampleNumberId(value);
   
   // Explicit nullable to non-nullable conversions
   public static explicit operator long(ExampleNumberId? id) => id ?? Empty;
   public static explicit operator ExampleNumberId(long? value) => value.HasValue ? new ExampleNumberId(value.Value) : Empty;
   
   // Span parsable interface
   public static ExampleNumberId Parse(string s, IFormatProvider? provider)
      => Parse(s.AsSpan(), provider);

   public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, out ExampleNumberId result)
      => TryParse(s.AsSpan(), provider, out result);

   public static ExampleNumberId Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
   {
      return new ExampleNumberId(long.Parse(s, provider));
   }

   public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out ExampleNumberId result)
   {
      if (long.TryParse(s, provider, out var value))
      {
         result = new ExampleNumberId(value);
         return true;
      }
      
      result = default;
      return false;
   }
   
   // Type safe identifier interface
   public int CompareTo(ITypeSafeIdentifier<long>? other)
   {
      return other switch 
      { 
         null => 1, 
         ExampleNumberId id => Value.CompareTo(id.Value), 
         _ => throw new ArgumentException($"Object must be of type ExampleNumberId.") 
      }; 
   }
   
   public bool Equals(ITypeSafeIdentifier<long>? other)
   {
      return other is ExampleNumberId id && Value == id.Value;
   }
}