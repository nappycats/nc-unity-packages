// Copyright (c) 2025 Nappy Cat. All rights reserved.
// Packages/com.nappycat.extensions/Runtime/Unity/NcUnityExtensions.Renderer.cs
using UnityEngine;

namespace NappyCat.Extensions
{
    public static partial class NcUnityExtensions
    {
        public static bool NcIsVisibleFrom(this Renderer renderer, Camera camera)
        {
            var planes = GeometryUtility.CalculateFrustumPlanes(camera);
            return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
        }
    }
}

