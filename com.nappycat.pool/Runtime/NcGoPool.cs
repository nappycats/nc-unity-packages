// Packages/com.nappycat.pool/Runtime/NcGoPool.cs
using System.Collections.Generic;
using UnityEngine;

namespace NappyCat.Pool
{
    /// <summary>Simple GameObject pool per prefab.</summary>
    public sealed class NcGoPool
    {
        readonly Transform _root; readonly GameObject _prefab; readonly Stack<GameObject> _stack = new();
        public NcGoPool(GameObject prefab, Transform root = null, int warm=0)
        { _prefab = prefab; _root = root; for (int i=0;i<warm;i++){ var go=Object.Instantiate(_prefab,_root); go.SetActive(false); _stack.Push(go);} }
        public GameObject Get(Transform parent=null)
        { var go = _stack.Count>0? _stack.Pop() : Object.Instantiate(_prefab, parent?parent:_root); if (parent) go.transform.SetParent(parent,false); go.SetActive(true); return go; }
        public void Release(GameObject go){ go.SetActive(false); go.transform.SetParent(_root,false); _stack.Push(go); }
        public void Clear(){ while(_stack.Count>0) Object.Destroy(_stack.Pop()); }
    }
}

