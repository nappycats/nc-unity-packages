# NappyCat • Tags API

Hierarchy-aware tags, index builder, and Nc.Keys generation.

## Surface
### Assets
- NcTag (path/color/icon/parent/synonyms)
- NcTaggable (scene/prefab)
- NcTagContainer (asset tagging)

### Runtime
- NcTagIndex (Resources/NappyCat/NcTagIndex)
- NcTagQuery.AnyOf/AllOf/WhereType

### Editor
- Rebuild Tag Index
- Generate Keys (Nc.Keys.Tags.*)

## Example
```csharp
var hits = NappyCat.Tags.NcTagQuery.AnyOf("Gameplay/Enemy", "VFX/Fire");
```
