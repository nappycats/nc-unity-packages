// Copyright (c) 2025 Nappy Cat. All rights reserved.
// Packages/com.nappycat.extensions/Runtime/Unity/NcUnityExtensions.Rect.cs
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

