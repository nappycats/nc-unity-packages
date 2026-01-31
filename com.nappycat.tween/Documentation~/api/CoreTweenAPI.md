# NappyCat Tween API Reference

This reference follows the style of the Unity scripting manual. Namespaces are under `NappyCat.Tween` unless noted otherwise.

---

## NcTween (static class)

### Summary
Static facade for creating and controlling tweens. Provides typed `To` overloads, manual ticking, and group management.

### Methods

| Method | Description |
| --- | --- |
| `TweenHandle To(Func<float> getter, Action<float> setter, float to, in NcTweenOptions options, in NcTweenEvents events = default)` | Creates and starts a float tween. |
| `TweenHandle To(Func<Vector2> getter, Action<Vector2> setter, Vector2 to, in NcTweenOptions options, in NcTweenEvents events = default)` | Creates and starts a Vector2 tween. |
| `TweenHandle To(Func<Vector3> getter, Action<Vector3> setter, Vector3 to, in NcTweenOptions options, in NcTweenEvents events = default)` | Creates and starts a Vector3 tween. |
| `TweenHandle To(Func<Vector4> getter, Action<Vector4> setter, Vector4 to, in NcTweenOptions options, in NcTweenEvents events = default)` | Creates and starts a Vector4 tween. |
| `TweenHandle To(Func<Quaternion> getter, Action<Quaternion> setter, Quaternion to, in NcTweenOptions options, in NcTweenEvents events = default)` | Creates and starts a Quaternion tween (slerp). |
| `TweenHandle To(Func<Color> getter, Action<Color> setter, Color to, in NcTweenOptions options, in NcTweenEvents events = default)` | Creates and starts a Color tween. |
| `void Kill(TweenHandle handle, bool complete = false)` | Stops the tween. If `complete` is true the value is snapped to the end state before stopping. |
| `void Pause(TweenHandle handle, bool pause = true)` | Pauses or resumes the tween. |
| `void KillGroup(int groupId, bool complete = false)` | Kills all tweens that were created with the same `NcTweenOptions.GroupId`. |
| `void PauseGroup(int groupId, bool pause = true)` | Pauses or resumes a group. |
| `bool OnStart(TweenHandle handle, Action callback)` | Registers a callback that fires when the tween starts. Returns `false` if the handle is invalid. |
| `bool OnUpdate(TweenHandle handle, Action<float> callback)` | Registers a callback executed every tick with the eased progress (0–1). |
| `bool OnLoop(TweenHandle handle, Action<int> callback)` | Registers a callback when a loop completes. Parameter is remaining loops (or -1 for infinite). |
| `bool OnComplete(TweenHandle handle, Action callback)` | Registers a callback when the tween finishes. |
| `bool OnKill(TweenHandle handle, Action callback)` | Registers a callback when the tween is killed. |
| `void ManualTick(float deltaTime)` | Advances all tweens that use `NcClock.Manual` by `deltaTime` seconds. |
| `NcTweenRunner EnsureRunner()` | Ensures the singleton runner exists (auto-created otherwise). |

### Properties

| Property | Type | Description |
| --- | --- | --- |
| `int ActiveCount` | `int` | Number of active tweens. |
| `bool AnyActive` | `bool` | `true` if at least one tween is active. |
| `double LastTickMicros` | `double` | Microseconds spent in the last tick pass. |

---

## TweenHandle (struct)

### Summary
Lightweight identifier for active tweens. Contains only the tween ID.

### Members

| Member | Type | Description |
| --- | --- | --- |
| `int Id` | `int` | Unique identifier (internal use). |
| `bool IsValid` | `bool` | Returns `true` if the handle currently refers to a live tween. |

### Operators

- Equality and inequality operators compare handles by `Id`.

---

## NcTweenOptions (struct)

### Summary
Immutable options used when creating a tween.

### Fields

| Field | Type | Description |
| --- | --- | --- |
| `float Duration` | `float` | Length of the tween in seconds. Clamped to ≥ 0.000001. |
| `float Delay` | `float` | Optional delay before the tween starts. |
| `NcEase Ease` | `NcEase` | Built-in easing function. |
| `AnimationCurve CustomCurve` | `AnimationCurve` | Optional custom curve (used when `Ease == NcEase.CustomCurve`). |
| `NcLoop Loop` | `NcLoop` | Looping mode (`Once`, `Loop`, `Yoyo`). |
| `int LoopCount` | `int` | Number of loops. `0` means infinite (for Loop/Yoyo). |
| `NcClock Clock` | `NcClock` | Clock used (`Scaled`, `Unscaled`, `Manual`). |
| `int GroupId` | `int` | User-defined group identifier. |

### Constructors

