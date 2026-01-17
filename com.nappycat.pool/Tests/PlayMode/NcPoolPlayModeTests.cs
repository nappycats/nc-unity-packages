using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace NappyCat.Pool.Tests.PlayMode
{
    public class NcPoolPlayModeTests
    {
        [UnityTest]
        public IEnumerator NcGoPool_ReusesInstances_AndParentsToRoot()
        {
            var prefab = new GameObject("pooled-prefab");
            prefab.SetActive(false);

            var root = new GameObject("pool-root").transform;
            var pool = new NcGoPool(prefab, root, warm: 1);

            var first = pool.Get();
            Assert.IsTrue(first.activeSelf, "Instance should be active after Get.");
            Assert.AreEqual(root, first.transform.parent, "Instance should parent to root.");

            pool.Release(first);
            Assert.IsFalse(first.activeSelf, "Release should deactivate instance.");
            Assert.AreEqual(root, first.transform.parent, "Release should keep root parent.");

            var second = pool.Get();
            Assert.AreSame(first, second, "Pool should reuse the same instance.");

            pool.Release(second);
            pool.Clear();

            Object.Destroy(prefab);
            Object.Destroy(root.gameObject);
            yield break;
        }
    }
}
