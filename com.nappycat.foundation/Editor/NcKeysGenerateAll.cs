/*
 * NAPPY CAT
 *
 * Copyright © 2025 NAPPY CAT Games
 * http://nappycat.net
 *
 * Author: Stan Nesi
 *
 * File: Packages/com.nappycat.foundation/Editor/NcKeysGenerateAll.cs
 */
 
#if UNITY_EDITOR
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace NappyCat.Foundation.Editor
{
    public static class NcKeysGenerateAll
    {
        [MenuItem("Nappy Cat/Nc.Keys/Generate All Keys", false, 0)]
        public static void GenerateAll()
        {
            int ran = 0;
            ran += TryInvoke("NappyCat.Tags.Editor.NcTagKeysGenerator", "Generate");
            ran += TryInvoke("NappyCat.Param.Editor.NcParamKeysGenerator", "Generate");
            ran += TryInvoke("NappyCat.AppInfo.Editor.NcAppConstsGenerator", "Generate");
            if (ran == 0)
                Debug.LogWarning("[NcKeys] No generators found. Are Tags/Params packages installed?");
        }

        static int TryInvoke(string typeName, string method)
        {
            var t = Type.GetType(typeName + ", " + GetAssemblyName(typeName));
            if (t == null) return 0;
            var m = t.GetMethod(method, BindingFlags.Public | BindingFlags.Static);
            if (m == null) return 0;
            m.Invoke(null, null);
            return 1;
        }

        // Heuristic: assembly name is the root namespace segment chain without the final class
        static string GetAssemblyName(string fullType)
        {
            // Examples:
            // NappyCat.Tags.Editor.NcTagKeysGenerator → NappyCat.Tags.Editor
            var lastDot = fullType.LastIndexOf('.');
            return lastDot > 0 ? fullType.Substring(0, lastDot) : fullType;
        }
    }
}
#endif
