# NappyCat • Extensions API

Zero-alloc helpers for math, collections, Unity types, and utilities.

## Surface
### Math
- NcMath (Smooth01, Remap, Random*)
- NcMathExtensions (WithX/Y/Z, Rotate, NormalizeAngle)

### Collections
- NcCollectionsExtensions (Shuffle, RandomItem)
- NcArrayExtensions (RandomValue)
- NcDictionaryExtensions (AddOrUpdate, TryAdd)

### Unity
- NcUnityExtensions for Transform/RectTransform/Bounds/Rect/Camera/Renderer/LayerMask/ScrollRect
- NcTimeExtensions, NcStringExtensions

## Example
```csharp
using NappyCat.Extensions;
var pos = transform.position.NcWithY(2f);
var any = myList.NcRandomItem();
```
