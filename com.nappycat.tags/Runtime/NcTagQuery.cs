/*
* NAPPY CAT
*
* Copyright © 2025 NAPPY CAT Games
* http://nappycat.net
*
* Author: Stan Nesi
*
* File: Packages/com.nappycat.tags/Runtime/TagQuery.cs
*/

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace NappyCat.Tags
{
    public static class NcTagQuery
    {
        static NcTagIndex _index;


        // For simplicity, load from Resources. You can switch to Addressables later.
        public static NcTagIndex Index
        {
            get
            {
                if (_index == null)
                    _index = Resources.Load<NcTagIndex>("Nappy Cat/NcTagIndex");
                return _index;
            }
            set => _index = value;
        }


        // Asset-backed tag queries
        public static IEnumerable<AssetRef> AnyOf(params NcTag[] tags) => Index ? Index.QueryAny(tags) : Enumerable.Empty<AssetRef>();
        public static IEnumerable<AssetRef> AllOf(params NcTag[] tags) => Index ? Index.QueryAll(tags) : Enumerable.Empty<AssetRef>();

        // Ad-hoc string tag queries
        public static IEnumerable<AssetRef> AnyOfStrings(params string[] tags) => Index ? Index.QueryAnyStrings(tags) : Enumerable.Empty<AssetRef>();
        public static IEnumerable<AssetRef> AllOfStrings(params string[] tags) => Index ? Index.QueryAllStrings(tags) : Enumerable.Empty<AssetRef>();

        public static IEnumerable<AssetRef> WhereType(this IEnumerable<AssetRef> src, Type t)
            => src.Where(a => a.TypeName == t.FullName);
            
        // Mixed query (union): asset-backed + ad-hoc
        public static IEnumerable<AssetRef> AnyMixed(NcTag[] assetTags, string[] stringTags)
        {
            var a = AnyOf(assetTags ?? Array.Empty<NcTag>());
            var b = AnyOfStrings(stringTags ?? Array.Empty<string>());
            var seen = new HashSet<string>();
            foreach (var r in a) if (seen.Add(r.Guid)) yield return r;
            foreach (var r in b) if (seen.Add(r.Guid)) yield return r;
        }
    }
}
