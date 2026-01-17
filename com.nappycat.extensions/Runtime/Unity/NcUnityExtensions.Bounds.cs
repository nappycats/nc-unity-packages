// Copyright (c) 2025 Nappy Cat. All rights reserved.
// Packages/com.nappycat.extensions/Runtime/Unity/NcUnityExtensions.Bounds.cs
using UnityEngine;

namespace NappyCat.Extensions
{
    public static partial class NcUnityExtensions
    {
        public static Vector3 NcRandomPoint(this Bounds bounds)
        {
            return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z)
            );
        }

        public static Bounds NcGetColliderBounds(this GameObject go)
        {
            if (go.TryGetComponent<Collider>(out var col)) return col.bounds;
            if (go.TryGetComponent<Collider2D>(out var col2)) return col2.bounds;

            var c = go.GetComponentInChildren<Collider>();
            if (c != null)
            {
                var total = c.bounds;
                foreach (var each in go.GetComponentsInChildren<Collider>()) total.Encapsulate(each.bounds);
                return total;
            }

            var c2 = go.GetComponentInChildren<Collider2D>();
            if (c2 != null)
            {
                var total = c2.bounds;
                foreach (var each in go.GetComponentsInChildren<Collider2D>()) total.Encapsulate(each.bounds);
                return total;
            }

            return new Bounds(Vector3.zero, Vector3.zero);
        }

        public static Bounds NcGetRendererBounds(this GameObject go)
        {
            if (go.TryGetComponent<Renderer>(out var r)) return r.bounds;
            var child = go.GetComponentInChildren<Renderer>();
            if (child != null)
            {
                var total = child.bounds;
                foreach (var each in go.GetComponentsInChildren<Renderer>()) total.Encapsulate(each.bounds);
                return total;
            }
            return new Bounds(Vector3.zero, Vector3.zero);
        }
    }
}

