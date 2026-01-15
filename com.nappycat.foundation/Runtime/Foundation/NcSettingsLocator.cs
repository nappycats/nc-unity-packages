/*
 * NAPPY CAT
 *
 * Copyright © 2025 NAPPY CAT Games
 * http://nappycat.net
 *
 * Author: Stan Nesi
 *
 * File: Packages/com.nappycat.foundation/Runtime/Foundation/NcSettingsLocator.cs
 */
using System;
using System.Collections.Generic;
using UnityEngine;

namespace NappyCat.Foundation
{
    /// <summary>Loads and caches NcSettingsBase assets from Resources/NappyCat/Settings at startup.</summary>
    public static class NcSettingsLocator
    {
        static readonly Dictionary<Type, NcSettingsBase> _cache = new();
        const string PATH = "NappyCat/Settings";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        static void Bootstrap() => Rescan();

        public static T Get<T>() where T : NcSettingsBase
        {
            var key = typeof(T);
            if (_cache.TryGetValue(key, out var v)) return (T)v;

            // Try attribute path first if present (supports either full path or folder-only)
            var attr = (NcSettingsPathAttribute)Attribute.GetCustomAttribute(key, typeof(NcSettingsPathAttribute));
            if (attr != null && !string.IsNullOrEmpty(attr.ResourcesPath))
            {
                // 1) Try exact path
                var fromAttr = Resources.Load<T>(attr.ResourcesPath);
                if (fromAttr)
                {
                    _cache[key] = fromAttr;
                    return fromAttr;
                }
                // 2) Try folder + type name
                var comb = CombineResourcePath(attr.ResourcesPath, key.Name);
                fromAttr = Resources.Load<T>(comb);
                if (fromAttr)
                {
                    _cache[key] = fromAttr;
                    return fromAttr;
                }
            }

            // Default location: NappyCat/Settings/TypeName
            var asset = Resources.Load<T>($"{PATH}/{key.Name}");
            if (asset) _cache[key] = asset;
            return asset;
        }

        static string CombineResourcePath(string basePath, string leaf)
        {
            if (string.IsNullOrEmpty(basePath)) return leaf;
            basePath = basePath.Replace('\\','/');
            if (basePath.EndsWith("/")) return basePath + leaf;
            return basePath + "/" + leaf;
        }

        /// <summary>Clear the cache and rescan Resources for settings.</summary>
        public static void Rescan()
        {
            _cache.Clear();
            var all = Resources.LoadAll<NcSettingsBase>(PATH);
            foreach (var s in all)
                if (s) _cache[s.GetType()] = s;
        }

        /// <summary>Clear the cache without scanning. Next Get<T>() will reload on demand.</summary>
        public static void Clear() => _cache.Clear();

        /// <summary>Get a snapshot of the currently cached settings (type → instance).</summary>
        public static System.Collections.Generic.KeyValuePair<System.Type, NcSettingsBase>[] Snapshot()
        {
            var arr = new System.Collections.Generic.KeyValuePair<System.Type, NcSettingsBase>[_cache.Count];
            int i = 0; foreach (var kv in _cache) arr[i++] = kv; return arr;
        }
    }
}
