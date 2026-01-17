/*
 * NAPPY CAT
 *
 * Copyright © 2025 NAPPY CAT Games
 * http://nappycat.net
 *
 * author: stan nesi
 *
 * com.nappycat.tags / Runtime
 */

// File: Packages/com.nappycat.tags/Runtime/NcTagIndex.cs
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NappyCat.Tags
{
    [Serializable]
    public struct AssetRef
    {
        public string Guid;     // baked from AssetDatabase at build time
        public string Path;     // for debugging
        public string TypeName; // e.g., "UnityEngine.Sprite"
    }

    public class NcTagIndex : ScriptableObject
    {
        [Serializable]
        public struct Entry
        {
            public NcTag Tag;
            public List<AssetRef> Assets;
        }

        [Serializable]
        public struct StringEntry
        {
            public string Tag;           // ad-hoc string tag
            public List<AssetRef> Assets;
        }

        [SerializeField] List<Entry> _entries = new();           // asset-backed tags
        [SerializeField] List<StringEntry> _stringEntries = new(); // ad-hoc string tags

        Dictionary<NcTag, List<AssetRef>> _map;
        Dictionary<string, List<AssetRef>> _smap;

        Dictionary<NcTag, List<AssetRef>> Map => _map ??= BuildMap();
        Dictionary<string, List<AssetRef>> SMap => _smap ??= BuildStringMap();

        Dictionary<NcTag, List<AssetRef>> BuildMap()
        {
            var d = new Dictionary<NcTag, List<AssetRef>>();
            foreach (var e in _entries)
                if (e.Tag) d[e.Tag] = e.Assets ?? new List<AssetRef>();
            return d;
        }

        Dictionary<string, List<AssetRef>> BuildStringMap()
        {
            var d = new Dictionary<string, List<AssetRef>>(StringComparer.Ordinal);
            foreach (var e in _stringEntries)
            {
                var key = (e.Tag ?? string.Empty).Trim();
                if (key.Length == 0) continue;
                d[key] = e.Assets ?? new List<AssetRef>();
            }
            return d;
        }

        // ---------- Queries (typed NcTag) ----------
        public IReadOnlyList<AssetRef> GetAssets(NcTag tag)
            => Map.TryGetValue(tag, out var list) ? list : (IReadOnlyList<AssetRef>)Array.Empty<AssetRef>();

        public IEnumerable<AssetRef> QueryAny(params NcTag[] tags)
        {
            var seen = new HashSet<string>();
            foreach (var t in tags ?? Array.Empty<NcTag>())
            {
                if (!t) continue;
                if (Map.TryGetValue(t, out var list))
                    foreach (var a in list)
                        if (seen.Add(a.Guid)) yield return a;
            }
        }
        public IEnumerable<AssetRef> QueryAll(params NcTag[] tags)
        {
            var valid = tags?.Where(t => t).ToArray();
                if (valid == null || valid.Length == 0) yield break;


            var sets = valid
                .Select(t => Map.TryGetValue(t, out var l) ? l.Select(a => a.Guid) : Array.Empty<string>())
                .Select(gs => new HashSet<string>(gs))
                .ToList();
            if (sets.Count == 0) yield break;


            var inter = sets[0];
            for (int i = 1; i < sets.Count; i++) inter.IntersectWith(sets[i]);


            foreach (var g in inter)
            {
                // Return first matching ref from any tag list
                var src = Map[valid[0]];
                var hit = src.FirstOrDefault(a => a.Guid == g);
                if (!string.IsNullOrEmpty(hit.Guid)) yield return hit;
            }
        }

        // ---------- Queries (ad-hoc string tag paths) ----------
        public IReadOnlyList<AssetRef> GetAssets(string tag)
            => SMap.TryGetValue(tag?.Trim() ?? string.Empty, out var list) ? list : (IReadOnlyList<AssetRef>)Array.Empty<AssetRef>();

        public IEnumerable<AssetRef> QueryAnyStrings(params string[] tags)
        {
            var seen = new HashSet<string>();
            foreach (var t in tags ?? Array.Empty<string>())
            {
                var key = t?.Trim(); if (string.IsNullOrEmpty(key)) continue;
                if (SMap.TryGetValue(key, out var list))
                    foreach (var a in list)
                        if (seen.Add(a.Guid)) yield return a;
            }
        }

        public IEnumerable<AssetRef> QueryAllStrings(params string[] tags)
        {
            var valid = (tags ?? Array.Empty<string>()).Select(s => s?.Trim()).Where(s => !string.IsNullOrEmpty(s)).ToArray();
            if (valid.Length == 0) yield break;

            var sets = valid
                .Select(t => SMap.TryGetValue(t, out var l) ? l.Select(a => a.Guid) : Array.Empty<string>())
                .Select(gs => new HashSet<string>(gs))
                .ToList();
            if (sets.Count == 0) yield break;


            var inter = sets[0];
            for (int i = 1; i < sets.Count; i++) inter.IntersectWith(sets[i]);


            foreach (var g in inter)
            {
                // Return ref from any one list that matches GUID
                var srcKey = valid[0];
                var srcList = SMap.TryGetValue(srcKey, out var list) ? list : null;
                var hit = srcList?.FirstOrDefault(a => a.Guid == g) ?? default;
                if (!string.IsNullOrEmpty(hit.Guid)) yield return hit;
            }
        }

        void OnValidate()
        {
            _map = null;
            _smap = null;
        }
    }
}
