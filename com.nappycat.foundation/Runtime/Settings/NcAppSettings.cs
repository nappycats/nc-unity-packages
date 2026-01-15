/*
 * NAPPY CAT
 *
 * Copyright © 2025 NAPPY CAT Games
 * http://nappycat.net
 *
 * Author: Stan Nesi
 *
 * File: Packages/com.nappycat.foundation/Runtime/Settings/NcAppSettings.cs
 */
using NappyCat.Foundation;

namespace NappyCat.Foundation.Settings
{
    /// <summary>
    /// Optional global app settings. Create an asset under Resources/NappyCat/Settings/NcAppSettings.
    /// Extend or replace this in your app if you need different defaults.
    /// </summary>
    public sealed class NcAppSettings : NcSettings<NcAppSettings>
    {
        public bool reduceMotion;
        public string defaultLocale = "en";

        protected override void Validate()
        {
            if (string.IsNullOrWhiteSpace(defaultLocale)) defaultLocale = "en";
        }
    }
}

