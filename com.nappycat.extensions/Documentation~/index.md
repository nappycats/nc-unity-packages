# Extensions (`com.nappycat.extensions`)

Zero-allocation helpers for math, collections, text/time, and Unity types.

## Highlights
- Math: `NcMath` (Remap, Smooth01, RandomRangeInclusive) and `NcMathExtensions` (NcWithX/Y/Z, NcRotate, NcNormalizeAngle).
- Collections: `NcCollectionsExtensions` (NcForEachNoAlloc, NcShuffle), `NcArrayExtensions` (NcRandomValue), `NcDictionaryExtensions` (NcAddOrUpdate, NcTryAdd).
- Unity: `NcUnityExtensions` for `Transform`, `RectTransform`, `Bounds`, `Rect`, `Camera`, `LayerMask`, `GameObject`, `Component`, `ScrollRect`, `Renderer`.
- Utility: `NcTimeExtensions.NcToHumanSeconds()`, `NcStringExtensions.NcIsNullOrWhiteSpace()`.

## Example
```csharp
using NappyCat.Extensions;

var pos = transform.position.NcWithY(2f);
var angle = 185f.NcNormalizeAngle(); // -> -175
var random = myList.NcRandomItem();
```

## Links
- README: `../README.md`
- Changelog: `../CHANGELOG.md`
