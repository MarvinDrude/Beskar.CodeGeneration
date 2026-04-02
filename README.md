# Beskar.CodeGeneration

Beskar.CodeGeneration is a suite of tools designed to streamline the development of
**C# Source Generators** for **.NET 10** and beyond. Leveraging the latest C# language
features, it provides a robust infrastructure for symbol analysis, metadata extraction,
and type-safe code generation.

> **Note:** I use these mainly just for me and my private projects, but feel free to use them as well.
> Since I already build my generators with .NET 10, it will only work if your IDE supports it.

## Introduction

Developing Source Generators often involves repetitive tasks like manual attribute parsing,
navigating complex symbol trees, and managing diagnostic reporting. This project simplifies
that process by:

* **Archetype-Based Modeling**: Decouples heavy Roslyn `ISymbol` objects into lightweight, comparable "Archetypes"
  that are safe for use within the incremental generator pipeline.
* **Fluent Metadata Access**: Provides high-level extension methods to easily extract
  values from attributes, handle positional/named arguments, and resolve type information.
* **Modern .NET Standards**: Built from the ground up for .NET 10, utilizing features
  like `INumber<T>`, advanced spans, and the latest C# preview features.

## Project Modules

| Package                                     | NuGet                                                                                                                                                    | Documentation | Description                                                                                                       |
|:--------------------------------------------|:---------------------------------------------------------------------------------------------------------------------------------------------------------| :--- |:------------------------------------------------------------------------------------------------------------------|
| **Beskar.CodeGeneration.Extensions**        | [![NuGet](https://img.shields.io/nuget/v/Beskar.CodeGeneration.Extensions)](https://www.nuget.org/packages/Beskar.CodeGeneration.Extensions)             | [README](./Beskar.CodeGeneration.Extensions/README.md) | Core utilities, symbol transformers, and attribute helpers.                                                       |
| **Beskar.CodeGeneration.TypeIdGenerator**   | [![NuGet](https://img.shields.io/nuget/v/Beskar.CodeGeneration.TypeIdGenerator)](https://www.nuget.org/packages/Beskar.CodeGeneration.TypeIdGenerator)   | [README](./Bundles/Beskar.CodeGeneration.TypeIdGenerator.Bundle/README.md) | A ready-to-use generator for creating type-safe identifiers (e.g., `UserId`).                                     |
| **Beskar.CodeGeneration.LanguageGenerator** | [![NuGet](https://img.shields.io/nuget/v/Beskar.CodeGeneration.LanguageGenerator)](https://www.nuget.org/packages/Beskar.CodeGeneration.LanguageGenerator) | [README](./Bundles/Beskar.CodeGeneration.LanguageGenerator.Bundle/README.md) | A ready-to-use generator that transforms simple enumerations into a robust, type-safe translation infrastructure. |