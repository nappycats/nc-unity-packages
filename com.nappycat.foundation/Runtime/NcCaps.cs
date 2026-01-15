/*
 * NAPPY CAT
 *
 * Copyright © 2025 NAPPY CAT Games
 * http://nappycat.net
 *
 * Author: Stan Nesi
 *
 * File: Packages/com.nappycat.foundation/Runtime/NcCaps.cs
 */
namespace NappyCat.Foundation
{
    /// <summary>
    /// Compile-time capability map for adapters. Values baked at compile time from defines.
    /// </summary>
    public static class NcCaps
    {
        public const bool Ach_Steam =
#if NC_ACH_STEAM
            true;
#else
            false;
#endif
        public const bool Ach_Gpgs =
#if NC_ACH_GPGS
            true;
#else
            false;
#endif
        public const bool Ach_GameCenter =
#if NC_ACH_GAMECENTER
            true;
#else
            false;
#endif

        public const bool Iap_Unity =
#if NC_IAP_UNITY
            true;
#else
            false;
#endif
        public const bool Iap_Steam =
#if NC_IAP_STEAM
            true;
#else
            false;
#endif
        public const bool Iap_Amazon =
#if NC_IAP_AMAZON
            true;
#else
            false;
#endif

        public const bool Ads_Admob =
#if NC_ADS_ADMOB
            true;
#else
            false;
#endif
        public const bool Ads_Max =
#if NC_ADS_MAX
            true;
#else
            false;
#endif
        public const bool Ads_LevelPlay =
#if NC_ADS_LEVELPLAY
            true;
#else
            false;
#endif
        public const bool Ads_Fan =
#if NC_ADS_FAN
            true;
#else
            false;
#endif

        public const bool Save_SteamCloud =
#if NC_SAVE_STEAMCLOUD
            true;
#else
            false;
#endif
        public const bool Save_Gpgs =
#if NC_SAVE_GPGS
            true;
#else
            false;
#endif
        public const bool Save_ICloud =
#if NC_SAVE_ICLOUD
            true;
#else
            false;
#endif
        public const bool Save_Playfab =
#if NC_SAVE_PLAYFAB
            true;
#else
            false;
#endif

        public const bool Lb_Steam =
#if NC_LB_STEAM
            true;
#else
            false;
#endif
        public const bool Lb_Gpgs =
#if NC_LB_GPGS
            true;
#else
            false;
#endif
        public const bool Lb_GameCenter =
#if NC_LB_GAMECENTER
            true;
#else
            false;
#endif

        public const bool Analytics_Ugs =
#if NC_ANALYTICS_UGS
            true;
#else
            false;
#endif
        public const bool Analytics_Firebase =
#if NC_ANALYTICS_FIREBASE
            true;
#else
            false;
#endif
        public const bool Analytics_AppsFlyer =
#if NC_ANALYTICS_APPSFLYER
            true;
#else
            false;
#endif
        public const bool Analytics_Ga4 =
#if NC_ANALYTICS_GA4
            true;
#else
            false;
#endif

        public const bool Auth_Google =
#if NC_AUTH_GOOGLE
            true;
#else
            false;
#endif
        public const bool Auth_Apple =
#if NC_AUTH_APPLE
            true;
#else
            false;
#endif
        public const bool Auth_Steam =
#if NC_AUTH_STEAM
            true;
#else
            false;
#endif
        public const bool Auth_Eos =
#if NC_AUTH_EOS
            true;
#else
            false;
#endif
        public const bool Auth_Facebook =
#if NC_AUTH_FACEBOOK
            true;
#else
            false;
#endif
    }
}
