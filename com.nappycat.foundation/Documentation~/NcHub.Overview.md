# NappyCat Nc Hub — Overview

The Nc hub is a single global entry point that surfaces the most common runtime services:

```csharp
using NappyCat;

var move = Nc.Input.ActionVector2("Move");
await Nc.Save.SaveAsync("slot1");
float dt = Nc.Time.Delta(NappyCat.Foundation.Time.NcTimeChannel.Scaled);
var h = Nc.Tween.To(...);
await Nc.Scene.LoadAsync("Assets/Scenes/Level01.unity");
```

## Slots

- `Nc.Input` — core input (`com.nappycat.input`)
  - Unified actions, devices, gestures, haptics, prompts.
- `Nc.Save` — save service (`com.nappycat.save`)
  - Profiles, slots, autosave, schema + migrations.
- `Nc.Time` — time channels (`com.nappycat.time`)
  - Scaled/unscaled/custom channels, slow‑mo/freeze gates.
- `Nc.Tween` — tween engine (`com.nappycat.tween`)
  - Delegate‑based tweens, groups, manual tick, CueFX‑compatible.
- `Nc.Scene` — slice loader (`com.nappycat.scene`)
  - Slice/group orchestration, fades, plus simple `LoadAsync`/`RestartAsync` helpers.
- `Nc.Pool` — pooling helpers (`com.nappycat.pool`)
  - Create `NcPool<T>` instances.
- `Nc.Fsm` — state machines (`com.nappycat.fsm`)
  - Create `NcStateMachine` instances for HFSM flows.
- `Nc.Keys` — generated constants hub (Tags/Params/App, etc.), emitted by optional generators.

Each slot forwards to the underlying core type (e.g., `Nc.Save` → `NcSaveService.Current`), so you can still use the module APIs directly when needed.
