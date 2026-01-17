// Copyright (c) 2025 Nappy Cat. All rights reserved.
// Packages/com.nappycat.extensions/Runtime/Collections/NcCollectionsExtensions.cs
using System;
using System.Collections.Generic;

namespace NappyCat.Extensions
{
    /// <summary>
    /// Collection helpers that avoid allocations and GC pressure.
    /// </summary>
    public static class NcCollectionsExtensions
    {
        public static void NcForEachNoAlloc<T>(this List<T> list, Action<T> action)
        {
            if (list == null || action == null)
                return;

            for (int i = 0; i < list.Count; i++)
                action(list[i]);
        }

        public static void NcSwap<T>(this IList<T> list, int i, int j)
        {
            T tmp = list[i];
            list[i] = list[j];
            list[j] = tmp;
        }

        public static void NcShuffle<T>(this IList<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list.NcSwap(i, UnityEngine.Random.Range(i, list.Count));
            }
        }

        public static T NcGetFirst<T>(this IList<T> list)
            => (list != null && list.Count > 0) ? list[0] : default;

        public static T NcGetLast<T>(this IList<T> list)
            => (list != null && list.Count > 0) ? list[list.Count - 1] : default;

        public static List<T> NcClone<T>(this IList<T> list)
        {
            var clone = new List<T>();
            if (list != null)
                for (int i = 0; i < list.Count; i++) clone.Add(list[i]);
            return clone;
        }

        public static T NcRandomItem<T>(this IList<T> list)
        {
            if (list == null || list.Count == 0) return default;
            int index = UnityEngine.Random.Range(0, list.Count);
            return list[index];
        }

        public static T NcRemoveRandom<T>(this IList<T> list)
        {
            if (list == null || list.Count == 0)
                throw new IndexOutOfRangeException("Cannot remove a random item from an empty list");
            int index = UnityEngine.Random.Range(0, list.Count);
            T item = list[index];
            list.RemoveAt(index);
            return item;
        }
    }
}
