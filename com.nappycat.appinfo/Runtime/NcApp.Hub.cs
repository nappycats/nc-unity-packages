/*
 * NAPPY CAT
 *
 * Copyright © 2025 NAPPY CAT Games
 * http://nappycat.net
 *
 * Author: Stan Nesi
 *
 * File: Packages/com.nappycat.appinfo/Runtime/NcApp.Hub.cs
 */

using UnityEngine;
using NappyCat.AppInfo;

namespace NappyCat
{
    public static partial class Nc
    {
        public static class App
        {
            static  NcAppInfo _info;
            /// <summary>
            /// Access to app-wide information.
            /// </summary>
            public static NcAppInfo Info => _info ? _info : (_info = Resources.Load<NcAppInfo>("NappyCat/NcAppInfo"));
            // public static NcAppInfo Info => Nc.App.Info;
        }
    }
}
