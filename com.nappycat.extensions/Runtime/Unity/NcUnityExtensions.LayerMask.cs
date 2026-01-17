// Copyright (c) 2025 Nappy Cat. All rights reserved.
// Packages/com.nappycat.extensions/Runtime/Unity/NcUnityExtensions.LayerMask.cs
using UnityEngine;

namespace NappyCat.Extensions
{
    public static partial class NcUnityExtensions
    {
        public static bool NcContains(this LayerMask mask, int layer)
            => (mask.value & (1 << layer)) != 0;

        public static bool NcContains(this LayerMask mask, GameObject go)
            => (mask.value & (1 << go.layer)) != 0;
    }
}

