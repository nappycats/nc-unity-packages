// Copyright (c) 2025 Nappy Cat. All rights reserved.
// Packages/com.nappycat.extensions/Runtime/Math/NcMathExtensions.cs
using UnityEngine;

namespace NappyCat.Extensions
{
    public static class NcMathExtensions
    {
        // Pure helpers live in NcMath; extension wrappers for convenience
        public static float NcSmooth01(this float t) => NcMath.Smooth01(t);
        public static float NcRemap(this float v, float a1, float b1, float a2, float b2) => NcMath.Remap(v, a1, b1, a2, b2);
        public static Vector3 NcWithX(this Vector3 v, float x) { v.x = x; return v; }
        public static Vector3 NcWithY(this Vector3 v, float y) { v.y = y; return v; }
        public static Vector3 NcWithZ(this Vector3 v, float z) { v.z = z; return v; }

        // Vector2 helpers
        public static Vector2 NcWithX(this Vector2 v, float x) { v.x = x; return v; }
        public static Vector2 NcWithY(this Vector2 v, float y) { v.y = y; return v; }
        public static Vector2 NcAddX(this Vector2 v, float dx) { v.x += dx; return v; }
        public static Vector2 NcAddY(this Vector2 v, float dy) { v.y += dy; return v; }
        public static Vector2 NcRotate(this Vector2 v, float angleDeg)
        {
            float sin = Mathf.Sin(angleDeg * Mathf.Deg2Rad);
            float cos = Mathf.Cos(angleDeg * Mathf.Deg2Rad);
            float tx = v.x; float ty = v.y;
            v.x = cos * tx - sin * ty;
            v.y = sin * tx + cos * ty;
            return v;
        }

        // Vector3 helpers
        public static Vector3 NcInvert(this Vector3 v) => new Vector3(1f / v.x, 1f / v.y, 1f / v.z);
        public static Vector3 NcProject(this Vector3 v, Vector3 onto)
        {
            var dot = Vector3.Dot(v, onto);
            return dot * onto;
        }
        public static Vector3 NcReject(this Vector3 v, Vector3 onto) => v - v.NcProject(onto);
        public static Vector3 NcRound(this Vector3 v)
        {
            v.x = Mathf.Round(v.x); v.y = Mathf.Round(v.y); v.z = Mathf.Round(v.z); return v;
        }

        // Closest point helpers
        public static Vector2 NcGetClosest(this Vector2 p, System.Collections.Generic.IEnumerable<Vector2> others)
        {
            var closest = Vector2.zero; var best = float.PositiveInfinity;
            foreach (var q in others)
            {
                var d = (p - q).sqrMagnitude; if (d < best) { best = d; closest = q; }
            }
            return closest;
        }
        public static Vector3 NcGetClosest(this Vector3 p, System.Collections.Generic.IEnumerable<Vector3> others)
        {
            var closest = Vector3.zero; var best = float.PositiveInfinity;
            foreach (var q in others)
            {
                var d = (p - q).sqrMagnitude; if (d < best) { best = d; closest = q; }
            }
            return closest;
        }
        public static Vector3 NcGetClosest(this Vector3 p, Vector3[] others)
        {
            var closest = Vector3.zero; var best = float.PositiveInfinity;
            for (int i = 0; i < others.Length; i++)
            {
                var d = (p - others[i]).sqrMagnitude; if (d < best) { best = d; closest = others[i]; }
            }
            return closest;
        }

        // Wrappers to pure NcMath helpers as extensions
        public static Vector3 NcRotateAroundPivot(this Vector3 point, Vector3 pivot, Vector3 eulerAngles)
            => NcMath.NcRotatePointAroundPivot(point, pivot, eulerAngles);

        public static Vector3 NcRotateAroundPivot2D(this Vector3 point, Vector3 pivot, float angleDeg)
            => NcMath.NcRotatePointAroundPivot2D(point, pivot, angleDeg);

        public static float NcDistanceToLine(this Vector3 point, Vector3 lineStart, Vector3 lineEnd)
            => NcMath.NcDistancePointToLine(point, lineStart, lineEnd);

        public static Vector3 NcProjectOnLine(this Vector3 point, Vector3 lineStart, Vector3 lineEnd)
            => NcMath.NcProjectPointOnLine(point, lineStart, lineEnd);

        public static float NcAngleBetween360(this Vector2 a, Vector2 b)
            => NcMath.NcAngleBetween360(a, b);

        public static float NcAngleDirection(this Vector3 a, Vector3 b, Vector3 up)
            => NcMath.NcAngleDirection(a, b, up);

        // Float helpers
        public static float NcNormalizeAngle(this float angleDeg)
        {
            angleDeg %= 360f; if (angleDeg < 0f) angleDeg += 360f; return angleDeg;
        }
        public static float NcRoundDown(this float value, int decimals)
        {
            float pow = Mathf.Pow(10f, decimals);
            return Mathf.Floor(value * pow) / pow;
        }

        // Misc helpers moved to NcMath: rounding, random vectors/points, rotations, angles, projections
    }
}
