# NappyCat • Pool API

Lightweight pooling for C# objects and GameObjects with Nc hub helpers.

## Surface
### Objects
- NcPool<T> (factory/reset/warm)
- NcObjectPool<T> + INcPoolPolicy<T>

### GameObjects
- NcGoPool (prefab, parent, NcPooledBehaviour hooks)

### Hub
- Nc.Pool.Create(...) quick helper

## Example
```csharp
var pool = new NappyCat.Pool.NcGoPool(prefab, transform, warm:4);
var go = pool.Get();
pool.Release(go);
```
