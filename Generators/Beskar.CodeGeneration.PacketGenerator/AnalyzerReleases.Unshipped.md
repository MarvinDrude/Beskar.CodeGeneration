; Unshipped analyzer release
; https://github.com/dotnet/roslyn/blob/main/src/RoslynAnalyzers/Microsoft.CodeAnalysis.Analyzers/ReleaseTrackingAnalyzers.Help.md

### New Rules

| Rule ID | Category        | Severity | Notes                                 |
|---------|-----------------|-------|---------------------------------------|
| PG001   | PacketGenerator | Error | Packet needs to be class or struct with interface IPacket |
| PG002   | PacketGenerator | Error | Invalid type: PacketRegistry is required to be a class and must derive from BasePacketRegistry |