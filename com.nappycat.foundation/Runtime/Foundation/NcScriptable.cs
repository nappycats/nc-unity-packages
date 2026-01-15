/*
 * NAPPY CAT
 *
 * Copyright © 2025 NAPPY CAT Games
 * http://nappycat.net
 *
 * Author: Stan Nesi
 *
 * File: Packages/com.nappycat.foundation/Runtime/Foundation/NcScriptable.cs
 */
using System;
using UnityEngine;

namespace NappyCat.Foundation
{
    /// <summary>
    /// Lightweight base for NappyCat ScriptableObjects.
    /// - Stable Id (GUID string)
    /// - Version & timestamps (UTC ticks)
    /// - Validation hook (Editor)
    /// - Runtime‑safe Clone()
    /// - Optional read‑only flag for builds
    /// </summary>
    public abstract class NcScriptable : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField, HideInInspector] string _id;
        [SerializeField, HideInInspector] int    _version = 1;
        [SerializeField, HideInInspector] long   _createdUtcTicks;
        [SerializeField, HideInInspector] long   _modifiedUtcTicks;
        [SerializeField] bool _readOnlyInBuild = false;

        // Public API
        public string Id            => _id;
        public string Guid          => _id; // back‑compat alias
        public int    Version       => _version;
        public DateTime CreatedUtc  => new DateTime(_createdUtcTicks == 0 ? DateTime.UtcNow.Ticks : _createdUtcTicks, DateTimeKind.Utc);
        public DateTime ModifiedUtc => new DateTime(_modifiedUtcTicks == 0 ? (_createdUtcTicks == 0 ? DateTime.UtcNow.Ticks : _createdUtcTicks) : _modifiedUtcTicks, DateTimeKind.Utc);
        public bool ReadOnlyInBuild => _readOnlyInBuild;

        // Local change signal (no global dependencies)
        public event Action<NcScriptable> Changed;

        /// <summary>Override to validate fields. Editor-only call path triggers this.</summary>
        protected virtual void Validate() { }

        /// <summary>Raise Changed and bump modified timestamp.</summary>
        protected void NotifyChanged()
        {
            _modifiedUtcTicks = DateTime.UtcNow.Ticks;
            Changed?.Invoke(this);
        }

        /// <summary>Return a runtime clone (does not touch the asset on disk).</summary>
        public T Clone<T>() where T : NcScriptable => Instantiate(this) as T;
        /// <summary>Convenience non‑generic clone.</summary>
        public NcScriptable Clone() => Instantiate(this);

        // --- Unity hooks ---
        void OnEnable()
        {
#if UNITY_EDITOR
            // Assign a GUID if missing (editor only to avoid writing in builds)
            if (string.IsNullOrEmpty(_id))
            {
                _id = UnityEditor.GUID.Generate().ToString();
                if (_createdUtcTicks == 0) _createdUtcTicks = DateTime.UtcNow.Ticks;
                _modifiedUtcTicks = DateTime.UtcNow.Ticks;
                UnityEditor.EditorUtility.SetDirty(this);
            }
#endif
            if (_createdUtcTicks == 0) _createdUtcTicks = DateTime.UtcNow.Ticks;
            if (_modifiedUtcTicks == 0) _modifiedUtcTicks = _createdUtcTicks;
        }

#if UNITY_EDITOR
        // Editor‑only validation path
        protected virtual void OnValidate()
        {
            if (_readOnlyInBuild && Application.isPlaying) return;
            try { Validate(); }
            finally
            {
                _version = Math.Max(1, _version + 1);
                _modifiedUtcTicks = DateTime.UtcNow.Ticks;
                NotifyChanged();
                UnityEditor.EditorUtility.SetDirty(this);
            }
        }
#endif

        // ISerializationCallbackReceiver
        public virtual void OnBeforeSerialize() { }
        public virtual void OnAfterDeserialize() { }
    }
}
