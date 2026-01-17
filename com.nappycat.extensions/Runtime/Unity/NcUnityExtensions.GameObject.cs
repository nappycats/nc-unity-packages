/*
 * NAPPY CAT
 *
 * Copyright © 2025 NAPPY CAT Games
 * http://nappycat.net
 *
 * Author: Stan Nesi
 *
 * File: Packages/com.nappycat.extensions/Runtime/Unity/NcUnityExtensions.GameObject.cs
 */
using System;
using System.Collections.Generic;
using UnityEngine;

namespace NappyCat.Extensions
{
    public static partial class NcUnityExtensions
    {
        static readonly List<Component> __nc_componentCache = new List<Component>();

        public static Component NcGetComponentNoAlloc(this GameObject go, Type componentType)
        {
            go.GetComponents(componentType, __nc_componentCache);
            Component component = __nc_componentCache.Count > 0 ? __nc_componentCache[0] : null;
            __nc_componentCache.Clear();
            return component;
        }

        public static T NcGetComponentNoAlloc<T>(this GameObject go) where T : Component
        {
            go.GetComponents(typeof(T), __nc_componentCache);
            Component component = __nc_componentCache.Count > 0 ? __nc_componentCache[0] : null;
            __nc_componentCache.Clear();
            return component as T;
        }

        public static T NcGetComponentAroundOrAdd<T>(this GameObject go) where T : Component
        {
            T component = go.GetComponentInChildren<T>(true);
            if (!component) component = go.GetComponentInParent<T>();
            if (!component) component = go.AddComponent<T>();
            return component;
        }

        public static bool NcHasComponent<T>(this GameObject go) where T : Component
            => go.GetComponent<T>() != null;
    }
}
