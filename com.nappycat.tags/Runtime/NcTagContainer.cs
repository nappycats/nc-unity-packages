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
 
// File: Packages/com.nappycat.tags/Runtime/TagContainer.cs
using System.Collections.Generic;
using UnityEngine;


namespace NappyCat.Tags
{
    [CreateAssetMenu(menuName = "Nappy Cat/Tags/Tag Container", fileName = "TagContainer")]
    public class NcTagContainer : ScriptableObject
    {
        public Object Target; // Sprite, AnimationClip, Prefab, ScriptableObject, etc.
        public List<NcTag> Tags = new();

        [Tooltip("Free-form labels for rapid prototyping. Promote to asset-backed tags later.")]
        public List<string> AdHocTags = new();
    }
}
