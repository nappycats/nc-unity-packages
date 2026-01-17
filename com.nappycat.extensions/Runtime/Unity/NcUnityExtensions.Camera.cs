/*
 * NAPPY CAT
 *
 * Copyright © 2025 NAPPY CAT Games
 * http://nappycat.net
 *
 * Author: Stan Nesi
 *
 * File: Packages/com.nappycat.extensions/Runtime/Unity/NcUnityExtensions.Camera.cs
 */
using UnityEngine;

namespace NappyCat.Extensions
{
    public static partial class NcUnityExtensions
    {
        public static float NcWorldSpaceWidth(this Camera camera, float depth = 0f)
        {
            if (camera.orthographic)
            {
                return camera.aspect * camera.orthographicSize * 2f;
            }
            else
            {
                float fovRad = camera.fieldOfView * Mathf.Deg2Rad;
                float height = 2f * depth * Mathf.Tan(fovRad * 0.5f);
                return height * camera.aspect;
            }
        }

        public static float NcWorldSpaceHeight(this Camera camera, float depth = 0f)
        {
            if (camera.orthographic)
            {
                return camera.orthographicSize * 2f;
            }
            else
            {
                float fovRad = camera.fieldOfView * Mathf.Deg2Rad;
                return 2f * depth * Mathf.Tan(fovRad * 0.5f);
            }
        }
    }
}
