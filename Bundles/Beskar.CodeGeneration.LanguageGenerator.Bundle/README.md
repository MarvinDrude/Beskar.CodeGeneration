# Beskar.CodeGeneration.LanguageGenerator

A high-performance C# Source Generator for **.NET 10** that transforms simple enumerations into a robust, type-safe translation infrastructure. It automatically generates translation keys and a fluent `TranslationFacade` to streamline localization in your applications.

## Key Features

* **🚀 Zero-Reflection**: All keys and mapping logic are generated at compile-time for maximum performance.
* **🏗️ Fluent Facade**: Provides a strongly-typed API (`translation.Group.Key`) instead of error-prone magic strings.
* **📦 Multiple Providers**: Use the built-in `JsonTranslationProvider` or implement your own for Databases or Third-party APIs.
* **🔍 Culture Detection**: Support for multiple prioritized language detectors (e.g., `SystemCultureDetector`).
* **🛠️ Default Values**: Define fallback translations directly in your code using attributes.

## Installation

The generator is part of the **Beskar.CodeGeneration** suite. Ensure your project targets **.NET 10** or higher to leverage the latest C# features.

## Quick Start

### 1. Define your Translation Groups
Decorate your enums with `[TranslationGroup]` and members with `[TranslationKey]`.

```csharp
[TranslationGroup]
public enum TestGroup
{
   [TranslationKey]
   Test = 1,
   
   [TranslationKey(defaultValue: "Default Greeting")]
   WelcomeMessage = 2,
}

[TranslationGroup(GroupName = "Identity")]
public enum RegisterGroup
{
   [TranslationKey(keyName: "Title")]
   Header = 1,
   
   [TranslationKey]
   Description = 2,
}
```

### 2. Configure Dependency Injection

Register the necessary services to enable the translation engine.

```csharp
var services = new ServiceCollection()
   .AddSingleton<ILanguageDetector, SystemCultureDetector>()
   .AddSingleton<ITranslationProvider, JsonTranslationProvider>()
   .AddSingleton<TranslationFacade>();
   
var provider = services.BuildServiceProvider();

// Initialize the Json Provider (Example)
var jsonProvider = (JsonTranslationProvider)provider.GetRequiredService<ITranslationProvider>();
jsonProvider.Initialize("Translations"); // Path to your .json files
await jsonProvider.PopulateCache(CancellationToken.None);

var translation = provider.GetRequiredService<TranslationFacade>();
```

### 3. Usage

Access your translations with full IntelliSense support.

```csharp
// Accessing generated properties via the Facade
string welcome = translation.TestGroup.WelcomeMessage;
string desc = translation.Identity.Description;

// Accessing raw generated constant keys
string rawKey = LangKey.TestGroup.WelcomeMessage;
```

## Generated Artifacts

In the background, the generator creates partial classes to handle the heavy lifting:

* **`LangKey`**: A static class containing `const string` definitions for all translation keys, an `AllKeys` collection, and a `GetDefaultValue` lookup based on your attributes.
* **`TranslationFacade`**: A sealed class that wraps the `ITranslationProvider` to provide the fluent access layer. It uses the new C# `field` keyword for efficient lazy loading of group facades.

## Advanced Configuration

### Custom Providers

You can create custom providers by inheriting from `TranslationBaseProvider`. This allows you to fetch translations from a database, a CMS, or an external API.

```csharp
public class SqlTranslationProvider : TranslationBaseProvider
{
   public override async ValueTask PopulateCache(CancellationToken ct) 
   {
      // Custom logic to load translations from SQL
      // AddToCache("en", data);
   }
}
```