# Tags (`com.nappycat.tags`)

Hierarchy-aware tagging for prefabs, ScriptableObjects, and loose assets with Nc.Keys generation.

## Setup
1) Create `NcTag` assets (path, color, icon, parent, synonyms).  
2) Tag scene objects with `NcTaggable` or assets with `NcTagContainer`.  
3) Rebuild the index via `Nappy Cat/Tags/Rebuild Tag Index` (writes `Resources/NappyCat/NcTagIndex.asset`).  
4) Generate keys via `Nappy Cat/Nc.Keys/Generate Keys (Tags)` when tags change.

## Runtime queries
```csharp
using NappyCat.Tags;

// Find all assets with any of the specified tags
var hits = NcTagQuery.AnyOf("Gameplay/Enemy", "VFX/Fire");
```

## Codegen targets
- With Foundation (`NC_PKG_FOUNDATION`): `Packages/com.nappycat.foundation/Runtime/NcKeys/NcKeys.Tags.g.cs`.
- Without Foundation: `Assets/NappyCat/NcKeys.Generated/NcKeys.Tags.g.cs`.

## Links
- README: `../README.md`
- Changelog: `../CHANGELOG.md`
