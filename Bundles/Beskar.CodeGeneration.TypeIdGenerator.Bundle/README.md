# Beskar.CodeGeneration.TypeIdGenerator

A high-performance C# Source Generator designed to create implementation-heavy, 
type-safe identifiers (Strongly Typed IDs) for .NET 10 and higher. By decorating a simple 
`readonly partial record struct` with the `[TypeSafeId]` attribute, the generator produces 
a robust implementation including conversions, parsing logic, and interface implementations.

## Features

* **📦 Compact Storage**: Generated IDs are based on unmanaged types (e.g., `int`, `long`, `Guid`) and 
stored as `readonly record structs` for zero-allocation overhead.
* **🔄 Comprehensive Conversions**: Automatically generates implicit and explicit conversion 
operators between the ID and its underlying value, including nullable support.
* **🔡 Text & Span Support**: Implements `ISpanParsable<T>` and `ISpanFormattable`,
allowing efficient parsing and formatting without string allocations.
* **🔍 Domain Integrity**: Implements `ITypeSafeIdentifier<T>`, providing 
a unified interface for ID comparison and value access across your domain.
* **🛠️ Customizable Generation**: Fine-tune the generated output 
(e.g., toggle JSON converters, string overrides, or conversion types) via attribute properties.

## Getting Started

```csharp
using Beskar.CodeGeneration.TypeIdGenerator.Marker.Attributes;

namespace MyProject.Domain;

[TypeSafeId]
public readonly partial record struct ExampleId(long Value);
```

Will generate something along the lines of:

```csharp
[StructLayout(LayoutKind.Sequential)]
[DebuggerDisplay("{DebuggerView,nq}")]
internal readonly partial record struct ExampleId(long Value)
   : IComparable<ExampleNumberId>, ISpanParsable<ExampleNumberId>, ITypeSafeIdentifier<long>, ISpanFormattable
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
   
   // Span formattable
   public string ToString(string? format, IFormatProvider? formatProvider)
      => Value.ToString(format, formatProvider);

   public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
      => Value.TryFormat(destination, out charsWritten, format, provider);
}
```