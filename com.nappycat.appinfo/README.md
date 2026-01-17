# Nappy Cat App Core

Centralised app metadata and code generation for shipping games built on the Nappy Cat stack. Keep store URLs, legal links, app identifiers, and tooling paths in one ScriptableObject, then surface strongly-typed constants across your project.

## Requirements
- Unity 2021.3 LTS or newer

### Optional NappyCat Foundation
- Package runs standalone (NcAppInfo derives from ScriptableObject).
- When `com.nappycat.foundation` is present (asmdef adds `NC_PKG_FOUNDATION`), the const generator writes `Nc.Keys.App.*` to the Foundation package; otherwise it writes to `Assets/NappyCat/NcKeys.Generated/`.

## Installation
1. Add the package folder under `Packages/` and register it via Add package from disk....
2. Create an `NcAppInfo` asset and place it under `Resources/NappyCat` so runtime access works out of the box.
3. Use the built-in generator to emit `NcKeys.App.g.cs` (`Nc.Keys.App.*`) whenever App Info changes.

## Highlights
- `NcAppInfo` ScriptableObject with identity, version, company, language, and URL metadata.
- `NcApp` static accessor that loads the shared asset from `Resources/NappyCat/NcAppInfo`.
- `NcAppConstsGenerator` editor tool that creates strongly-typed constants under `Nc.Keys.App.*` (targets Foundation when present, otherwise `Assets/NappyCat/NcKeys.Generated/`).
- Project Settings integration for quick edits without hunting for the asset.

## Documentation
- See `Documentation~/index.md` for setup, runtime access, and codegen targets.

## Changelog & License
- See `CHANGELOG.md` for release history.
- Licensed under the MIT License (`LICENSE.md`).

---

© 2025 NAPPY CAT Games
