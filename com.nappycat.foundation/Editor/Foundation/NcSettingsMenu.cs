/*
 * NAPPY CAT
 *
 * Copyright © 2025 NAPPY CAT Games
 * http://nappycat.net
 *
 * Author: Stan Nesi
 *
 * File: Packages/com.nappycat.foundation/Editor/Foundation/NcSettingsMenu.cs
 */
#if UNITY_EDITOR
using UnityEditor;

namespace NappyCat.Foundation.Editor
{
    /// <summary>
    /// Nappy Cat/Settings menu shortcuts into core settings providers.
    /// </summary>
    internal static class NcSettingsMenu
    {
        static bool HasType(string fullName)
        {
            return NcCreateMenuUtil.FindType(fullName) != null;
        }

        static void ShowMissingPackageDialog(string domain, string packageId)
        {
            EditorUtility.DisplayDialog(
                $"Nappy Cat · {domain} Settings",
                $"{domain} settings are provided by the package \"{packageId}\".\n\n" +
                "Install it via Package Manager (Add package by name) to enable this panel.",
                "OK");
        }

        [MenuItem(NcMenuPaths.SETTINGS + "App Info", false, NcMenuOrder.CORE + 0)]
        static void OpenAppInfo()
        {
            if (!HasType("NappyCat.App.Editor.NcAppInfoSettings"))
            {
                ShowMissingPackageDialog("App Info", "com.nappycat.appinfo");
                return;
            }

            SettingsService.OpenProjectSettings("Project/Nappy Cat/App Info");
        }

        [MenuItem(NcMenuPaths.SETTINGS + "Params", false, NcMenuOrder.CORE + 10)]
        static void OpenParams()
        {
            if (!HasType("NappyCat.Param.Editor.NcParamsSettings"))
            {
                ShowMissingPackageDialog("Params", "com.nappycat.param");
                return;
            }

            SettingsService.OpenProjectSettings("Project/Nappy Cat/Params");
        }

        [MenuItem(NcMenuPaths.SETTINGS + "Locale", false, NcMenuOrder.LOCALE + 0)]
        static void OpenLocale()
        {
            if (!HasType("NappyCat.Locale.NcLocaleSettings"))
            {
                ShowMissingPackageDialog("Locale", "com.nappycat.locale");
                return;
            }

            SettingsService.OpenProjectSettings("Project/Nappy Cat/Locale");
        }

        [MenuItem(NcMenuPaths.SETTINGS + "Audio", false, NcMenuOrder.AUDIO + 0)]
        static void OpenAudio()
        {
            if (!HasType("NappyCat.Audio.INcAudio"))
            {
                ShowMissingPackageDialog("Audio", "com.nappycat.audio");
                return;
            }

            SettingsService.OpenProjectSettings("Project/Nappy Cat/Audio");
        }

        [MenuItem(NcMenuPaths.SETTINGS + "UI", false, NcMenuOrder.UI + 0)]
        static void OpenUI()
        {
            if (!HasType("NappyCat.UI.NcUIRuntimeSettings"))
            {
                ShowMissingPackageDialog("UI", "com.nappycat.ui");
                return;
            }

            SettingsService.OpenProjectSettings("Project/Nappy Cat/UI");
        }

        [MenuItem(NcMenuPaths.SETTINGS + "Input", false, NcMenuOrder.INPUT + 0)]
        static void OpenInput()
        {
            if (!HasType("NappyCat.Input.InputSettings"))
            {
                ShowMissingPackageDialog("Input", "com.nappycat.input");
                return;
            }

            SettingsService.OpenProjectSettings("Project/Nappy Cat/Input");
        }
    }
}
#endif
