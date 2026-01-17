// Copyright (c) 2025 Nappy Cat. All rights reserved.
// Packages/com.nappycat.extensions/Runtime/Math/NcMath.cs
using UnityEngine;

namespace NappyCat.Extensions
{
    /// <summary>
    /// Pure math helpers (non-extension methods).
    /// Keep symmetric/pure operations here to avoid awkward extension semantics.
    /// </summary>
    public static class NcMath
    {
        public static float Smooth01(float t) => t * t * (3f - 2f * t);
        public static float Remap(float v, float a1, float b1, float a2, float b2) => a2 + (v - a1) * (b2 - a2) / (b1 - a1);

        public static float NcRoundToNearestHalf(float v) => v - (v % 0.5f);

        public static Vector2 NcRandomVector2(Vector2 min, Vector2 max)
            => new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));

        public static Vector3 NcRandomVector3(Vector3 min, Vector3 max)
            => new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));

        public static Vector2 NcRandomPointOnCircle(float radius)
            => Random.insideUnitCircle.normalized * radius;

        public static Vector3 NcRandomPointOnSphere(float radius)
            => Random.onUnitSphere * radius;

        public static Vector3 NcRotatePointAroundPivot2D(Vector3 point, Vector3 pivot, float angleDeg)
        {
            float rad = angleDeg * Mathf.Deg2Rad;
            float cos = Mathf.Cos(rad); float sin = Mathf.Sin(rad);
            float rx = cos * (point.x - pivot.x) - sin * (point.y - pivot.y) + pivot.x;
            float ry = sin * (point.x - pivot.x) + cos * (point.y - pivot.y) + pivot.y;
            return new Vector3(rx, ry, point.z);
        }

        public static Vector3 NcRotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 eulerAngles)
        {
            var q = Quaternion.Euler(eulerAngles);
            var dir = point - pivot;
            dir = q * dir;
            return pivot + dir;
        }

        public static float NcAngleBetween360(Vector2 a, Vector2 b)
        {
            float angle = Vector2.Angle(a, b);
            var cross = Vector3.Cross(a, b);
            if (cross.z > 0) angle = 360 - angle;
            return angle;
        }

        public static float NcAngleDirection(Vector3 a, Vector3 b, Vector3 up)
        {
            var cross = Vector3.Cross(a, b);
            return Vector3.Dot(cross, up);
        }

        public static float NcDistancePointToLine(Vector3 point, Vector3 lineStart, Vector3 lineEnd)
            => (NcProjectPointOnLine(point, lineStart, lineEnd) - point).magnitude;

        public static Vector3 NcProjectPointOnLine(Vector3 point, Vector3 lineStart, Vector3 lineEnd)
        {
            Vector3 rhs = point - lineStart;
            Vector3 v = lineEnd - lineStart;
            float mag = v.magnitude;
            Vector3 dir = mag > 1E-06f ? v / mag : v;
            float d = Mathf.Clamp(Vector3.Dot(dir, rhs), 0f, mag);
            return lineStart + dir * d;
        }
    }
}

