# NcScriptable Cookbook

NcScriptable is a lightweight ScriptableObject base that adds a stable Id (GUID), versioning, UTC timestamps, a validation hook, a local Changed event, and runtime‑safe cloning.

Key members
- `string Id` (alias `Guid`): stable identifier (editor‑assigned GUID)
- `int Version`: bumps on editor validation
- `DateTime CreatedUtc`, `ModifiedUtc`
- `event Action<NcScriptable> Changed`
- `protected virtual void Validate()` and `protected void NotifyChanged()`
- `T Clone<T>()`, `NcScriptable Clone()`

Usage pattern
1) Derive your asset from `NcScriptable`.
2) Override `Validate()` to keep data consistent (called on editor changes).
3) Raise `NotifyChanged()` when you mutate from code (runtime or editor tooling).

---

Example: CueFxPreset : NcScriptable
```csharp
using UnityEngine;
using NappyCat.Foundation;

[CreateAssetMenu(menuName = "Nappy Cat/CueFX/CuePreset")] 
public sealed class CueFxPreset : NcScriptable
{
    [Header("Meta")] public string tag = "FX/Default";
    [Min(0)] public int groupId;

    // Called in editor when values change
    protected override void Validate()
    {
        if (string.IsNullOrWhiteSpace(tag)) tag = "FX/Default";
        // Trigger downstream caches (e.g., compiled specs) as needed
        // CompiledSpecsCache.Invalidate(this);
    }
}
```

Example: CueFxLibrary : NcScriptable
```csharp
using System;
using System.Collections.Generic;
using UnityEngine;
using NappyCat.Foundation;

[CreateAssetMenu(menuName = "Nappy Cat/CueFX/CueBank")] 
public sealed class CueFxLibrary : NcScriptable
{
    [Serializable] public struct Entry { public string tag; public CueFxPreset preset; }
    public List<Entry> entries = new();

    public bool TryGet(string tag, out CueFxPreset preset)
    {
        for (int i = 0; i < entries.Count; i++)
        {
            var e = entries[i];
            if (e.tag == tag && e.preset) { preset = e.preset; return true; }
        }
        preset = null; return false;
    }

    protected override void Validate()
    {
        // Remove nulls and trim tags
        entries.RemoveAll(e => e.preset == null);
        for (int i = 0; i < entries.Count; i++)
            entries[i] = new Entry { tag = entries[i].tag?.Trim() ?? string.Empty, preset = entries[i].preset };
    }
}
```

Example: NcatShapeAsset (NcUI)
```csharp
using UnityEngine;
using NappyCat.Foundation;

[CreateAssetMenu(menuName = "Nappy Cat/UI/Shape Style")] 
public sealed class NcatShapeAsset : NcScriptable
{
    [Header("Stroke/Fill")] public Material stroke; public float strokeWidth = 1f; public Material fill;
    protected override void Validate()
    {
        strokeWidth = Mathf.Max(0f, strokeWidth);
    }
}
```

Example: NcAudioPlaylist (Pawdio)
```csharp
using System.Collections.Generic;
using UnityEngine;
using NappyCat.Foundation;

[CreateAssetMenu(menuName = "Nappy Cat/Audio/Playlist")] 
public sealed class NcAudioPlaylist : NcScriptable
{
    [System.Serializable] public struct Entry { public AudioClip clip; public float volume; }
    public List<Entry> tracks = new();
    protected override void Validate()
    {
        // Clamp volumes and drop missing clips
        for (int i = 0; i < tracks.Count; i++)
            tracks[i] = new Entry { clip = tracks[i].clip, volume = Mathf.Clamp01(tracks[i].volume) };
        tracks.RemoveAll(t => t.clip == null);
    }
}
```

Example: CatTheme : NcScriptable
```csharp
using UnityEngine;
using NappyCat.Foundation;

[CreateAssetMenu(menuName = "Nappy Cat/UI/Theme")] 
public sealed class CatTheme : NcScriptable
{
    public string themeId = "Default";
    public ScriptableObject tokens; // replace with your token container
    protected override void Validate()
    {
        if (string.IsNullOrWhiteSpace(themeId)) themeId = "Default";
        // Optionally normalize token values here
    }
}
```

Tips
- Use `Changed +=` to refresh live editors/preview windows.
- Use `Clone<T>()` when you need a temporary runtime copy without touching the asset on disk.
- Set `ReadOnlyInBuild = true` on assets you never want to mutate in builds.
