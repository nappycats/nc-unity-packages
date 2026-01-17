# NappyCat • Pool

Lightweight pools for objects and GameObjects with an Nc hub helper.

Highlights
- `NcPool<T>`: generic pool with factory/reset and optional warmup.
- `NcGoPool`: prefab pool storing inactive instances under an optional root.
- `NcObjectPool<T>` + `INcPoolPolicy<T>`: policy-based lifecycle hooks.
- `INcPoolable`: optional lifecycle interface for pooled items.
- `Nc.Pool.Create(...)`: hub helper for quick pools (no extra setup).

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
