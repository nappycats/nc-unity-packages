#if UNITY_EDITOR
using System;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace NappyCat.Foundation.Editor
{
    internal static class NcCreateMenuUtil
    {
        internal const int ORDER_LOCALE    = 100;
        internal const int ORDER_AUDIO     = 200;
        internal const int ORDER_UI        = 300;
        internal const int ORDER_SHAPES    = 400;
        internal const int ORDER_DEBUG     = 500;
        internal const int ORDER_INPUT     = 600;
        internal const int ORDER_INVENTORY = 650;
        internal const int ORDER_ACHIEVEMENTS = 660;
        internal const int ORDER_STATS     = 665;
        internal const int ORDER_SAVE      = 700;
        internal const int ORDER_INSPECTOR = 800;

        internal const string ROOT = "Assets/Create/Nappy Cat/";

        internal static Type FindType(string fullName)
        {
            var t = Type.GetType(fullName, throwOnError: false);
            if (t != null) return t;

            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    t = asm.GetType(fullName, false);
                    if (t != null) return t;
                }
                catch { }
            }

            return null;
        }

        internal static bool HasType(string fullName) => FindType(fullName) != null;

        internal static string GetSelectedFolderOr(string fallbackFolder)
        {
            var path = "Assets";
            foreach (var obj in Selection.GetFiltered<UnityEngine.Object>(SelectionMode.Assets))
            {
                path = AssetDatabase.GetAssetPath(obj);
                if (File.Exists(path))
                    path = Path.GetDirectoryName(path);
                break;
            }

            if (string.IsNullOrEmpty(path) || !path.StartsWith("Assets", StringComparison.Ordinal))
                path = fallbackFolder;

            return EnsureFolder(path);
        }

        internal static string EnsureFolder(string folder)
        {
            if (string.IsNullOrEmpty(folder))
                folder = "Assets";

            var parts = folder.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var built = parts[0];
            for (int i = 1; i < parts.Length; i++)
            {
                var next = $"{built}/{parts[i]}";
                if (!AssetDatabase.IsValidFolder(next))
                    AssetDatabase.CreateFolder(built, parts[i]);
                built = next;
            }

            return built;
        }

        internal static UnityEngine.Object CreateAssetByTypeName(string typeFullName, string defaultName, string domainFolder)
        {
            var t = FindType(typeFullName);
            if (t == null)
            {
                Debug.LogWarning($"[Nappy Cat] Type not found: {typeFullName}. Is the package installed?");
                return null;
            }

            var baseFolder = EnsureFolder($"Assets/NappyCat/{domainFolder}");
            var targetFolder = GetSelectedFolderOr(baseFolder);
            var path = AssetDatabase.GenerateUniqueAssetPath($"{targetFolder}/{defaultName}.asset");

            var asset = ScriptableObject.CreateInstance(t);
            AssetDatabase.CreateAsset(asset, path);

            TrySeedDefaults(asset);

            AssetDatabase.SaveAssets();
            EditorGUIUtility.PingObject(asset);
            Selection.activeObject = asset;
            return asset;
        }

        static void TrySeedDefaults(UnityEngine.Object asset)
        {
            try
            {
                var t = asset.GetType();
                switch (t.FullName)
                {
                    case "NappyCat.Locale.UI.FontProfile":
                        SetIfExists(asset, "rtl", false);
                        break;
                    case "NappyCat.Pawdio.PawdioBank":
                        AddStringIfListExists(asset, "Buses", "Master");
                        AddStringIfListExists(asset, "Buses", "Music");
                        AddStringIfListExists(asset, "Buses", "SFX");
                        break;
                }
            }
            catch { }
        }

        static void SetIfExists(object target, string fieldOrProp, object value)
        {
            var t = target.GetType();
            var f = t.GetField(fieldOrProp, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (f != null && (value == null || f.FieldType.IsInstanceOfType(value)))
            {
                f.SetValue(target, value);
                return;
            }

            var p = t.GetProperty(fieldOrProp, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (p != null && p.CanWrite && (value == null || p.PropertyType.IsInstanceOfType(value)))
            {
                p.SetValue(target, value, null);
            }
        }

        static void AddStringIfListExists(object target, string listName, string item)
        {
            var t = target.GetType();
            var f = t.GetField(listName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var list = f?.GetValue(target) as System.Collections.IList;
            list?.Add(item);
        }
    }
}
#endif
