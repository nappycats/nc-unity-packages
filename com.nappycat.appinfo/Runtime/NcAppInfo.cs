/*
 * NAPPY CAT
 *
 * Copyright © 2025 NAPPY CAT Games
 * http://nappycat.net
 *
 * Author: Stan Nesi
 *
 * File: Packages/com.nappycat.appinfo/Runtime/NcAppInfo.cs
 */
using UnityEngine;

namespace NappyCat.AppInfo
{
    [CreateAssetMenu(menuName = "Nappy Cat/App/App Info", fileName = "NcAppInfo")]
    public sealed class NcAppInfo : ScriptableObject
    {
        [Header("Identity")]
        public string GameId = "NC-GME-XXXX-XXXXXXXXXXXX";
        public string Title = "Your Game";
        public string Version = "1.0";
        public string CompanyName = "NAPPY CAT";
        public string DefaultLanguage = "en";

        [Header("Packages")]
        public string GamePackageName = "com.nappycat.yourgame";

        [Tooltip("List of core Nappy Cat packages this app expects. Used for diagnostics, about screens, or conditional wiring.")]
        public string[] NappyCatCorePackages = new[]
        {
            "com.nappycat.foundation",
            "com.nappycat.tween",
            "com.nappycat.scene",
            "com.nappycat.param",
            "com.nappycat.perf",
            "com.nappycat.pool",
            "com.nappycat.signals",
        };

        [Header("URLs")]
        public string NappyCatUrl = "https://nappycat.net/";
        public string PrivacyPolicyPath = "privacy-policy";
        public string TermsOfServicePath = "terms-of-service";
        public string ProductUrl = "https://example.com/";
        public string ProductStoreUrl = "https://example.com/store";

        [Header("Paths")]
        public string PrivacyPolicyUrl => NappyCatUrl + PrivacyPolicyPath;
        public string TermsOfServiceUrl => NappyCatUrl + TermsOfServicePath;
    }
}
