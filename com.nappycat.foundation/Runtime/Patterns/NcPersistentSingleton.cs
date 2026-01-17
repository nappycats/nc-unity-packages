/*
 * NAPPY CAT
 *
 * Copyright © 2025 NAPPY CAT Games
 * http://nappycat.net
 *
 * Author: Stan Nesi
 *
 * File: Packages/com.nappycat.foundation/Runtime/Patterns/NcPersistentSingleton.cs
 */
using UnityEngine;

namespace NappyCat.Foundation
{
    /// <summary>Cross-scene persistent singleton (DontDestroyOnLoad).</summary>
    public abstract class NcPersistentSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance) return _instance;
                _instance = Object.FindFirstObjectByType<T>();
                if (_instance) return _instance;
                var go = new GameObject($"[{typeof(T).Name}]");
                _instance = go.AddComponent<T>();
                Object.DontDestroyOnLoad(go);
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance && _instance != this){ Destroy(gameObject); return; }
            _instance = this as T; DontDestroyOnLoad(gameObject);
        }
    }
}

