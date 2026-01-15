/*
 * NAPPY CAT
 *
 * Copyright © 2025 NAPPY CAT Games
 * http://nappycat.net
 *
 * Author: Stan Nesi
 *
 * File: Packages/com.nappycat.foundation/Runtime/Foundation/NcSettingsPathAttribute.cs
 */
using System;

namespace NappyCat.Foundation
{
    /// <summary>
    /// Attribute to specify the Resources path for a NcSettings asset.
    /// Example: [NcSettingsPath("NappyCat/Settings/MySettings")]
    /// Supports either a full path (folder/name) or a folder path (we'll append the type name).
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class NcSettingsPathAttribute : Attribute
    {
        public readonly string ResourcesPath;
        public NcSettingsPathAttribute(string resourcesPath){ ResourcesPath = resourcesPath; }
    }
}

