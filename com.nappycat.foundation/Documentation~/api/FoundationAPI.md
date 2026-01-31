# NappyCat • Foundation API

Bedrock utilities: Nc.Keys hub, settings base, patterns, and editor helpers.

## Surface
### Core Types
- NcDefines
- NcSettingsBase / NcSettings<T>
- NcSettingsLocator
- NcScriptable

### Patterns
- NcSingleton
- NcPersistentSingleton
- NcMonoRunner (runner scaffold)

### Editor
- NcSettingsCreator menu
- NcKeysGenerateAll

## Example
```csharp
var cfg = MySettings.Instance;
var key = NappyCat.Nc.Keys.Tags.ENEMY;
```
