/*
 * NAPPY CAT
 *
 * Copyright © 2025 NAPPY CAT Games
 * http://nappycat.net
 *
 * Author: Stan Nesi
 *
 * File: Packages/com.nappycat.foundation/Runtime/Foundation/NcSettings.cs
 */
using UnityEngine;

namespace NappyCat.Foundation
{
    /// <summary>
    /// Generic global settings pattern backed by a ScriptableObject.
    /// Prefers NcSettingsLocator; if none is found, tries NcSettingsPathAttribute;
    /// if still not found, creates an ephemeral instance (not saved).
    /// </summary>
    public abstract class NcSettings<T> : NcSettingsBase where T : NcSettings<T>
    {
        static T _instance;
        static string _attrPath;

        public static T Instance
        {
            get
            {
                if (_instance) return _instance;

                // Prefer Locator cache / default convention
                _instance = NcSettingsLocator.Get<T>();
                if (_instance) return _instance;

                // Attribute path (cached)
                if (_attrPath == null)
                {
                    var attr = (NcSettingsPathAttribute)System.Attribute.GetCustomAttribute(typeof(T), typeof(NcSettingsPathAttribute));
                    _attrPath = attr != null ? attr.ResourcesPath : "NappyCat/Settings/" + typeof(T).Name;
                }

                // Try exact attribute path
                _instance = Resources.Load<T>(_attrPath);
                if (_instance) return _instance;

                // If attribute was a folder, try folder + type name
                if (!_attrPath.EndsWith(typeof(T).Name))
                {
                    string comb = _attrPath.TrimEnd('/') + "/" + typeof(T).Name;
                    _instance = Resources.Load<T>(comb);
                    if (_instance) return _instance;
                }

                // Fallback: ephemeral
                _instance = ScriptableObject.CreateInstance<T>();
                _instance.hideFlags = HideFlags.DontSaveInBuild | HideFlags.DontUnloadUnusedAsset;
                return _instance;
            }
        }
    }
}

