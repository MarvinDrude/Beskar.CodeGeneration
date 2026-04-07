; Unshipped analyzer release
; https://github.com/dotnet/roslyn/blob/main/src/RoslynAnalyzers/Microsoft.CodeAnalysis.Analyzers/ReleaseTrackingAnalyzers.Help.md

### New Rules

| Rule ID | Category           | Severity | Notes                                 |
|---------|--------------------|-------|---------------------------------------|
| PCG001  | ProcessorGenerator    | Error | Processor needs to be a class and implement one of the correct interfaces |
| PCG002  | ProcessorGenerator | Error | Invalid type: ProcessorPipeline must be a class with processor steps |