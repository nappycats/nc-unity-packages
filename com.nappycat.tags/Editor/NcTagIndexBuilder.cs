/*
 * NAPPY CAT
 *
 * Copyright © 2025 NAPPY CAT Games
 * http://nappycat.net
 *
 * author: stan nesi
 *
 * com.nappycat.tags / Editor
 */

#if UNITY_EDITOR
using UnityEditor;  
using UnityEngine;
using System.Linq;
using NappyCat.Tags;
using System.Collections.Generic;

namespace NappyCat.Tags.Editor
{
    public static class NcTagIndexBuilder
    {
        [MenuItem("Nappy Cat/Tags/Rebuild Tag Index")]
        public static void Rebuild()
        {
            var index = CreateOrLoadIndex();

            var tagToAssets = new Dictionary<NcTag, HashSet<AssetRef>>();
            var strToAssets = new Dictionary<string, HashSet<AssetRef>>(System.StringComparer.Ordinal);

            // 1) TagContainer assets
            var containerGuids = AssetDatabase.FindAssets("t:NcTagContainer");
            foreach (var g in containerGuids)
            {
                var path = AssetDatabase.GUIDToAssetPath(g);
                var container = AssetDatabase.LoadAssetAtPath<NcTagContainer>(path);
                if (!container || !container.Target) continue;

                var targetPath = AssetDatabase.GetAssetPath(container.Target);
                var targetGuid = AssetDatabase.AssetPathToGUID(targetPath);
                var aref = new AssetRef { Guid = targetGuid, Path = targetPath, TypeName = container.Target.GetType().FullName };

                if (container.Tags != null)
                    foreach (var t in container.Tags.Where(t => t))
                    {
                        HashSet<AssetRef> set;
                        if (!tagToAssets.TryGetValue(t, out set))
                        {
                            set = new HashSet<AssetRef>();
                            tagToAssets[t] = set;
                        }
                        set.Add(aref);
                    }

                // Ad-hoc strings
                if (container.AdHocTags != null)
                foreach (var s in container.AdHocTags)
                {
                    var key = (s ?? string.Empty).Trim(); if (key.Length == 0) continue;
                    HashSet<AssetRef> setStr;
                    if (!strToAssets.TryGetValue(key, out setStr))
                    {
                        setStr = new HashSet<AssetRef>();
                        strToAssets[key] = setStr;
                    }
                    setStr.Add(aref);
                }
            }
            // 2) Prefabs with Taggable
            foreach (var g in AssetDatabase.FindAssets("t:Prefab"))
            {
                var path = AssetDatabase.GUIDToAssetPath(g);
                var go = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                if (!go) continue;
                var taggable = go.GetComponent<NcTaggable>();
                if (!taggable) continue;


                var aref = new AssetRef { Guid = g, Path = path, TypeName = typeof(GameObject).FullName };

                if (taggable.Tags != null)
                    foreach (var t in taggable.Tags.Where(t => t))
                    {
                        HashSet<AssetRef> set;
                        if (!tagToAssets.TryGetValue(t, out set))
                        {
                            set = new HashSet<AssetRef>();
                            tagToAssets[t] = set;
                        }
                        set.Add(aref);
                    }


                if (taggable.AdHocTags != null)
                    foreach (var s in taggable.AdHocTags)
                    {
                        var key = (s ?? string.Empty).Trim(); if (key.Length == 0) continue;
                        HashSet<AssetRef> setStr;
                        if (!strToAssets.TryGetValue(key, out setStr))
                        {
                            setStr = new HashSet<AssetRef>();
                            strToAssets[key] = setStr;
                        }
                        setStr.Add(aref);
                    }
            }

            // Write into TagIndexSO
            var entries = tagToAssets
            .Select(kv => new NcTagIndex.Entry { Tag = kv.Key, Assets = kv.Value.ToList() })
            .OrderBy(e => e.Tag.Path)
            .ToList();

            var sEntries = strToAssets
            .Select(kv => new NcTagIndex.StringEntry { Tag = kv.Key, Assets = kv.Value.ToList() })
            .OrderBy(e => e.Tag)
            .ToList();


            var so = new SerializedObject(index);
            var entriesProp = so.FindProperty("_entries");
            entriesProp.arraySize = entries.Count;
            for (int i = 0; i < entries.Count; i++)
            {
                var el = entriesProp.GetArrayElementAtIndex(i);
                el.FindPropertyRelative("Tag").objectReferenceValue = entries[i].Tag;
                var listProp = el.FindPropertyRelative("Assets");
                listProp.arraySize = entries[i].Assets.Count;
                for (int j = 0; j < entries[i].Assets.Count; j++)
                {
                    var a = listProp.GetArrayElementAtIndex(j);
                    a.FindPropertyRelative("Guid").stringValue = entries[i].Assets[j].Guid;
                    a.FindPropertyRelative("Path").stringValue = entries[i].Assets[j].Path;
                    a.FindPropertyRelative("TypeName").stringValue = entries[i].Assets[j].TypeName;
                }
            }


            var sProp = so.FindProperty("_stringEntries");
            sProp.arraySize = sEntries.Count;
            for (int i = 0; i < sEntries.Count; i++)
            {
                var el = sProp.GetArrayElementAtIndex(i);
                el.FindPropertyRelative("Tag").stringValue = sEntries[i].Tag;
                var listProp = el.FindPropertyRelative("Assets");
                listProp.arraySize = sEntries[i].Assets.Count;
                for (int j = 0; j < sEntries[i].Assets.Count; j++)
                {
                var a = listProp.GetArrayElementAtIndex(j);
                a.FindPropertyRelative("Guid").stringValue = sEntries[i].Assets[j].Guid;
                a.FindPropertyRelative("Path").stringValue = sEntries[i].Assets[j].Path;
                a.FindPropertyRelative("TypeName").stringValue = sEntries[i].Assets[j].TypeName;
            }
            }


            so.ApplyModifiedProperties();
            EditorUtility.SetDirty(index);
            AssetDatabase.SaveAssets();


            Debug.Log($"[NcTagIndex] Rebuilt with {entries.Count} asset-backed tags and {sEntries.Count} ad-hoc tags.");
        }


        static NcTagIndex CreateOrLoadIndex()
        {
            const string path = "Assets/Resources/Nappy Cat/NcTagIndex.asset";
            var index = AssetDatabase.LoadAssetAtPath<NcTagIndex>(path);
            if (!index)
            {
                System.IO.Directory.CreateDirectory("Assets/Resources/Nappy Cat");
                index = ScriptableObject.CreateInstance<NcTagIndex>();
                AssetDatabase.CreateAsset(index, path);
            }
            return index;
        }
    }
}
#endif
