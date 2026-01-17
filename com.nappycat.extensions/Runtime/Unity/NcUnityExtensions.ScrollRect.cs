/*
 * NAPPY CAT
 *
 * Copyright © 2025 NAPPY CAT Games
 * http://nappycat.net
 *
 * Author: Stan Nesi
 *
 * File: Packages/com.nappycat.extensions/Runtime/Unity/NcUnityExtensions.ScrollRect.cs
 */
#if NC_UNITY_UGUI
using UnityEngine;
using UnityEngine.UI;

namespace NappyCat.Extensions
{
    public static partial class NcUnityExtensions
    {
        public static void NcScrollToTop(this ScrollRect scrollRect)
            => scrollRect.normalizedPosition = new Vector2(0, 1);

        public static void NcScrollToBottom(this ScrollRect scrollRect)
            => scrollRect.normalizedPosition = new Vector2(0, 0);
    }
}
#endif