/*
 * NAPPY CAT
 *
 * Copyright © 2025 NAPPY CAT Games
 * http://nappycat.net
 *
 * Author: Stan Nesi
 *
 * File: Packages/com.nappycat.foundation/Samples~/AppSettingsQuickStart/NcAppSettingsBootstrap.cs
 */
using UnityEngine;
using NappyCat.Foundation.Settings;

public sealed class NcAppSettingsBootstrap : MonoBehaviour
{
    void Start()
    {
        var s = NcAppSettings.Instance;
        Debug.Log($"[NcAppSettingsBootstrap] reduceMotion={s.reduceMotion}, defaultLocale={s.defaultLocale}");
        if (s.reduceMotion)
        {
            // Example of applying a global flag
            Application.targetFrameRate = 60;
        }
    }
}

