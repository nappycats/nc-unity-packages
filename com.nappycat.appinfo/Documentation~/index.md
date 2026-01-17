# App Info (`com.nappycat.appinfo`)

Centralised app metadata and code generation for store IDs, links, and versioning.

## Setup
1) Install the package (Add package from disk).  
2) Create or edit `Resources/NappyCat/NcAppInfo.asset` (Project Settings → Nappy Cat → App Info).  
3) Run `Nappy Cat/App/Generate App Consts (NcKeys.App.g.cs)` to emit strongly typed constants.

## Runtime access
```csharp
using NappyCat;

var info = Nc.App.Info;
Debug.Log($"{info.Title} ({info.GameId})");
```

## Codegen targets
- With Foundation present (`NC_PKG_FOUNDATION`): `Packages/com.nappycat.foundation/Runtime/NcKeys/NcKeys.App.g.cs`.
- Without Foundation: `Assets/NappyCat/NcKeys.Generated/NcKeys.App.g.cs`.

## Links
- README: `../README.md`
- Changelog: `../CHANGELOG.md`
