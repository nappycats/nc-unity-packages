/*
* NAPPY CAT
*
* Copyright © 2025 Nappy Cat Games
* http://nappycat.net
*
* Author: Stan Nesi
*
* File: Packages/com.NappyCat.Tween/Editor/ApiGuard/NcTweenContractGuard.cs
* Created: 2024-06-19
*/

#if UNITY_EDITOR
using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace NappyCat.Tween
{
    /// <summary>
    /// Reflection-based API guard to ensure symbols stay intact.
    /// </summary>
    [InitializeOnLoad]
    public static class NcTweenContractGuard
    {
        static NcTweenContractGuard()
        {
            Verify();
        }

        [MenuItem("Nappy Cat/Tween/Validate Contract", priority = 10)]
        public static void ValidateMenu()
        {
            Verify();
            EditorUtility.DisplayDialog("NcTween Contract", "Validation completed. See console for any issues.", "Close");
        }

        static void Require(bool condition, string message)
        {
            if (!condition)
            {
                Debug.LogError($"[NcTween ApiGuard] {message}");
            }
        }

        static void Verify()
        {
            var assembly = typeof(NcTween).Assembly;

            // Enums
            Require(assembly.GetType("NappyCat.Tween.NcClock")?.IsEnum == true, "Enum NcClock missing.");
            Require(assembly.GetType("NappyCat.Tween.NcLoop")?.IsEnum == true, "Enum NcLoop missing.");

            // Handle structure
            var handleType = assembly.GetType("NappyCat.Tween.TweenHandle");
            Require(handleType != null, "TweenHandle missing.");
            if (handleType != null)
            {
                Require(handleType.GetField("Id") != null || handleType.GetProperty("Id") != null, "TweenHandle.Id missing.");
                Require(handleType.GetProperty("IsValid") != null, "TweenHandle.IsValid property missing.");
            }

            // Options & events
            Require(assembly.GetType("NappyCat.Tween.NcTweenOptions") != null, "NcTweenOptions missing.");
            Require(assembly.GetType("NappyCat.Tween.NcTweenEvents") != null, "NcTweenEvents missing.");

            // NcTween surface
            var tweenType = typeof(NcTween);
            Require(tweenType.GetMethod("ManualTick", BindingFlags.Public | BindingFlags.Static) != null, "NcTween.ManualTick missing.");
            Require(tweenType.GetMethod("EnsureRunner", BindingFlags.Public | BindingFlags.Static) != null, "NcTween.EnsureRunner missing.");

            // Ensure To() overloads exist for each supported type
            var toMethods = tweenType.GetMethods(BindingFlags.Public | BindingFlags.Static).Where(m => m.Name == "To").ToArray();
            Require(toMethods.Length >= 6, "NcTween.To overloads missing.");
        }
    }
}
#endif
