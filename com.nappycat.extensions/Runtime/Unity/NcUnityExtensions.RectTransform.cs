// Copyright (c) 2025 Nappy Cat. All rights reserved.
// Packages/com.nappycat.extensions/Runtime/Unity/NcUnityExtensions.RectTransform.cs
using System;
using UnityEngine;

namespace NappyCat.Extensions
{
    public static partial class NcUnityExtensions
    {
        public static void NcSetLeft(this RectTransform rt, float left)
            => rt.offsetMin = new Vector2(left, rt.offsetMin.y);

        public static void NcSetRight(this RectTransform rt, float right)
            => rt.offsetMax = new Vector2(-right, rt.offsetMax.y);

        public static void NcSetTop(this RectTransform rt, float top)
            => rt.offsetMax = new Vector2(rt.offsetMax.x, -top);

        public static void NcSetBottom(this RectTransform rt, float bottom)
            => rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);

        /// <summary>
        /// Resizes a RectTransform to match the screen safe area.
        /// Works best for full screen canvases.
        /// </summary>
        public static void NcResizeToSafeArea(this RectTransform rectTransform, Canvas canvas)
        {
            if (rectTransform == null) throw new ArgumentNullException(nameof(rectTransform));
            if (canvas == null) throw new ArgumentNullException(nameof(canvas));

            Rect safeArea = Screen.safeArea;
            Rect canvasRect = canvas.pixelRect;

            Vector2 anchorMin = safeArea.position;
            Vector2 anchorMax = safeArea.position + safeArea.size;

            anchorMin.x /= canvasRect.width;
            anchorMin.y /= canvasRect.height;
            anchorMax.x /= canvasRect.width;
            anchorMax.y /= canvasRect.height;

#if UNITY_EDITOR
            rectTransform.anchorMin = new Vector2(0f, 0.05f);
            rectTransform.anchorMax = new Vector2(1f, 0.95f);
#else
            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
#endif
        }
    }
}

