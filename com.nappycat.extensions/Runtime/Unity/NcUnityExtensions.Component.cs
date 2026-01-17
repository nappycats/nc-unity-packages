// Copyright (c) 2025 Nappy Cat. All rights reserved.
// Packages/com.nappycat.extensions/Runtime/Unity/NcUnityExtensions.Component.cs
using UnityEngine;

namespace NappyCat.Extensions
{
    public static partial class NcUnityExtensions
    {
        public static bool NcTryGetComponent<T>(this Component component, out T result) where T : Component
        {
            result = component.GetComponent<T>();
            return result != null;
        }
    }
}
