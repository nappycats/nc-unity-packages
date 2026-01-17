// Copyright (c) 2025 Nappy Cat. All rights reserved.
// Packages/com.nappycat.extensions/Runtime/Collections/NcArrayExtensions.cs
using UnityEngine;

namespace NappyCat.Extensions
{
    public static class NcArrayExtensions
    {
        public static T NcRandomValue<T>(this T[] array)
        {
            if (array == null || array.Length == 0) return default;
            int index = Random.Range(0, array.Length);
            return array[index];
        }

        public static T[] NcShuffle<T>(this T[] array)
        {
            if (array == null || array.Length <= 1) return array;
            for (int i = 0; i < array.Length; i++)
            {
                int j = Random.Range(i, array.Length);
                (array[i], array[j]) = (array[j], array[i]);
            }
            return array;
        }
    }
}

