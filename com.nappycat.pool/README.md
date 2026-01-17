# Nappy Cat • Pool

<div style="background:linear-gradient(135deg,#0d1117,#0f241c);border:1px solid #3fb950;border-radius:10px;padding:12px;color:#c9d1d9;">
<strong style="color:#56d364">Reuse more, allocate less.</strong> Pools for data and GameObjects, plus Nc hub helpers.
</div>

Lightweight pools for objects and GameObjects with an Nc hub helper.

Highlights
- <span style="color:#56d364">`NcPool<T>`</span>: generic pool with factory/reset and optional warmup.
- <span style="color:#79c0ff">`NcGoPool`</span>: prefab pool storing inactive instances under an optional root; calls `NcPooledBehaviour` hooks.
- <span style="color:#d2a8ff">`NcObjectPool<T>` + `INcPoolPolicy<T>`</span>: policy-based lifecycle hooks.
- <span style="color:#ffa657">`Nc.Pool.Create(...)`</span>: hub helper for quick pools (no extra setup).
- `NcPooledBehaviour`: drop-in MonoBehaviour hook for acquire/release when using `NcGoPool`.

Examples
```
using NappyCat.Pool;

var pool = new NcPool<byte[]>(() => new byte[1024], a => System.Array.Clear(a,0,a.Length), 16);
var buf = pool.Get();
// use...
pool.Release(buf);
```

```
using NappyCat.Pool;

var goPool = new NcGoPool(prefab, root, warm:5);
var inst = goPool.Get(parent);
// use...
goPool.Release(inst);
```

Policy-based pool
```
using NappyCat.Pool;

sealed class BulletPolicy : INcPoolPolicy<Bullet>
{
    public Bullet Create() => new Bullet();
    public void OnRent(Bullet b) => b.OnSpawn();
    public void OnReturn(Bullet b) => b.OnDespawn();
    public void Destroy(Bullet b) { /* cleanup */ }
}

var bullets = new NcObjectPool<Bullet>(new BulletPolicy(), warm: 16);
var b = bullets.Rent();
// use...
bullets.Return(b);

// Or via Nc hub:
var hubPool = NappyCat.Nc.Pool.Create(() => new Bullet(), b => b.Reset(), warm:16);
```

## Samples
- `Samples~/PoolingQuickStart`: drop `PoolQuickStart` on a GameObject, hit Play, and press Space to spawn pooled cubes that recycle automatically.

## Documentation
- See `Documentation~/index.md` for API overview and quickstart snippets.
