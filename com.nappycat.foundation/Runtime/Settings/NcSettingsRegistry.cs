/*
 * NAPPY CAT
 *
 * Copyright © 2025 NAPPY CAT Games
 * http://nappycat.net
 *
 * Author: Stan Nesi
 *
 * File: Packages/com.nappycat.foundation/Runtime/Settings/NcSettingsRegistry.cs
 */
using System;
using System.Collections.Generic;
using UnityEngine;

namespace NappyCat.Foundation.Settings
{
    /// Lightweight registry to discover and access settings ScriptableObjects.
    public static class NcSettingsRegistry
    {
        static readonly SortedList<int, ScriptableObject> s_Settings = new();
        static bool s_Scanned;

        public static void Register(ScriptableObject so, int order = 0)
        {
            if (!so) return;
            while (s_Settings.ContainsKey(order)) order++;
            s_Settings.Add(order, so);
        }

        public static T Get<T>() where T : ScriptableObject
        {
            EnsureScanned();
            foreach (var kv in s_Settings)
                if (kv.Value is T t) return t;
            return null;
        }

        public static IReadOnlyList<ScriptableObject> All()
        {
            EnsureScanned();
            return new List<ScriptableObject>(s_Settings.Values);
        }

        static void EnsureScanned()
        {
            if (s_Scanned) return;
            s_Scanned = true;
            // Convention: resources under "NappyCat/Settings" are discovered automatically.
            var found = Resources.LoadAll<ScriptableObject>("NappyCat/Settings");
            for (int i = 0; i < found.Length; i++)
            {
                var so = found[i];
                if (so is INcSettingsProvider p)
                {
                    Register(p.GetSettings() ? p.GetSettings() : so, p.Order);
                }
                else Register(so, 0);
            }
        }
    }
}
