# NappyCat • Foundation

<div style="background:linear-gradient(135deg,#0d1117,#111c26);border:1px solid #238636;border-radius:10px;padding:12px;color:#c9d1d9;">
<strong style="color:#56d364">Bedrock utilities.</strong> Minimal, stable primitives used by every other Nappy Cat package.
</div>

Includes
- Foundation/NcDefines: define names used across NappyCat (doc‑level constants)
- Foundation/NcScriptable: ScriptableObject base with GUID/Version/Timestamps + Validate/Changed
- Foundation/NcSettingsBase + NcSettings<T> + NcSettingsLocator: typed singletons + discovery under Resources/NappyCat/Settings
- Patterns/NcSingleton + NcPersistentSingleton: lightweight singletons
- Editor/Foundation/NcSettingsCreator: menu to create the Resources/NappyCat/Settings folder
- Nc.Keys hub for shared constants such as Tags/Params/App info

Usage
- Define typed settings per domain via `public sealed class MySettings : NcSettings<MySettings> { ... }`.
- Put the asset under `Resources/NappyCat/Settings/` (default path). Access via `MySettings.Instance` or `NcSettingsLocator.Get<MySettings>()`.
- Use NcSingleton/NcPersistentSingleton sparingly; consider DI for large systems.

Docs & examples
- See `Documentation~/NcScriptable.Cookbook.md` for concrete patterns:
  - CueFxPreset / CueFxLibrary
  - NcUI Theme
  - Shape Style asset
  - Audio Playlist
  - App Settings (NcAppSettings)

Documentation
- `Documentation~/` contains cookbooks and design notes for core utilities.

Samples
- Import `Samples~/AppSettingsQuickStart` to see a minimal bootstrap that reads `NcAppSettings.Instance`.