`NcTweenOptions(float duration, float delay = 0f, NcEase ease = NcEase.OutQuad, AnimationCurve customCurve = null, NcLoop loop = NcLoop.Once, int loopCount = 1, NcClock clock = NcClock.Scaled, int groupId = 0)`

### Helper Methods

| Method | Description |
| --- | --- |
| `NcTweenOptions WithGroup(int groupId)` | Returns a copy with a different group ID. |
| `NcTweenOptions WithClock(NcClock clock)` | Returns a copy using another clock. |
| `NcTweenOptions WithLoop(NcLoop loop, int loopCount = 1)` | Returns a copy with new loop settings. |
| `NcTweenOptions WithEase(NcEase ease, AnimationCurve customCurve = null)` | Returns a copy with a different ease/curve. |

---

## NcTweenEvents (struct)

### Summary
Optional callbacks that can be passed during tween creation.

### Fields

| Field | Type | Description |
| --- | --- | --- |
| `Action OnStart` | `Action` | Fired when the tween starts. |
| `Action<float> OnUpdate` | `Action<float>` | Fired each tick with eased progress. |
| `Action<int> OnLoop` | `Action<int>` | Fired when a loop completes. Parameter is loops remaining (or -1 for infinite). |
| `Action OnComplete` | `Action` | Fired on completion. |
| `Action OnKill` | `Action` | Fired when the tween is killed. |

---

## NcLoop (enum)

| Value | Description |
| --- | --- |
| `Once` | Plays once and completes. |
| `Loop` | Repeats from the beginning. |
| `Yoyo` | Alternates forward/backward each loop. |

---

## NcClock (enum)

| Value | Description |
| --- | --- |
| `Scaled` | Uses `Time.deltaTime` (default). |
| `Unscaled` | Uses `Time.unscaledDeltaTime`. |
| `Manual` | Requires explicit calls to `NcTween.ManualTick`. |

---

## NcEase (enum)

Includes standard easing curves: Linear, In/Out variations for Quad, Cubic, Quart, Quint, Sine, Expo, Circ, Back, Elastic, Bounce, plus `CustomCurve`.

---

## NcSequence (class)

### Summary
Lightweight sequence builder that orchestrates serial (`Then`) and parallel (`Also`) tween steps.

### Methods

| Method | Description |
| --- | --- |
| `NcSequence Then(Func<TweenHandle> factory)` | Adds a serial step that starts after previous steps complete. |
| `NcSequence Also(Func<TweenHandle> factory)` | Adds a parallel tween to the most recent serial step. |
| `NcSequence OnComplete(Action callback)` | Registers a callback fired when the sequence finishes. |
| `void Play()` | Starts the sequence. |

### Notes

- The sequence captures the `TweenHandle`s returned by each factory and automatically advances when they complete or are killed.
- Sequences are reusable when built entirely from pooled tweens.

---

## NcTweenRunner (MonoBehaviour)

### Summary
Singleton runner component responsible for calling `NcTween.Tick` every frame. Spawned automatically on first tween creation.

### Members

| Member | Type | Description |
| --- | --- | --- |
| `static NcTweenRunner Instance` | `NcTweenRunner` | The active runner instance. |
| `bool persistentMode` | `bool` | When true, runner survives scene loads (`DontDestroyOnLoad`). |
| `void Setup(bool persistent)` | `internal` | Helper used by the runtime when instantiating the runner. |

### Behaviour

- The runner lives on a hidden GameObject named `~NcTweenRunner`.
- Set `persistentMode` before play if you want scene-local behaviour.
- Disabling the component allows you to manually drive `NcTween.Tick` from your own update loop.

---

## Extension Methods (namespace `NappyCat.Tween`)

The following extension methods are provided for convenience:

| Type | Methods |
| --- | --- |
| `Transform` | `NcMoveTo`, `NcLocalMoveTo`, `NcRotateTo`, `NcLocalRotateTo`, `NcLocalScaleTo` |
| `CanvasGroup` | `NcAlphaTo` |
| `UnityEngine.UI.Image` | `NcFillTo`, `NcColorTo`, `NcAlphaTo` |
| `SpriteRenderer` | `NcColorTo`, `NcAlphaTo` |
| `TMPro.TMP_Text` | `NcAlphaTo` |
| `Renderer` | `NcShaderFloatTo`, `NcShaderColorTo` |

Each extension forwards to `NcTween.To` with sensible defaults and returns a `TweenHandle`.

---

## Example: Manual Clock

```csharp
var options = new NcTweenOptions(1f, clock: NcClock.Manual);
var handle = NcTween.To(() => progress, v => progress = v, 1f, options);

// elsewhere (e.g., Timeline playable):
NcTween.ManualTick(deltaTime);
```

---

## Related Packages

- **com.nappycat.cuefx** – higher level FX toolkit powered by this runtime.
- **com.nappycat.tools.catinspector** – editor framework that includes preview widgets for manual ticking.
