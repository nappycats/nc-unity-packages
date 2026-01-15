// ─────────────────────────────────────────────────────────────────────────────
/*
* NAPPY CAT
*
* Copyright © 2025 NAPPY CAT Games
* http://nappycat.net
*
* Author: Stan Nesi
*
* File: Packages/com.nappycat.foundation/Runtime/NcGlobalConfig.cs
*/

using UnityEngine;

namespace NappyCat.Core
{
    [CreateAssetMenu(menuName = "Nappy Cat/Global Config", fileName = "NcGlobalConfig")]
    public sealed class NcGlobalConfig : ScriptableObject
    {
        public bool ShowDebugHud = false;
        public bool UseAddressables = false; // opt-in project flag
        public float MasterVolume = 1f;
    }
}