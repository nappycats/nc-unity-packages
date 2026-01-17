// Copyright (c) 2025 Nappy Cat. All rights reserved.
// Packages/com.nappycat.extensions/Runtime/Text/NcStringExtensions.cs
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
