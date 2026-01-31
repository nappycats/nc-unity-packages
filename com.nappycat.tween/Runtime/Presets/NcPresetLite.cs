/*
* NAPPY CAT
*
* Copyright © 2025 Nappy Cat Games
* http://nappycat.net
*
* Author: Stan Nesi
*
* File: Packages/com.NappyCat.Tween/Runtime/OptionalPresets/NcTweenPlayer.cs
* Created: 2024-06-19
*/

using UnityEngine;

namespace NappyCat.Tween
{
    /// <summary>Minimal preset struct (optional) for simple designer flows without CueFX.</summary>
    [System.Serializable]
    public struct NcPresetLite
    {
        public enum Kind { Move, LocalScale, Rotate, CanvasAlpha, SpriteColor }
        public Kind kind;
        public float duration;
        public float delay;
        public NcEase ease;
        public Vector3 v3;
        public Color color;
    }
}
