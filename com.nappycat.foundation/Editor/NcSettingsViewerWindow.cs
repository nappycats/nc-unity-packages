#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NappyCat.Foundation.Editor
{
    /// <summary>
    /// Minimal viewer for NcSettingsLocator cache with quick actions.
    /// </summary>
    public sealed class NcSettingsViewerWindow : EditorWindow
    {
        Vector2 _scroll;
        bool _showOnlyMissing;

        [MenuItem("Nappy Cat/Settings/Settings Viewer")] 
        static void Open() => GetWindow<NcSettingsViewerWindow>(true, "NcSettings • Viewer");

        void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Rescan", GUILayout.Width(100))) NcSettingsLocator.Rescan();
            if (GUILayout.Button("Clear", GUILayout.Width(100))) NcSettingsLocator.Clear();
            _showOnlyMissing = GUILayout.Toggle(_showOnlyMissing, "Show Only Missing", GUILayout.Width(150));
            if (GUILayout.Button("Validate Duplicates", GUILayout.Width(160))) ValidateDuplicates();
            if (GUILayout.Button("Open Resources Folder", GUILayout.Width(180))) OpenResourcesFolder();
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(4);
            _scroll = EditorGUILayout.BeginScrollView(_scroll);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(SafeGetTypes)
                .Where(t => t != null && !t.IsAbstract && typeof(NcSettingsBase).IsAssignableFrom(t))
                .OrderBy(t => t.FullName)
                .ToArray();

            if (types.Length == 0)
                EditorGUILayout.HelpBox("No NcSettingsBase types found.", MessageType.Info);

            foreach (var t in types)
            {
                NcSettingsTools.ResolvePathForType(t, out string folderRel, out string assetName);
                string folderFull = Path.Combine("Assets/Resources", folderRel).Replace('\\','/');
                string assetPath = folderFull.TrimEnd('/') + "/" + assetName + ".asset";
                var existing = AssetDatabase.LoadAssetAtPath(assetPath, t) as NcSettingsBase;
                // Find duplicates anywhere under Resources (not just canonical path)
                var duplicates = FindAllSettingsAssetsOfType(t);
                int duplicateCount = duplicates.Length;
                bool hasDup = duplicateCount > 1;
                if (_showOnlyMissing && existing) continue;

                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField(t.FullName, EditorStyles.boldLabel);
                EditorGUILayout.LabelField("Resources Path", folderRel + "/" + assetName);
                // Auto-apply status badge if the settings type exposes an 'applyOnStartup' bool
                if (existing)
                {
                    var auto = TryGetAutoApply(existing, out bool on);
                    if (auto)
                    {
                        var color = on ? new Color(0.2f, 0.8f, 0.3f) : new Color(0.5f, 0.5f, 0.5f);
                        EditorGUILayout.BeginHorizontal();
                        DrawDot(color);
                        EditorGUILayout.LabelField(on ? "Auto Apply: On" : "Auto Apply: Off", GUILayout.Width(120));
                        GUILayout.FlexibleSpace();
                        EditorGUILayout.EndHorizontal();
                    }
                }
                using (new EditorGUI.DisabledScope(true))
                    EditorGUILayout.ObjectField("Asset", existing, typeof(NcSettingsBase), false);
                EditorGUILayout.BeginHorizontal();
                if (existing)
                {
                    if (GUILayout.Button("Ping")) EditorGUIUtility.PingObject(existing);
                    if (GUILayout.Button("Apply Now"))
                    {
                        if (!TryApplySetting(existing))
                            EditorUtility.DisplayDialog("Apply Now", "This setting does not expose a supported Apply method.", "OK");
                    }
                    if (hasDup)
                    {
                        if (GUILayout.Button($"Select {duplicateCount} Duplicates"))
                        {
                            Selection.objects = duplicates.Cast<UnityEngine.Object>().ToArray();
                        }
                    }
                }
                else
                {
                    if (GUILayout.Button("Create"))
                    {
                        NcSettingsTools.EnsureSettingForType(t, out _);
                    }
                }
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
                if (hasDup)
                {
                    EditorGUILayout.HelpBox($"{duplicateCount} settings assets found in Resources for {t.Name}. Keep only one to avoid ambiguity.", MessageType.Warning);
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndScrollView();
        }

        static Type[] SafeGetTypes(Assembly a)
        {
            try { return a.GetTypes(); }
            catch { return Array.Empty<Type>(); }
        }

        static NcSettingsBase[] FindAllSettingsAssetsOfType(Type t)
        {
            var guids = AssetDatabase.FindAssets($"t:{t.Name}");
            var list = new System.Collections.Generic.List<NcSettingsBase>();
            foreach (var g in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(g);
                if (!path.Contains("/Resources/")) continue;
                var obj = AssetDatabase.LoadAssetAtPath(path, t) as NcSettingsBase;
                if (obj) list.Add(obj);
            }
            return list.ToArray();
        }

        static bool TryApplySetting(NcSettingsBase setting)
        {
            if (!setting) return false;
            var t = setting.GetType();
            // Try common patterns: Apply(), ApplyNow()
            var m = t.GetMethod("Apply", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null)
                 ?? t.GetMethod("ApplyNow", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
            if (m != null) { m.Invoke(setting, null); return true; }

            // Try ApplyTo(service)
            var methods = t.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var mi in methods)
            {
                if (mi.Name != "ApplyTo") continue;
                var ps = mi.GetParameters();
                if (ps.Length != 1) continue;
                var svcType = ps[0].ParameterType;
                object svc = ResolveServiceInstance(svcType);
                if (svc != null)
                {
                    mi.Invoke(setting, new object[] { svc });
                    return true;
                }
            }
            return false;
        }

        static object ResolveServiceInstance(Type svcType)
        {
            // Look for static property Current or Instance
            var prop = svcType.GetProperty("Current", BindingFlags.Public | BindingFlags.Static)
                   ?? svcType.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static);
            if (prop != null)
            {
                try { return prop.GetValue(null); } catch { }
            }
            // Try parameterless ctor
            try { return Activator.CreateInstance(svcType); } catch { }
            return null;
        }

        void ValidateDuplicates()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(SafeGetTypes)
                .Where(t => t != null && !t.IsAbstract && typeof(NcSettingsBase).IsAssignableFrom(t))
                .OrderBy(t => t.FullName)
                .ToArray();
            var sb = new System.Text.StringBuilder();
            int totalDupTypes = 0, totalDupAssets = 0;
            foreach (var t in types)
            {
                var dups = FindAllSettingsAssetsOfType(t);
                if (dups.Length > 1)
                {
                    totalDupTypes++;
                    totalDupAssets += dups.Length;
                    sb.AppendLine($"{t.FullName}: {dups.Length} assets");
                    foreach (var d in dups)
                        sb.AppendLine("  - " + AssetDatabase.GetAssetPath(d));
                }
            }
            if (totalDupTypes == 0)
            {
                EditorUtility.DisplayDialog("Validate Duplicates", "No duplicate settings assets found under Resources.", "OK");
                return;
            }
            EditorUtility.DisplayDialog("Validate Duplicates", $"Duplicate types: {totalDupTypes}\nTotal duplicate assets: {totalDupAssets}\n\n" + sb.ToString(), "OK");
        }

        void OpenResourcesFolder()
        {
            const string root = "Assets/Resources";
            if (!AssetDatabase.IsValidFolder(root))
            {
                Directory.CreateDirectory(root);
                AssetDatabase.Refresh();
            }
            EditorUtility.RevealInFinder(root);
        }

        static bool TryGetAutoApply(NcSettingsBase setting, out bool value)
        {
            value = false;
            if (!setting) return false;
            var t = setting.GetType();
            var f = t.GetField("applyOnStartup", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (f != null && f.FieldType == typeof(bool)) { value = (bool)f.GetValue(setting); return true; }
            var p = t.GetProperty("applyOnStartup", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (p != null && p.PropertyType == typeof(bool)) { value = (bool)(p.GetValue(setting)); return true; }
            return false;
        }

        static void DrawDot(Color color)
        {
            var rect = GUILayoutUtility.GetRect(10, 10, GUILayout.Width(10), GUILayout.Height(10));
            rect.y += 4;
            rect.width = rect.height = 8;
            EditorGUI.DrawRect(rect, color);
        }
    }
}
#endif
