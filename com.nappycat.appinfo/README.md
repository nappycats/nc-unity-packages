# Nappy Cat • App Info

<div style="background:linear-gradient(135deg,#0d1117,#10253f);border:1px solid #1f6feb;border-radius:10px;padding:12px;color:#c9d1d9;">
<strong style="color:#79c0ff">Centralise your app identity.</strong> Store IDs, URLs, and version info in one asset; generate Nc.Keys for code use.
</div>

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
- <span style="color:#d2a8ff">`NcAppInfo`</span>: ScriptableObject with identity, version, company, language, and URL metadata.
- <span style="color:#79c0ff">`Nc.App.Info`</span>: static accessor that loads the shared asset from `Resources/NappyCat/NcAppInfo`.
- <span style="color:#56d364">`NcAppConstsGenerator`</span>: creates strongly-typed constants under `Nc.Keys.App.*` (targets Foundation when present, otherwise `Assets/NappyCat/NcKeys.Generated/`).
- Project Settings integration for quick edits without hunting for the asset.

## Documentation
- See `Documentation~/index.md` for setup, runtime access, and codegen targets.

## Changelog & License
- See `CHANGELOG.md` for release history.
- Licensed under the MIT License (`LICENSE.md`).

---

© 2025 NAPPY CAT Games
