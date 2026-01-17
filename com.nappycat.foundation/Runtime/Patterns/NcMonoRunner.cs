
/*
 * NAPPY CAT
 *
 * Copyright © 2025 NAPPY CAT Games
 * http://nappycat.net
 *
 * Author: Stan Nesi
 *
 * File: Packages/com.nappycat.foundation/Runtime/Patterns/NcMonoRunner.cs
 */
using System.Collections;
using UnityEngine;

namespace NappyCat.Foundation
{
    [AddComponentMenu("Nappy Cat/Runners/NcMono Runner")]
    /// <summary>
    /// Shared lightweight runner for packages that need a
    /// coroutine host but don't want hidden objects per package.
    /// </summary>
    public sealed class NcMonoRunner : MonoBehaviour
    {
        static NcMonoRunner _instance;

        /// <summary>Ensure a runner exists;
        /// creates a hidden DontDestroyOnLoad GO if needed.</summary>
        public static NcMonoRunner Ensure(string name = "[NcRunner]")
        {
            if (_instance) return _instance;
            _instance = Object.FindFirstObjectByType<NcMonoRunner>();
            if (_instance) return _instance;

            var go = new GameObject(name);
            go.hideFlags = HideFlags.HideAndDontSave;
            DontDestroyOnLoad(go);
            _instance = go.AddComponent<NcMonoRunner>();
            return _instance;
        }

        public Coroutine StartSafeCoroutine(IEnumerator routine)
        {
            if (routine == null) return null;
            return StartCoroutine(routine);
        }
    }
}
