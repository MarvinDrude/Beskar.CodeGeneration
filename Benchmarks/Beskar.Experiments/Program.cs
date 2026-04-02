using Beskar.CodeGeneration.LanguageGenerator.Marker.Attributes;
using Beskar.CodeGeneration.LanguageGenerator.Marker.Detectors;
using Beskar.CodeGeneration.LanguageGenerator.Marker.Interfaces;
using Beskar.CodeGeneration.LanguageGenerator.Marker.Providers;
using Beskar.Languages;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello World!");

var serviceCollection = new ServiceCollection()
   .AddSingleton<ILanguageDetector, SystemCultureDetector>() // can add multiple detectors of varying priority
   .AddSingleton<ITranslationProvider, JsonTranslationProvider>() // example implementation of json files as base (can be DB too)
   .AddSingleton<TranslationFacade>();
   
var serviceProvider = serviceCollection.BuildServiceProvider();
   
// Json Provider example - can create own providers like DB or Third party api
var provider = (JsonTranslationProvider)serviceProvider.GetRequiredService<ITranslationProvider>();
provider.Initialize("Translations");
await provider.PopulateCache(CancellationToken.None); // can be called again if file changed at runtime

var translation = serviceProvider.GetRequiredService<TranslationFacade>();
Console.WriteLine(translation.TestGroup.Test);
Console.WriteLine(translation.RegisterGroup.Description);

return;

[TranslationGroup]
public enum TestGroup
{
   [TranslationKey]
   Test = 1,
   [TranslationKey(defaultValue: "Test2-Default")]
   Test2 = 2,
}

[TranslationGroup]
public enum RegisterGroup
{
   [TranslationKey]
   Title = 1,
   [TranslationKey]
   Description = 2,
}