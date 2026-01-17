using System.Collections;
using UnityEngine;
using NappyCat.Pool;

namespace NappyCat.Pool.Samples
{
    /// <summary>
    /// Drop this on an empty GameObject and press Space to spawn/recycle pooled cubes.
    /// </summary>
    public sealed class PoolQuickStart : MonoBehaviour
    {
        [SerializeField] GameObject _prefab;
        [SerializeField] int _warm = 8;
        [SerializeField] float _lifetime = 1.5f;

        NcGoPool _pool;

        void Awake()
        {
            if (!_prefab)
            {
                _prefab = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _prefab.name = "PooledCube";
                _prefab.SetActive(false);
            }

            _pool = new NcGoPool(_prefab, root: transform, warm: _warm);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Spawn();
        }

        void Spawn()
        {
            var go = _pool.Get();
            go.transform.SetPositionAndRotation(Random.insideUnitSphere * 3f, Quaternion.identity);
            StartCoroutine(ReturnAfter(go, _lifetime));
        }

        IEnumerator ReturnAfter(GameObject go, float delay)
        {
            yield return new WaitForSeconds(delay);
            if (go)
                _pool.Release(go);
        }

        void OnDestroy()
        {
            _pool?.Clear();
            if (_prefab) Destroy(_prefab);
        }
    }
}
