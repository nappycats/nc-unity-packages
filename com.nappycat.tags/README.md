# Nappy Cat • Tags

Hierarchy-aware tagging for prefabs, ScriptableObjects, and loose assets. Ship curated tag libraries, generate searchable indices, and query everything with a few helper methods.

## Requirements
- Unity 2021.3 LTS or newer

## Installation
1. Add the package to your `Packages/` directory and register it in the Package Manager.
2. Run Nappy Cat/Tags/Rebuild Tag Index after you create or update tag assignments.
3. Pair with `com.nappycat.foundation` to emit `Nc.Keys.Tags.*` constants (falls back to `Assets/NappyCat/NcKeys.Generated/` if Foundation isn’t present).

## Highlights
- `NcTag` assets with path, display name, color, icon, parent, and synonym metadata.
- `NcTaggable` component for prefabs and scene objects.
- `NcTagContainer` wrapper to tag assets that cannot host MonoBehaviours (sprites, clips, ScriptableObjects, etc.).
- `NcTagIndex` builder that writes `Resources/NappyCat/TagIndex.asset` for runtime queries.
- `NcTagQuery` static helpers: `AnyOf`, `AllOf`, `WhereType`, and cached index access.

## Documentation
- See `Documentation~/index.md` for setup, key generation, and query examples.

## Changelog & License
- See `CHANGELOG.md` for release notes.
- Licensed under the MIT License (`LICENSE.md`).

---

© 2025 NAPPY CAT Games
