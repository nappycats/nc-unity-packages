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

// File: Packages/com.nappycat.tags/Editor/TagPickerWindow.cs

#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using NappyCat.Tags;

namespace NappyCat.Tags.Editor
{
    public sealed class NcTagPickerWindow : EditorWindow
    {
        TextField _search;
        ListView _list;
        NcTag[] _allTags;
        List<object> _items; // NcTag or string (ad-hoc)

        [MenuItem("Nappy Cat/Tags/Tag Picker")]
        public static void Open()
        {
            var win = GetWindow<NcTagPickerWindow>(true, "NcTag Picker");
            win.minSize = new Vector2(360, 480);
            win.Show();
        }

        void OnEnable()
        {
            _search = new TextField("Search");
            _search.RegisterValueChangedCallback(_ => Refilter());
            rootVisualElement.Add(_search);


            _list = new ListView();
            _list.selectionType = SelectionType.Multiple;
            _list.makeItem = () => new Label();
            _list.bindItem = (ve, i) =>
            {
            var item = _items[i];
            var label = (Label)ve;
            if (item is NcTag t) label.text = t.DisplayName + " (" + t.Path + ")";
            else label.text = "ad-hoc: " + item.ToString();
            };
            rootVisualElement.Add(_list);

            Reload();
        }

        void Reload()
        {
            var tagGuids = AssetDatabase.FindAssets("t:NcTag");
            var defined = tagGuids
                .Select(g => AssetDatabase.GUIDToAssetPath(g))
                .Select(p => AssetDatabase.LoadAssetAtPath<NcTag>(p))
                .Where(t => t)
                .OrderBy(t => t.Path)
                .Cast<object>();


            var adhoc = new HashSet<string>();
            foreach (var g in AssetDatabase.FindAssets("t:NcTagContainer"))
            {
                var c = AssetDatabase.LoadAssetAtPath<NcTagContainer>(AssetDatabase.GUIDToAssetPath(g));
                if (c?.AdHocTags == null) continue; foreach (var s in c.AdHocTags) if (!string.IsNullOrWhiteSpace(s)) adhoc.Add(s.Trim());
            }
            foreach (var g in AssetDatabase.FindAssets("t:Prefab"))
            {
                var go = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(g));
                var tg = go ? go.GetComponent<NcTaggable>() : null; if (tg?.AdHocTags == null) continue;
                foreach (var s in tg.AdHocTags) if (!string.IsNullOrWhiteSpace(s)) adhoc.Add(s.Trim());
            }

            var adhocItems = adhoc.OrderBy(s => s).Cast<object>();
            _items = defined.Concat(adhocItems).ToList();
            _list.itemsSource = _items; _list.Rebuild();
        }

        void Refilter()
        {
            var q = _search.value?.Trim().ToLowerInvariant() ?? string.Empty;
            if (string.IsNullOrEmpty(q)) { _list.itemsSource = _items; _list.Rebuild(); return; }
            var filtered = _items.Where(it => it is NcTag t
                    ? (t.DisplayName?.ToLowerInvariant().Contains(q) == true || t.Path.ToLowerInvariant().Contains(q))
                    : it.ToString().ToLowerInvariant().Contains(q)).ToList();
            _list.itemsSource = filtered; _list.Rebuild();
            }
    }
}
#endif
