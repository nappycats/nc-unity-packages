/*
 * NAPPY CAT
 *
 * Copyright © 2025 NAPPY CAT Games
 * http://nappycat.net
 *
 * Author: Stan Nesi
 *
 * File: Packages/com.nappycat.extensions/Runtime/Unity/NcUnityExtensions.Transform.cs
 */
using UnityEngine;

namespace NappyCat.Extensions
{
    public static partial class NcUnityExtensions
    {
        public static Bounds NcTransformBounds(this Transform t, Bounds b)
        {
            var c = t.TransformPoint(b.center);
            var ex = t.TransformVector(b.extents);
            return new Bounds(c, new Vector3(Mathf.Abs(ex.x) * 2, Mathf.Abs(ex.y) * 2, Mathf.Abs(ex.z) * 2));
        }

        public static void NcDestroyAllChildren(this Transform transform)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                var child = transform.GetChild(i).gameObject;
                if (Application.isPlaying) Object.Destroy(child);
                else Object.DestroyImmediate(child);
            }
        }

        public static Transform NcFindDeepChildBreadthFirst(this Transform parent, string name)
        {
            var queue = new System.Collections.Generic.Queue<Transform>();
            queue.Enqueue(parent);
            while (queue.Count > 0)
            {
                var child = queue.Dequeue();
                if (child.name == name) return child;
                foreach (Transform t in child) queue.Enqueue(t);
            }
            return null;
        }

        public static Transform NcFindDeepChildDepthFirst(this Transform parent, string name)
        {
            foreach (Transform child in parent)
            {
                if (child.name == name) return child;
                var result = child.NcFindDeepChildDepthFirst(name);
                if (result != null) return result;
            }
            return null;
        }

        public static void NcChangeLayersRecursively(this Transform transform, string layerName)
        {
            transform.gameObject.layer = LayerMask.NameToLayer(layerName);
            foreach (Transform child in transform) child.NcChangeLayersRecursively(layerName);
        }

        public static void NcChangeLayersRecursively(this Transform transform, int layerIndex)
        {
            transform.gameObject.layer = layerIndex;
            foreach (Transform child in transform) child.NcChangeLayersRecursively(layerIndex);
        }

        public static void NcSetPositionX(this Transform transform, float x)
            => transform.position = new Vector3(x, transform.position.y, transform.position.z);

        public static void NcSetPositionY(this Transform transform, float y)
            => transform.position = new Vector3(transform.position.x, y, transform.position.z);

        public static void NcSetPositionZ(this Transform transform, float z)
            => transform.position = new Vector3(transform.position.x, transform.position.y, z);

        public static void NcAddChildren(this Transform transform, GameObject[] children)
        {
            if (children == null) return;
            for (int i = 0; i < children.Length; i++)
                if (children[i]) children[i].transform.parent = transform;
        }

        public static void NcAddChildren(this Transform transform, Component[] children)
        {
            if (children == null) return;
            for (int i = 0; i < children.Length; i++)
                if (children[i]) children[i].transform.parent = transform;
        }

        public static void NcResetChildPositions(this Transform transform, bool recursive = false)
        {
            foreach (Transform child in transform)
            {
                child.position = Vector3.zero;
                if (recursive) child.NcResetChildPositions(true);
            }
        }

        public static void NcResetTransformation(this Transform transform)
        {
            transform.position = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
    }
}