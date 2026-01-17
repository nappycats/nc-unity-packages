# Extensions (`com.nappycat.extensions`)

<div style="background:#0d1117;padding:12px;border-radius:8px;border:1px solid #30363d">
<strong style="color:#79c0ff">Zero‑alloc helpers</strong> across math, collections, Unity, and utilities. Quick reference below with light color hints per category.
</div>

## <span style="color:#e29b40">Math</span>
- `NcMath.Smooth01(float t)`: smoothstep without clamping (0→1 easing).
- `NcMath.Remap(v, a1, b1, a2, b2)`: remap value from [a1,b1] to [a2,b2].
- `NcMath.NcRoundToNearestHalf(float v)`: round to .0/.5 steps.
- Random: `NcRandomVector2/3`, `NcRandomPointOnCircle/Sphere`.
- Geometry: `NcRotatePointAroundPivot`, `NcRotatePointAroundPivot2D`, `NcDistancePointToLine`, `NcProjectPointOnLine`, `NcAngleBetween360`, `NcAngleDirection`.
- Extensions (`NcMathExtensions`):
  - Floats: `NcSmooth01`, `NcRemap`, `NcNormalizeAngle`, `NcRoundDown(decimals)`.
  - Vector2/3: `NcWithX/Y/Z`, `NcAddX/Y` (Vector2), `NcRotate` (2D), `NcInvert`, `NcProject`/`NcReject`, `NcRound`.
  - Closest helpers: `Vector2/3.NcGetClosest(IEnumerable/array)`.
  - Pivot/line helpers: `NcRotateAroundPivot`, `NcRotateAroundPivot2D`, `NcDistanceToLine`, `NcProjectOnLine`.
  - Angles: `Vector2.NcAngleBetween360`, `Vector3.NcAngleDirection`.

## <span style="color:#56d364">Collections</span>
- Lists (`NcCollectionsExtensions`):
  - `NcForEachNoAlloc(Action<T>)`: index-based loop without allocs.
  - `NcSwap(i,j)`, `NcShuffle()`: in-place Fisher–Yates.
  - `NcGetFirst/NcGetLast()`: safe access with default fallback.
  - `NcClone()`: shallow copy to new `List<T>`.
  - `NcRandomItem()`: random element (default if empty).
  - `NcRemoveRandom()`: remove and return random element (throws if empty).
- Arrays (`NcArrayExtensions`):
  - `NcRandomValue()`: random element (default if empty).
  - `NcShuffle()`: in-place Fisher–Yates.
- Dictionaries (`NcDictionaryExtensions`):
  - `NcKeyByValue()`: first key matching a value (linear search).
  - `NcTryRemove(key, out value)`: remove if present.
  - `NcAddOrUpdate(key, value/update)` overloads: set new or update existing.
  - `NcTryAdd(key, value)`: add if key absent.

## <span style="color:#79c0ff">Unity helpers</span> (`NcUnityExtensions.*`)
- Transform/Component/GameObject:
  - `NcTryGetComponent<T>(out T)`, `NcFindOrAddComponent<T>()`.
  - `NcSetPositionX/Y/Z`, `NcSetLocalPosition*`, `NcResetLocal`.
  - `NcSetLayerRecursive(int)`, `NcSetActiveIf(bool)`, `NcDestroyChildren()`.
- Rect/RectTransform/UI:
  - `RectTransform`: `NcSetLeft/Right/Top/Bottom`, `NcSetWidth/Height`, `NcSetAnchor`, `NcSetPivot`, `NcToScreenRect(Camera)`.
  - `Rect`: `NcWithX/Y/W/H`, `NcContains(Rect other)`, `NcCenter`.
  - `ScrollRect`: `NcScrollToTop/Bottom/Normalized(float)`.
- Bounds:
  - `NcEncapsulate(Bounds)`, `NcEncapsulate(Vector3)`, `NcExpand(float)`, `NcSize2D`, `NcCenter2D`.
- Camera:
  - `NcWorldSpaceWidth/Height(float distance)`, `NcScreenToWorldPoint(Vector3)`.
- LayerMask:
  - `NcContains(int layer)`, `NcAddLayer(int layer)`, `NcRemoveLayer(int layer)`.
- Renderer:
  - `NcBoundsWorld()`, `NcBoundsLocal()`.

## <span style="color:#d2a8ff">Text & Time</span>
- `NcStringExtensions.NcIsNullOrWhiteSpace()`
- `NcTimeExtensions.NcToHumanSeconds()`: formats seconds as `1h 02m 05s`.

## Usage snippet
```csharp
using NappyCat.Extensions;

var pos = transform.position.NcWithY(2f);
var angle = 185f.NcNormalizeAngle(); // -> 185 (wrapped to 0-360)
var closest = playerPos.NcGetClosest(enemyPositions);
if (myLayerMask.NcContains(gameObject.layer)) { /* ... */ }
```

## Links
- README: `../README.md`
- Changelog: `../CHANGELOG.md`
