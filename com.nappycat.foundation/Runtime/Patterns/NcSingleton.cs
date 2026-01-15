/*
 * NAPPY CAT
 *
 * Copyright © 2025 NAPPY CAT Games
 * http://nappycat.net
 *
 * Author: Stan Nesi
 *
 * File: Packages/com.nappycat.foundation/Runtime/Patterns/NcSingleton.cs
 */
using UnityEngine;

namespace NappyCat.Foundation
{
    /// <summary>Scene-lifetime singleton (not persisted). Call Instance to lazy-create.</summary>
    public abstract class NcSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance) return _instance;
                _instance = Object.FindObjectOfType<T>();
                if (_instance) return _instance;
                var go = new GameObject($"[{typeof(T).Name}]");
                _instance = go.AddComponent<T>();
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance && _instance != this){ Destroy(gameObject); return; }
            _instance = this as T;
        }
    }
}

