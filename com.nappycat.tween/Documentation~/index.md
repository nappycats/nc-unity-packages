# NappyCat Core Tween (Documentation)

`com.NappyCat.Tween` is a lightweight tweening runtime designed for zero-GC gameplay code. This page summarises the public surface, common patterns, and editor tooling shipped with the package.

Refer to the companion package `com.nappycat.cuefx` for higher-level presets, Timeline helpers, and optional editor utilities.

---

## 1. Package Overview

| Assembly | Namespace | Purpose |
| --- | --- | --- |
| `NappyCat.Tween` | `NappyCat.Tween` | Runtime tweens, sequences, easing, runner |
| `NappyCat.Tween.Editor` | `NappyCat.Tween` | API guard & perf harness |

Key characteristics:

- Delegate-driven getter/setter pairs keep the hot path allocation free after setup.
- All tweens are pooled; `NcTween` helpers fetch from the pool, configure, and start playback.
- A single `NcTweenRunner` GameObject (auto-spawned) ticks active tweens every frame.
- Manual ticking is supported for deterministic playback in Timeline, Editor previews, or unit tests.

---

## 2. Installation

1. Copy the `com.NappyCat.Tween` folder into your project's `Packages/` directory.
2. In Unity, choose **Window → Package Manager → + → Add package from disk…** and select the package folder.
3. (Optional) Import the **Core Samples** package sample. It installs the `NcPlayground` script and a menu item that spawns a ready-to-run scene.

---

## 3. Quick Start

```csharp
using NappyCat.Tween;
using UnityEngine;

public sealed class LiftAndDrop : MonoBehaviour
{
    TweenHandle _handle;

    void OnEnable()
    {
        _handle = NcTween.To(
            () => transform.position,
            value => transform.position = value,
            transform.position + Vector3.up * 2f,
            new NcTweenOptions(duration: 0.9f, ease: NcEase.OutCubic));

        NcTween.OnComplete(_handle, () => Debug.Log("Lift complete"));
    }

    void OnDisable()
    {
        NcTween.Kill(_handle);
    }
}
```

Highlights:

- `NcTween.To` (and its vector/color counterparts) configure a tween and return a `TweenHandle` that can be paused, killed, or extended with events.
- `NcTweenOptions` stores immutable timing data; use the `.With*` helpers to derive variants without re-typing parameters.
- `NcTweenRunner.EnsureRunner()` forces the runner to exist if you need to customise it (e.g., set `persistentMode`).

---

## 4. Core API Surface

### 4.1 Tween Helpers

| Method | Description |
| --- | --- |
| `NcTween.To` (float/Vector2/Vector3/Vector4/Quaternion/Color) | Schedule a tween between two values. |
| `NcTween.Kill` | Stop a tween; optionally snap to completion first. |
| `NcTween.Pause` | Toggle pause state on a tween. |
| `NcTween.OnStart/OnUpdate/OnLoop/OnComplete/OnKill` | Attach callbacks after the fact; they do not allocate. |

### 4.2 Sequences

`NcSequence` composes multiple tweens. Typical usage:

```csharp
var seq = new NcSequence()
    .Then(() => NcTween.To(...))
    .Also(() => NcTween.To(...))
    .OnComplete(() => Debug.Log("Sequence finished"));
seq.Play();
```

`Then` enqueues serial steps, `Also` groups them in parallel, and `OnComplete` fires once the queue is empty.

### 4.3 Extension Methods

`Runtime/Extensions` contains convenience methods for common Unity types:

- `Transform.NcMoveTo`, `NcLocalMoveTo`, `NcRotateTo`, `NcLocalScaleTo`
- `CanvasGroup.NcAlphaTo`
- `Image.NcFillTo`, `Image.NcColorTo`
- `SpriteRenderer.NcColorTo`
- `TMP_Text.NcAlphaTo`
- `Renderer.NcShaderFloatTo`, `Renderer.NcShaderColorTo`

These simply forward to the core helpers but keep call-sites succinct.

### 4.4 Manual Ticking

- `NcTween.Tick(deltaScaled, deltaUnscaled)` is called by the runner each `Update`.
- `NcTween.ManualTick(delta)` accumulates manual delta for tweens using `NcClock.Manual`.
- Disable the runner (set `enabled = false`) if you intend to drive `Tick` yourself in play mode.

### 4.5 Diagnostics

- `NcTween.ActiveCount` returns the number of live tweens.
- `NcTween.LastTickMicros` reports the microseconds spent in the last tick pass.
- The editor window **NappyCat → Perf → Run 1000 Tween Test** benchmarks 1,000 manual-clock tweens and logs average µs/tick and GC totals.
- `NcTweenContractGuard` runs at editor load and can be triggered via **NappyCat → Core Tween → Validate Contract** to ensure public symbols exist.

---

## 5. Working With Groups

`NcTweenOptions.WithGroup(groupId)` assigns a logical group integer. You can then call:

| API | Description |
| --- | --- |
| `NcTween.KillGroup(groupId, complete)` | Kill every tween in that group. |
| `NcTween.PauseGroup(groupId, pause)` | Pause/resume an entire group. |

Groups are useful for pausing a UI panel or cancelling all tweens triggered by a specific gameplay entity.

---

## 6. Editor Extras

- **Sample Scene Generator**: **Create → NappyCat → Core → Playground Scene** scaffolds a demo scene with a cube using the `NcPlayground` script.
- **Perf Harness**: **Window → NappyCat → Perf → Run 1000 Tween Test** spawns 1,000 tweens and ticks them manually for 300 frames, logging GC and average cost.
- **ApiGuard**: ensures enums (`NcClock`, `NcLoop`), handles, and helper methods remain present between releases.

---

## 7. Best Practices

1. Hold onto `TweenHandle` references if you need to cancel animations on `OnDisable`/`OnDestroy`.
2. Prefer the provided extension methods for clarity, but remember they are simple wrappers—mix with the raw helpers when needed.
3. Avoid capturing large closures inside getter/setter delegates; keep them static or inline lambdas accessing fields.
4. Use `NcTweenRunner.PersistentMode = true` if you spawn the runner in a bootstrap scene and want it to survive scene loads.
5. Run the perf harness periodically in automated tests to catch regressions in GC behaviour.

---

## 8. Samples & Next Steps

- **Core Samples** (package sample) demonstrate tweakable parameters and runner interactions.
- **CueFX Samples** build upon the same engine to provide higher-level UI, gameplay, and Timeline scenarios.
- Use the ADRs in each package for architecture decisions and rationale behind the design choices.

---

## 9. Support & License

The package is released under MIT. For questions, reach out via the Nappy Cat repositories or the support channel listed in `package.json`.

Happy tweening!
