#if UNITY_EDITOR
/*
 * NAPPY CAT
 *
 * Copyright © 2025 NAPPY CAT Games
 * http://nappycat.net
 *
 * Author: Stan Nesi
 *
 * File: Packages/com.nappycat.foundation/Editor/Foundation/NcSettingsCreator.cs
 */
using UnityEditor; using UnityEngine;

namespace NappyCat.Foundation.Editor
{
    public static class NcSettingsCreator
    {
        [MenuItem(NcMenuPaths.SETTINGS + "Create Settings Folder")]
        static void CreateFolder()
        {
            var path = "Assets/Resources/NappyCat/Settings";
            if (!AssetDatabase.IsValidFolder("Assets/Resources")) AssetDatabase.CreateFolder("Assets","Resources");
            if (!AssetDatabase.IsValidFolder("Assets/Resources/NappyCat")) AssetDatabase.CreateFolder("Assets/Resources","NappyCat");
            if (!AssetDatabase.IsValidFolder(path)) AssetDatabase.CreateFolder("Assets/Resources/Nappy Cat","Settings");
            EditorUtility.DisplayDialog("Nappy Cat","Created Resources/NappyCat/Settings.","OK");
        }

        [MenuItem(NcMenuPaths.SETTINGS + "Create App Settings Asset")]
        static void CreateAppSettings()
        {
            CreateFolder();
            var asset = NcScriptableEditorUtil.EnsureSettingsAsset<NappyCat.Foundation.Settings.NcAppSettings>("NappyCat/Settings/NcAppSettings");
            EditorGUIUtility.PingObject(asset);
            Selection.activeObject = asset;
        }
    }
}
#endif
