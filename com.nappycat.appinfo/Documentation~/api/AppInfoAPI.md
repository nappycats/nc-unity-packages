# NappyCat • App Info API

Centralise app metadata and emit Nc.Keys.App constants.

## Surface
### Runtime
- NcAppInfo asset (Resources/NappyCat/NcAppInfo)
- Nc.App.Info accessor

### Editor
- NcAppConstsGenerator (Nc.Keys.App.*)
- Project Settings → Nappy Cat → App Info

## Example
```csharp
var info = NappyCat.Nc.App.Info;
Debug.Log($"{info.Title} ({info.GameId})");
```
