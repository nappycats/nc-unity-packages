# Pool (`com.nappycat.pool`)

Lightweight pooling for C# objects and GameObjects, with Nc hub helpers.

## Core APIs
- `NcPool<T>`: factory + optional reset + warmup.
- `NcObjectPool<T>` + `INcPoolPolicy<T>`: lifecycle hooks (Create/OnRent/OnReturn/Destroy).
- `NcGoPool`: prefab pool with activation/parenting and `NcPooledBehaviour` hooks.
- `Nc.Pool.Create(...)`: quick helper that returns an `NcPool<T>`.

## Usage
```csharp
using NappyCat.Pool;

// Objects
var pool = new NcPool<byte[]>(() => new byte[1024], buf => System.Array.Clear(buf, 0, buf.Length), 8);
var buffer = pool.Get();
pool.Release(buffer);

// GameObjects
var goPool = new NcGoPool(prefab, root, warm: 4);
var inst = goPool.Get();
goPool.Release(inst);
```

Attach `NcPooledBehaviour` to pooled prefabs to reset state on acquire/release (hooks are invoked for all components on the pooled GameObject).

## Samples
- `Samples~/PoolingQuickStart`: press Space to spawn/recycle pooled cubes.

## Links
- README: `../README.md`
- Changelog: `../CHANGELOG.md`
