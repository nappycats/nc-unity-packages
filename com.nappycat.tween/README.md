# NappyCat • Core Tween

<div style="background:linear-gradient(135deg,#0d1117,#1a1026);border:1px solid #bf7af0;border-radius:10px;padding:12px;color:#c9d1d9;">
<strong style="color:#d2a8ff">Zero-GC tweens, tiny API.</strong> Delegate-driven tweens, pooled sequences, manual ticking, and Unity helpers.
</div>

`com.NappyCat.Tween` is a zero-GC tweening runtime for Unity 2021.3+. It exposes a tiny API surface centred around delegate-driven tweens, pooled sequences, and manual ticking, making it trivial to integrate with gameplay code, tools, and custom editors.

---

## Package Facts

| Item | Value |
| --- | --- |
| Assembly (runtime) | `NappyCat.Tween` |
| Assembly (editor) | `NappyCat.Tween.Editor` |
| Unity requirement | 2021.3 LTS or newer |
| Dependencies | none |

---

## Installation

1. Copy the `com.NappyCat.Tween` folder into your project’s `Packages/` directory.
2. In Unity open **Window → Package Manager → + → Add package from disk…** and select `Packages/com.NappyCat.Tween/package.json`.
3. (Optional) Import the **Core Samples** package sample to get a ready-to-run scene and sandbox scripts.

---

## Quick Start

```csharp
using NappyCat;
using UnityEngine;

public sealed class MoveUpAndBack : MonoBehaviour
{
    NappyCat.Tween.TweenHandle _handle;

    void OnEnable()
    {
        _handle = Nc.Tween.To(
            () => transform.position,
            value => transform.position = value,
            transform.position + Vector3.up * 2f,
            new NcTweenOptions(duration: 0.8f, ease: NcEase.OutCubic));

        Nc.Tween.OnComplete(_handle, () => Debug.Log("Move complete"));
    }

    void OnDisable()
    {
        Nc.Tween.Kill(_handle);
    }
}
```

Each `NcTween.To` call fetches a pooled tween, configures the getter/setter and timing options, and starts playback immediately. Use the returned `TweenHandle` to pause, kill, or append callbacks.

---

## Key Concepts

### <span style="color:#d2a8ff">Delegate getters & setters</span>
Tweens read and write values through `Func<T>` and `Action<T>` delegates. Keep them allocation-free by using inline lambdas or cached methods. Supported value types:

- `float`
- `Vector2`, `Vector3`, `Vector4`
- `Quaternion`
- `Color`

### <span style="color:#79c0ff">NcTweenRunner</span>
The first tween created spawns an `NcTweenRunner` GameObject (marked `DontDestroyOnLoad`) that calls `NcTween.Tick` every frame. Call `NcTweenRunner.EnsureRunner()` if you need to tweak it ahead of time (e.g., make it non-persistent).

### <span style="color:#56d364">Groups</span>
`NcTweenOptions.WithGroup(groupId)` tags tweens. Use `NcTween.KillGroup(id, complete)` or `NcTween.PauseGroup(id, pause)` to control whole sets of tweens.

### <span style="color:#ffa657">Manual ticking</span>
Pass `NcClock.Manual` in `NcTweenOptions` to opt out of the automatic runner. Drive these tweens yourself via `NcTween.ManualTick(deltaTime)`—ideal for Timeline, simulation playback, or custom editors.

### <span style="color:#8ae9c1">Sequences</span>
`NcSequence` orchestrates multiple tweens declaratively:

```csharp
new NcSequence()
    .Then(() => transform.NcMoveTo(target.position, 0.25f))
    .Also(() => light.NcIntensityTo(5f, 0.25f))
    .Then(() => transform.NcRotateTo(Quaternion.identity, 0.2f))
    .OnComplete(() => Debug.Log("Sequence done"))
    .Play();
```

Sequences are pooled and allocation-free once created.

### <span style="color:#79c0ff">Extension helpers</span>
The package includes extension methods for common Unity components (Transform, CanvasGroup, Image, SpriteRenderer, TMP_Text, Renderer), mirroring the behaviour of the core helpers while keeping call sites succinct.

---

## Tooling & Samples

- **Benchmarks** – Run **Window → NappyCat → Perf → Run 1000 Tween Test** to validate GC behaviour and average tick cost on your machine.
- **Sample scene generator** – **Create → NappyCat → Core → Playground Scene** scaffold a demo scene using the `NcPlayground` sample script.
- **Reference material** – `Documentation~/core-tween-webdoc.html` provides a dynamic single-page manual; `Documentation~/api/CoreTweenAPI.md` mirrors Unity’s scripting reference style.

---

## When to use CueFX
`com.nappycat.cuefx` builds on Core Tween with designer-facing presets, camera/audio/haptic cues, and Timeline integration. Use Core when you want the lowest-level control; install CueFX when you need ready-made FX systems while keeping the same runtime guarantees.

---

## Support & License

Core Tween is released under MIT. Pull requests and issues are welcome via the Nappy Cat repositories or contact listed in `package.json`.
