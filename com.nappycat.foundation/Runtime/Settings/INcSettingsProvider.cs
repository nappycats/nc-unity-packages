/*
 * NAPPY CAT
 *
 * Copyright © 2025 NAPPY CAT Games
 * http://nappycat.net
 *
 * Author: Stan Nesi
 *
 * File: Packages/com.nappycat.foundation/Runtime/Settings/INcSettingsProvider.cs
 */
using UnityEngine;

namespace NappyCat.Foundation.Settings
{
    /// Implement on ScriptableObjects that provide app-wide settings.
    public interface INcSettingsProvider
    {
        string Id { get; }
        int Order { get; }
        ScriptableObject GetSettings();
    }
}

