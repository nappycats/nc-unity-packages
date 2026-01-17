/*
 * NAPPY CAT
 *
 * Copyright © 2025 NAPPY CAT Games
 * http://nappycat.net
 *
 * Author: Stan Nesi
 *
 * File: Packages/com.nappycat.extensions/Runtime/Unity/NcUnityExtensions.Rect.cs
 */
using UnityEngine;

namespace NappyCat.Extensions
{
    public static partial class NcUnityExtensions
    {
        public static bool NcIntersects(this Rect a, Rect b)
        {
            return !((a.x > b.xMax) || (a.xMax < b.x) || (a.y > b.yMax) || (a.yMax < b.y));
        }
    }
}
