// ─────────────────────────────────────────────────────────────────────────────
/*
* NAPPY CAT
*
* Copyright © 2025 NAPPY CAT Games
* http://nappycat.net
*
* Author: Stan Nesi
*
* File: Packages/com.nappycat.appinfo/Editor/NcAppInfoSettings.cs
*/

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace NappyCat.AppInfo.Editor
{
    public static class NcAppInfoSettings
    {
        const string SettingsPath = "Project/Nappy Cat/App Info";

        [SettingsProvider]
        public static SettingsProvider CreateProvider() => new SettingsProvider(SettingsPath, SettingsScope.Project)
        {
            label = "App Info",
            activateHandler = (_, root) =>
            {
                root.Clear();
                root.style.flexGrow = 1;
                root.style.paddingLeft = 10;
                root.style.paddingRight = 10;
                root.style.paddingTop = 5;
                root.style.paddingBottom = 10;

                var header = new Label("App Info");
                header.style.unityFontStyleAndWeight = FontStyle.Bold;
                header.style.fontSize = 18;
                root.Add(header);

                var asset = LoadOrCreate();
                var so = new SerializedObject(asset);

                var scroll = new ScrollView();
                scroll.style.flexGrow = 1;
                root.Add(scroll);

                // Identity
                scroll.Add(new PropertyField(so.FindProperty("GameId"), "Game ID"));
                scroll.Add(new PropertyField(so.FindProperty("Title"), "Title"));
                scroll.Add(new PropertyField(so.FindProperty("Version"), "Version"));
                scroll.Add(new PropertyField(so.FindProperty("CompanyName"), "Company Name"));
                scroll.Add(new PropertyField(so.FindProperty("DefaultLanguage"), "Default Language"));

                // Packages
                // var pk = new Label("Packages");
                // pk.style.unityFontStyleAndWeight = FontStyle.Bold; scroll.Add(pk);
                // scroll.Add(new PropertyField(so.FindProperty("NappyCatToolsPackage"), "Nappy Cat Tools Package"));
                scroll.Add(new PropertyField(so.FindProperty("GamePackageName"), "Game Package Name"));

                // URLs
                // var urls = new Label("URLs");
                // urls.style.unityFontStyleAndWeight = FontStyle.Bold; scroll.Add(urls);
                scroll.Add(new PropertyField(so.FindProperty("NappyCatUrl"), "Nappy Cat URL"));
                scroll.Add(new PropertyField(so.FindProperty("PrivacyPolicyPath"), "Privacy Policy Path"));
                scroll.Add(new PropertyField(so.FindProperty("TermsOfServicePath"), "Terms of Service Path"));
                scroll.Add(new PropertyField(so.FindProperty("ProductUrl"), "Product URL"));
                scroll.Add(new PropertyField(so.FindProperty("ProductStoreUrl"), "Product Store URL"));

                scroll.Bind(so);
            },
            guiHandler = _ =>
            {
                var asset = LoadOrCreate();
                var ed = UnityEditor.Editor.CreateEditor(asset);
                if (ed != null)
                {
                    ed.OnInspectorGUI();
                    UnityEditor.Editor.DestroyImmediate(ed);
                }
            }
        };
        static NcAppInfo LoadOrCreate()
        {
            const string assetPath = "Assets/Resources/NappyCat/NcAppInfo.asset";
            const string folder = "Assets/Resources/NappyCat";
            if (!System.IO.Directory.Exists(folder))
                System.IO.Directory.CreateDirectory(folder);
            var a = AssetDatabase.LoadAssetAtPath<NcAppInfo>(assetPath);
            if (!a)
            {
                a = ScriptableObject.CreateInstance<NcAppInfo>();
                AssetDatabase.CreateAsset(a, assetPath);
            }
            return a;
        }
    }
}
#endif
