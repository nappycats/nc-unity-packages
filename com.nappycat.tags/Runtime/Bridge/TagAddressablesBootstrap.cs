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
    /// Bootstrap component to load TagIndexSO from Addressables at runtime.
    /// Sets TagQuery.Index to the loaded index, overriding the Resources fallback.
    [AddComponentMenu("Nappy Cat/Bridge/Tag Index (Addressables)")]
    public sealed class TagAddressablesBootstrap : MonoBehaviour
    {
        [Tooltip("Addressables key or label that resolves to TagIndexSO")]
        public string Key = "NC.Tags.Index"; // set a label or address for TagIndex.asset

        AsyncOperationHandle<TagIndexSO> _handle;

        void OnEnable()
        {
            _handle = Addressables.LoadAssetAsync<TagIndexSO>(Key);
            _handle.Completed += op =>
            {
                if (op.Status == AsyncOperationStatus.Succeeded && op.Result)
                    TagQuery.Index = op.Result; // override Resources fallback
            };
        }

        void OnDisable()
        {
            if (_handle.IsValid()) Addressables.Release(_handle);
            if (TagQuery.Index && _handle.IsValid() && TagQuery.Index == _handle.Result)
                TagQuery.Index = null;
        }

        // Optional helper: load by GUID via Addressables (if your entries use GUID addresses)
        public static AsyncOperationHandle<T> LoadByGuid<T>(string guid)
            => Addressables.LoadAssetAsync<T>(guid);
    }
}
#endif
