# Nappy Cat • Tags

<div style="background:linear-gradient(135deg,#0d1117,#10253f);border:1px solid #c297ff;border-radius:10px;padding:12px;color:#c9d1d9;">
<strong style="color:#d2a8ff">Tag everything with intent.</strong> Curate hierarchies, rebuild indexes, and generate Nc.Keys for typed access.
</div>

Hierarchy-aware tagging for prefabs, ScriptableObjects, and loose assets. Ship curated tag libraries, generate searchable indices, and query everything with a few helper methods.

## Requirements
- Unity 2021.3 LTS or newer

## Installation
1. Add the package to your `Packages/` directory and register it in the Package Manager.
2. Run Nappy Cat/Tags/Rebuild Tag Index after you create or update tag assignments.
3. Pair with `com.nappycat.foundation` to emit `Nc.Keys.Tags.*` constants (falls back to `Assets/NappyCat/NcKeys.Generated/` if Foundation isn’t present).

## Highlights
- <span style="color:#d2a8ff">`NcTag`</span>: assets with path, display name, color, icon, parent, and synonyms.
- <span style="color:#79c0ff">`NcTaggable`</span> for prefabs/scene objects; <span style="color:#79c0ff">`NcTagContainer`</span> for non-MonoBehaviour assets.
- <span style="color:#56d364">`NcTagIndex`</span>: builder writes `Resources/NappyCat/NcTagIndex.asset`.
- <span style="color:#ffa657">`NcTagQuery`</span>: `AnyOf`, `AllOf`, `WhereType`, cached index access.

## Documentation
- See `Documentation~/index.md` for setup, key generation, and query examples.

## Changelog & License
- See `CHANGELOG.md` for release notes.
- Licensed under the MIT License (`LICENSE.md`).

---

© 2025 NAPPY CAT Games
