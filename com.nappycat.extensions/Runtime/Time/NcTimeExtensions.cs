// Copyright (c) 2025 Nappy Cat. All rights reserved.
// Packages/com.nappycat.extensions/Runtime/Time/NcTimeExtensions.cs
using System.Globalization;

namespace NappyCat.Extensions
{
    /// <summary>
    /// Time formatting helpers.
    /// </summary>
    public static class NcTimeExtensions
    {
        public static string NcToHumanSeconds(this float value)
            => string.Format(CultureInfo.InvariantCulture, "{0:0.00}s", value);
    }
}
