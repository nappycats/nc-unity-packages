// ─────────────────────────────────────────────────────────────────────────────
/*
* NAPPY CAT
*
* Copyright © 2025 NAPPY CAT Games
* http://nappycat.net
*
* Author: Stan Nesi
*
* File: Packages/com.nappycat.appinfo/Editor/NcAppConstsGenerator.cs
*/

#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

namespace NappyCat.AppInfo.Editor
{
    public static class NcAppConstsGenerator
    {
        [MenuItem("Nappy Cat/App/Generate App Consts (NcKeys.App.g.cs)", false, -11)]
        public static void Generate()
        {
            var info = Resources.Load<NcAppInfo>("NappyCat/NcAppInfo");
            if (!info) { Debug.LogWarning("NcAppInfo not found at Resources/NappyCat/NcAppInfo.asset"); return; }

            var root = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
#if NC_PKG_FOUNDATION
            var outPath = Path.Combine(root, "Packages/com.nappycat.foundation/Runtime/NcKeys/NcKeys.App.g.cs");
#else
            var outPath = Path.Combine(root, "Assets/NappyCat/NcKeys.Generated/NcKeys.App.g.cs");
#endif
            Directory.CreateDirectory(Path.GetDirectoryName(outPath)!);

            
            string S(string s) => s?.Replace("\\", "\\\\").Replace("\"", "\\\"") ?? "";

            var code =
$@"// Auto-generated from NcAppInfo. Do not edit.
/*
 * NAPPY CAT
 *
 * Copyright © 2025 NAPPY CAT Games
 * http://nappycat.net
 *
 * Author: Stan Nesi
 *
 * File: Packages/com.nappycat.foundation/Runtime/NcKeys/NcKeys.App.g.cs
 */
namespace NappyCat
{{
    public static partial class Nc
    {{
        public static partial class Keys
        {{
            public static partial class App
            {{
                public const string GAME_ID = ""{S(info.GameId)}"";
                public const string TITLE   = ""{S(info.Title)}"";
                public const string VER     = ""{S(info.Version)}"";
                public const string COMPANY_NAME = ""{S(info.CompanyName)}"";
                public const string DEFAULT_LANGUAGE = ""{S(info.DefaultLanguage)}"";
                
                public const string GAME_PACKAGE_NAME = ""{S(info.GamePackageName)}"";

                public const string NAPPYCAT_URL         = ""{S(info.NappyCatUrl)}"";
                public const string PRIVACY_POLICY_URL   = ""{S(info.PrivacyPolicyUrl)}"";
                public const string TERMS_OF_SERVICE_URL = ""{S(info.TermsOfServiceUrl)}"";
                public const string PRODUCT_URL          = ""{S(info.ProductUrl)}"";
                public const string PRODUCT_STORE_URL    = ""{S(info.ProductStoreUrl)}"";
            }}
        }}
    }}
}}";
            File.WriteAllText(outPath, code);
            AssetDatabase.Refresh();
            Debug.Log($"[NcAppConsts] Wrote {outPath}");
        }
    }
}
#endif
