# Beskar.CodeGeneration.Extensions

A comprehensive suite of utilities and models designed to simplify the development of 
C# Source Generators. This package provides high-level abstractions ("Archetypes") and 
fluent extension methods to handle Roslyn symbols and attribute data efficiently.

## Core Features

### 🏛️ Symbol Archetypes
Roslyn's `ISymbol` objects are not suitable for rendering storage or caching in incremental 
generators because they hold references to the entire compilation. These extensions provide 
"Archetypes" (like `NamedTypeSymbolArchetype` or `MethodSymbolArchetype`) which are lightweight,
serializable records containing only the metadata needed for code generation.

* **Field Archetypes**: Capture `RefKind`, `IsReadOnly`, `IsVolatile`, and constant values.
* **Method Archetypes**: Track return types, parameters, async/iterator status, and accessibility.
* **Type Archetypes**: Handle inheritance, interface implementations, and generic constraints.
* **Parameter Archetypes**: Stores ordinal position, scope, and optionality.
* **Property Archetypes**: Includes metadata for getters, setters, and indexer status.

And more...

### 🛠️ Extension Methods

#### Attribute Data Handling
The package provides powerful "Fallback" extensions to resolve attribute values. 
These methods automatically check for named arguments first and fall back to positional 
constructor arguments if the named one is missing.

```csharp
// Example: Determine a value regardless of how the user provided it
string? value = attribute.DetermineStringValue(name: "ParameterName", index: 0, defaultValue: "Default");
bool isEnabled = attribute.DetermineBoolValue("Enabled", 1, true);
```

Supported types for determination include:
* **Primitives**: `bool`, `int`, `long`, `float`, `double`, `decimal`, `byte`, `short`, `char`, `uint`, `ulong`.
* **Complex**: `ITypeSymbol`, `IFieldSymbol` (Enums).
* **Arrays**: Specialized support for arrays of all the above types.

### ⚙️ Configurable Transformation
Use `ArchetypeTransformOptions` to control the depth and detail of the symbol transformation to optimize performance and memory usage.

* **Depth Control**: Limit how deep the transformer navigates the type tree (e.g., stopping at the first level of base types).
* **Load Flags**: Fine-grained control over whether to load attributes, interfaces, or specific member types.
* **Filtering**: Provide custom predicates (e.g., `MethodFilter`) to only include relevant members in the generated Archetype.
* **Attribute Registration**: Register specific attributes that should be parsed into `IAttributeSpec` objects during transformation.