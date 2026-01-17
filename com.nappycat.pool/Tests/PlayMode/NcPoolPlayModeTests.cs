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

        [UnityTest]
        public IEnumerator NcGoPool_InvokesPooledBehaviourHooks()
        {
            var prefab = new GameObject("pooled-prefab");
            var tracker = prefab.AddComponent<PooledTracker>();
            prefab.SetActive(false);

            var pool = new NcGoPool(prefab, warm: 0);
            var inst = pool.Get();
            var instTracker = inst.GetComponent<PooledTracker>();

            Assert.AreEqual(1, instTracker.AcquireCount);
            Assert.AreEqual(0, instTracker.ReleaseCount);

            pool.Release(inst);
            Assert.AreEqual(1, instTracker.ReleaseCount);

            pool.Get();
            Assert.AreEqual(2, instTracker.AcquireCount);

            pool.Release(inst);
            pool.Clear();
            Object.Destroy(prefab);
            yield break;
        }

        sealed class PooledTracker : NcPooledBehaviour
        {
            public int AcquireCount;
            public int ReleaseCount;

            public override void OnPoolAcquire() => AcquireCount++;
            public override void OnPoolRelease() => ReleaseCount++;
        }
    }
}
