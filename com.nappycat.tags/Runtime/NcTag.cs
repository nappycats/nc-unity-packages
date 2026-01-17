/*
 * NAPPY CAT
 *
 * Copyright © 2025 NAPPY CAT Games
 * http://nappycat.net
 *
 * author: stan nesi
 *
 * com.nappycat.tags / Runtime
 * File: Packages/com.nappycat.tags/Runtime/NcTag.cs
 */
    
using System;
using System.Collections.Generic;
using UnityEngine;

namespace NappyCat.Tags
{
    [CreateAssetMenu(menuName = "Nappy Cat/Tags/Tag", fileName = "Tag")]
    public class NcTag : ScriptableObject
    {
        [SerializeField] string _path = ""; // e.g., "species/dragon/fire"
        [SerializeField] string _displayName = ""; // UI label
        [SerializeField] string[] _synonyms = Array.Empty<string>();
        [SerializeField] Color _color = Color.white;
        [SerializeField] Texture2D _icon;
        [SerializeField] NcTag _parent; // optional hierarchy


        public string Path => _path;
        public string DisplayName => string.IsNullOrWhiteSpace(_displayName) ? _path : _displayName;
        public IReadOnlyList<string> Synonyms => _synonyms ?? Array.Empty<string>();
        public Color Color => _color;
        public Texture2D Icon => _icon;
        public NcTag Parent => _parent;
    }
}
