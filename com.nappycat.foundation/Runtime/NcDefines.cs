/*
 * NAPPY CAT
 *
 * Copyright © 2025 NAPPY CAT Games
 * http://nappycat.net
 *
 * Author: Stan Nesi
 *
 * File: Packages/com.nappycat.foundation/Runtime/NcDefines.cs
 */
namespace NappyCat.Foundation
{
    /// <summary>
    /// Canonical scripting define names used across NappyCat.
    /// These are set via asmdef Version Defines, not global Scripting Define Symbols.
    /// </summary>
    public static class NcDefines
    {
        // Package presence (NappyCat UPMs) — set on assemblies that integrate with them
        public const string NC_PKG_CORE      = "NC_PKG_CORE";       // com.nappycat.foundation
        public const string NC_PKG_TWEEN     = "NC_PKG_TWEEN";      // com.nappycat.tween
        public const string NC_PKG_CUEFX     = "NC_PKG_CUEFX";      // com.nappycat.cuefx
        public const string NC_PKG_UI        = "NC_PKG_UI";         // com.nappycat.ui (legacy define name kept for compatibility)
        public const string NC_PKG_EVENTS    = "NC_PKG_EVENTS";     // com.nappycat.events
        public const string NC_PKG_ADS       = "NC_PKG_ADS";        // com.nappycat.ads
        public const string NC_PKG_VENDORS   = "NC_PKG_VENDORS";    // com.nappycat.vendors

        // Unity packages — mirrors, namespaced to avoid collisions
        public const string NC_UNITY_UGUI         = "NC_UNITY_UGUI";         // com.unity.ugui
        public const string NC_UNITY_TMP          = "NC_UNITY_TMP";          // com.unity.textmeshpro
        public const string NC_UNITY_URP          = "NC_UNITY_URP";          // com.unity.render-pipelines.universal
        public const string NC_UNITY_HDRP         = "NC_UNITY_HDRP";         // com.unity.render-pipelines.high-definition
        public const string NC_UNITY_CINEMACHINE  = "NC_UNITY_CINEMACHINE";  // com.unity.cinemachine
        public const string NC_UNITY_LOCALIZATION = "NC_UNITY_LOCALIZATION"; // com.unity.localization
        public const string NC_UNITY_INPUT        = "NC_UNITY_INPUT";        // com.unity.inputsystem
        public const string NC_UNITY_VFX          = "NC_UNITY_VFX";          // com.unity.visualeffectgraph
        public const string NC_UNITY_ADDRESSABLES = "NC_UNITY_ADDRESSABLES"; // com.unity.addressables

        // Third-party adapter domains (set via Version Defines on adapters)
        public const string NC_ACH_STEAM        = "NC_ACH_STEAM";
        public const string NC_ACH_GPGS         = "NC_ACH_GPGS";
        public const string NC_ACH_GAMECENTER   = "NC_ACH_GAMECENTER";
        public const string NC_ACH_XBOX         = "NC_ACH_XBOX";
        public const string NC_ACH_EOS          = "NC_ACH_EOS";

        // In-App Purchases
        public const string NC_IAP_UNITY        = "NC_IAP_UNITY";
        public const string NC_IAP_STEAM        = "NC_IAP_STEAM";
        public const string NC_IAP_AMAZON       = "NC_IAP_AMAZON";
        public const string NC_IAP_EPIC         = "NC_IAP_EPIC";

        // Ads
        public const string NC_ADS_UNITY        = "NC_ADS_UNITY";
        public const string NC_ADS_ADMOB        = "NC_ADS_ADMOB";
        public const string NC_ADS_MAX          = "NC_ADS_MAX";
        public const string NC_ADS_LEVELPLAY    = "NC_ADS_LEVELPLAY";
        public const string NC_ADS_FAN          = "NC_ADS_FAN";

        // Save Games
        public const string NC_SAVE_STEAMCLOUD  = "NC_SAVE_STEAMCLOUD";
        public const string NC_SAVE_GPGS        = "NC_SAVE_GPGS";
        public const string NC_SAVE_ICLOUD      = "NC_SAVE_ICLOUD";
        public const string NC_SAVE_PLAYFAB     = "NC_SAVE_PLAYFAB";
        public const string NC_SAVE_FIREBASE    = "NC_SAVE_FIREBASE";

        // Leaderboards
        public const string NC_LB_STEAM         = "NC_LB_STEAM";
        public const string NC_LB_GPGS          = "NC_LB_GPGS";
        public const string NC_LB_GAMECENTER    = "NC_LB_GAMECENTER";
        public const string NC_LB_PLAYFAB       = "NC_LB_PLAYFAB";
        public const string NC_LB_UGS           = "NC_LB_UGS";

        // Analytics
        public const string NC_ANALYTICS_UGS    = "NC_ANALYTICS_UGS";
        public const string NC_ANALYTICS_FIREBASE= "NC_ANALYTICS_FIREBASE";
        public const string NC_ANALYTICS_APPSFLYER= "NC_ANALYTICS_APPSFLYER";
        public const string NC_ANALYTICS_GA4    = "NC_ANALYTICS_GA4";

        // Authentication
        public const string NC_AUTH_GOOGLE      = "NC_AUTH_GOOGLE";
        public const string NC_AUTH_APPLE       = "NC_AUTH_APPLE";
        public const string NC_AUTH_STEAM       = "NC_AUTH_STEAM";
        public const string NC_AUTH_EOS         = "NC_AUTH_EOS";
        public const string NC_AUTH_FACEBOOK    = "NC_AUTH_FACEBOOK";
    }
}
