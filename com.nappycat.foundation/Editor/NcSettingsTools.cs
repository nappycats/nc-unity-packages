/*
 * NAPPY CAT
 *
 * Copyright © 2025 NAPPY CAT Games
 * http://nappycat.net
 *
 * Author: Stan Nesi
 *
 * File: Packages/com.nappycat.foundation/Editor/NcSettingsTools.cs
 */
#if UNITY_EDITOR
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace NappyCat.Foundation.Editor
{
    /// <summary>
    /// Utilities to ensure and manage NcSettings assets under Resources.
    /// </summary>
    public static class NcSettingsTools
    {
        const string ResourcesRoot = "Assets/Resources";

        [MenuItem(NappyCat.Foundation.Editor.NcMenuPaths.SETTINGS + "Ensure Default Settings", priority = 10)]
        public static void EnsureAllSettings()
        {
            int created = 0, skipped = 0;
            var settingsTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(SafeGetTypes)
                .Where(t => t != null && !t.IsAbstract && typeof(NcSettingsBase).IsAssignableFrom(t))
                .Distinct()
                .ToArray();

            foreach (var t in settingsTypes)
            {
                ResolvePathForType(t, out string folderRel, out string assetName);
                string folderFull = Path.Combine(ResourcesRoot, folderRel).Replace('\\','/');
                string assetPath = folderFull.TrimEnd('/') + "/" + assetName + ".asset";

                var existing = AssetDatabase.LoadAssetAtPath(assetPath, t);
                if (existing)
                {
                    skipped++;
                    continue;
                }

                EnsureFolders(folderFull);
                var inst = ScriptableObject.CreateInstance(t);
                AssetDatabase.CreateAsset(inst, assetPath);
                created++;
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            NcSettingsLocator.Rescan();

            EditorUtility.DisplayDialog(
                "NcSettings • Ensure Defaults",
                $"Created: {created}\nExisting: {skipped}",
                "OK");
        }

        [MenuItem(NappyCat.Foundation.Editor.NcMenuPaths.SETTINGS + "Rescan Settings Cache", priority = 11)]
        public static void RescanCache()
        {
            NcSettingsLocator.Rescan();
            EditorUtility.DisplayDialog("NcSettings • Rescan", "Cache refreshed from Resources.", "OK");
        }

        internal static void ResolvePathForType(Type t, out string folderRel, out string assetName)
        {
            assetName = t.Name;
            var attr = (NcSettingsPathAttribute)Attribute.GetCustomAttribute(t, typeof(NcSettingsPathAttribute));
            string path = attr != null && !string.IsNullOrEmpty(attr.ResourcesPath) ? attr.ResourcesPath : $"NappyCat/Settings/{assetName}";
            path = path.Replace('\\','/');
            if (path.EndsWith("/" + assetName))
            {
                folderRel = path.Substring(0, path.Length - assetName.Length - 1);
            }
            else
            {
                folderRel = path;
            }
            if (string.IsNullOrEmpty(folderRel)) folderRel = "NappyCat/Settings";
        }

        internal static void EnsureFolders(string fullPath)
        {
            fullPath = fullPath.Replace('\\','/');
            var parts = fullPath.Split('/');
            string acc = parts[0];
            for (int i = 1; i < parts.Length; i++)
            {
                string next = acc + "/" + parts[i];
                if (!AssetDatabase.IsValidFolder(next))
                    AssetDatabase.CreateFolder(acc, parts[i]);
                acc = next;
            }
        }

        static Type[] SafeGetTypes(Assembly a)
        {
            try { return a.GetTypes(); }
            catch { return Array.Empty<Type>(); }
        }

        /// <summary>Create the settings asset for a specific NcSettingsBase type if missing.</summary>
        public static bool EnsureSettingForType(Type t, out string assetPath)
        {
            assetPath = null;
            if (t == null || !typeof(NcSettingsBase).IsAssignableFrom(t) || t.IsAbstract) return false;
            ResolvePathForType(t, out string folderRel, out string name);
            string folderFull = Path.Combine(ResourcesRoot, folderRel).Replace('\\','/');
            assetPath = folderFull.TrimEnd('/') + "/" + name + ".asset";
            var existing = AssetDatabase.LoadAssetAtPath(assetPath, t);
            if (existing)
                return false;
            EnsureFolders(folderFull);
            var inst = ScriptableObject.CreateInstance(t);
            AssetDatabase.CreateAsset(inst, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            NcSettingsLocator.Rescan();
            return true;
        }
    }
}
#endif
