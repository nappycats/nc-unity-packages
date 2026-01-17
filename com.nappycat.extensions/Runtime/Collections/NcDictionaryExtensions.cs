// Copyright (c) 2025 Nappy Cat. All rights reserved.
// Packages/com.nappycat.extensions/Runtime/Collections/NcDictionaryExtensions.cs
using System;
using System.Collections.Generic;

namespace NappyCat.Extensions
{
    public static class NcDictionaryExtensions
    {
        public static TKey NcKeyByValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TValue value)
        {
            foreach (var pair in dictionary)
                if (Equals(pair.Value, value)) return pair.Key;
            return default;
        }

        public static bool NcTryRemove<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, out TValue value)
        {
            if (dictionary.TryGetValue(key, out value))
            {
                dictionary.Remove(key);
                return true;
            }
            return false;
        }

        public static TValue NcAddOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value, Func<TKey, TValue, TValue> update)
        {
            if (dictionary.TryGetValue(key, out var old)) value = update(key, old);
            dictionary[key] = value;
            return value;
        }

        public static TValue NcAddOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> add, Func<TKey, TValue, TValue> update)
        {
            if (dictionary.TryGetValue(key, out var old))
            {
                var value = update(key, old);
                dictionary[key] = value;
                return value;
            }
            else
            {
                var value = add(key);
                dictionary.Add(key, value);
                return value;
            }
        }

        public static bool NcTryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key)) return false;
            dictionary.Add(key, value);
            return true;
        }
    }
}

