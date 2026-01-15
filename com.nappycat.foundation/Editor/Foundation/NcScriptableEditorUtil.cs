/*
 * NAPPY CAT
 *
 * Copyright © 2025 NAPPY CAT Games
 * http://nappycat.net
 *
 * Author: Stan Nesi
 *
 * File: Packages/com.nappycat.foundation/Editor/Foundation/NcScriptableEditorUtil.cs
 */
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace NappyCat.Foundation.Editor
{
    public static class NcScriptableEditorUtil
    {
        public static void MarkDirty(Object asset)
        {
            if (!asset) return;
            EditorUtility.SetDirty(asset);
        }

        /// <summary>Create (or ensure) a settings asset at a Resources path.
        /// Accepts a full path without extension, e.g. "NappyCat/Settings/MySettings".</summary>
        public static T EnsureSettingsAsset<T>(string resourcesPath) where T : ScriptableObject
        {
            var obj = Resources.Load<T>(resourcesPath);
            if (obj) return obj;

            string dir = System.IO.Path.Combine("Assets/Resources", resourcesPath);
            string folder = System.IO.Path.GetDirectoryName(dir).Replace('\\','/');
            string name = System.IO.Path.GetFileName(dir);

            EnsureFolder(folder);

            var inst = ScriptableObject.CreateInstance<T>();
            string assetPath = $"{folder}/{name}.asset";
            AssetDatabase.CreateAsset(inst, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return AssetDatabase.LoadAssetAtPath<T>(assetPath);
        }

        static void EnsureFolder(string folder)
        {
            if (AssetDatabase.IsValidFolder(folder)) return;
            var parts = folder.Split('/');
            string walk = parts[0];
            for (int i = 1; i < parts.Length; i++)
            {
                string next = walk + "/" + parts[i];
                if (!AssetDatabase.IsValidFolder(next))
                    AssetDatabase.CreateFolder(walk, parts[i]);
                walk = next;
            }
        }
    }
}
#endif

