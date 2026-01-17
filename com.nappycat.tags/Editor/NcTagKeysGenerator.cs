/*
 * NAPPY CAT
 *
 * Copyright © 2025 NAPPY CAT Games
 * http://nappycat.net
 *
 * Author: Stan Nesi
 *
 * File: Packages/com.nappycat.tags/Editor/NcTagKeysGenerator.cs
 */
#if UNITY_EDITOR
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace NappyCat.Tags.Editor
{
    public static class NcTagKeysGenerator
    {
        const string OutputRelPath = "Packages/com.nappycat.foundation/Runtime/NcKeys/NcKeys.Tags.g.cs";
        const string FallbackRelPath = "Assets/NappyCat.Generated/NcKeys.Tags.g.cs";

        [MenuItem("Nappy Cat/Nc Keys/Generate Keys (Tags)", false, 10)]
        public static void Generate()
        {
            var guids = AssetDatabase.FindAssets("t:NcTag");
            var tags = guids
                .Select(g => AssetDatabase.GUIDToAssetPath(g))
                .Select(p => AssetDatabase.LoadAssetAtPath<NcTag>(p))
                .Where(t => t != null && !string.IsNullOrEmpty(t.Path))
                .Select(t => t.Path.Trim())
                .Distinct()
                .OrderBy(p => p)
                .ToArray();

            var outPath = OutputRelPath;
#if !NC_PKG_FOUNDATION
            outPath = FallbackRelPath;
#endif

            Directory.CreateDirectory(Path.GetDirectoryName(outPath)!);

            var sb = new StringBuilder(4096);
            sb.AppendLine("/*");
            sb.AppendLine(" * NAPPY CAT");
            sb.AppendLine(" *");
            sb.AppendLine(" * Copyright © 2025 NAPPY CAT Games");
            sb.AppendLine(" * http://nappycat.net");
            sb.AppendLine(" *");
            sb.AppendLine(" * Author: Stan Nesi");
            sb.AppendLine(" *");
            sb.AppendLine(" * File: " + outPath.Replace("\\", "/"));
            sb.AppendLine(" */");
            sb.AppendLine("namespace NappyCat");
            sb.AppendLine("{");
            sb.AppendLine("    public static partial class Nc");
            sb.AppendLine("    {");
            sb.AppendLine("        public static partial class Keys");
            sb.AppendLine("        {");
            sb.AppendLine("            public static partial class Tags");
            sb.AppendLine("            {");
            foreach (var tag in tags)
            {
                var constName = SanitizeConstName(tag).ToUpperInvariant();
                sb.AppendLine($"                public const string {constName} = \"{Escape(tag)}\";");
            }
            sb.AppendLine("            }");
            sb.AppendLine("        }");
            sb.AppendLine("    }");
            sb.AppendLine("}");

            WriteIfChanged(outPath, sb.ToString());
            Debug.Log($"[NcKeys] Generated Tags keys → {outPath} ({tags.Length} entries)");
        }

        static string SanitizeConstName(string raw)
        {
            // species/dragon/fire → species_dragon_fire
            var s = new string(raw.Select(ch => char.IsLetterOrDigit(ch) ? ch : '_').ToArray());
            if (s.Length == 0 || char.IsDigit(s[0])) s = "_" + s;
            return s;
        }

        static string Escape(string s) => s.Replace("\\", "\\\\").Replace("\"", "\\\"");

        static void WriteIfChanged(string path, string content)
        {
            var exists = File.Exists(path);
            if (exists && File.ReadAllText(path) == content) return;
            File.WriteAllText(path, content);
            AssetDatabase.Refresh();
        }
    }
}
#endif
