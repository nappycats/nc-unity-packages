/*
 * NAPPY CAT
 *
 * Copyright © 2025 NAPPY CAT Games
 * http://nappycat.net
 *
 * Author: Stan Nesi
 *
 * File: Packages/com.nappycat.extensions/Runtime/Text/NcStringExtensions.cs
 */
namespace NappyCat.Extensions
{
    /// <summary>
    /// String helpers with allocation-free checks.
    /// </summary>
    public static class NcStringExtensions
    {
        public static bool NcIsNullOrWhiteSpace(this string value)
        {
            if (value == null)
                return true;

            for (int i = 0; i < value.Length; i++)
            {
                if (!char.IsWhiteSpace(value[i]))
                    return false;
            }

            return true;
        }
    }
}