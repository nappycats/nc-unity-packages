// ─────────────────────────────────────────────────────────────────────────────
/*
* NAPPY CAT
*
* Copyright © 2025 NAPPY CAT Games
* http://nappycat.net
*
* Author: Stan Nesi
*
* File: Packages/com.nappycat.tags/Runtime/Bridge/TagAddressablesBridge.cs
*/

#if NC_UNITY_ADDRESSABLES
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using NappyCat.Tags;

namespace NappyCat.Bridge.TagsAddressables
{
    /// Convenience helpers to load assets referenced by the Tag index via Addressables.
    /// Assumes your Addressables use GUID-based keys (default when using AssetReferenceGUID).
    public static class TagAddressablesBridge
    {
        /// Convert a tag AssetRef to an Addressables AssetReference using its GUID.
        public static AssetReference ToAssetReference(in NcTagIndex.AssetRef a)
            => new AssetReference(a.Guid);

        /// Begin loading a single asset via Addressables using its GUID; caller should Release.
        public static AsyncOperationHandle<T> LoadAsset<T>(in NcTagIndex.AssetRef a) where T : Object
            => Addressables.LoadAssetAsync<T>(new AssetReference(a.Guid));

        /// Load all matching assets as Addressables handles.
        public static List<AsyncOperationHandle<T>> LoadAll<T>(IEnumerable<NcTagIndex.AssetRef> assets) where T : Object
        {
            var list = new List<AsyncOperationHandle<T>>();
            foreach (var a in assets)
            {
                list.Add(LoadAsset<T>(a));
            }
            return list;
        }

        /// Release a previously loaded handle.
        public static void Release<T>(AsyncOperationHandle<T> handle) where T : Object
        {
            if (handle.IsValid()) Addressables.Release(handle);
        }
    }
}
#endif
