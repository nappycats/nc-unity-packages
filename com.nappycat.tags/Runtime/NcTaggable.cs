/*
 * NAPPY CAT
 *
 * Copyright © 2025 NAPPY CAT Games
 * http://nappycat.net
 *
 * author: stan nesi
 *
 * com.nappycat.tags / Runtime
 * File: Packages/com.nappycat.tags/Runtime/Taggable.cs
 */
 
using System.Collections.Generic;
using UnityEngine;

namespace NappyCat.Tags
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Nappy Cat/Tags/NcTaggable")]
    public class NcTaggable : MonoBehaviour
    {
        public List<NcTag> Tags = new();
        [Tooltip("Free-form labels for rapid prototyping. Promote to asset-backed tags later.")]
        public List<string> AdHocTags = new();
    }

    [System.Obsolete("Use NcTaggable instead.", false)]
    public class Taggable : NcTaggable { }
}
// file renamed to NcTaggable.cs
