using NUnit.Framework;
using NappyCat.Pool;

namespace NappyCat.Pool.Tests.EditMode
{
    public class NcPoolEditModeTests
    {
        [Test]
        public void NcPool_ReusesInstances_AndResetsOnRelease()
        {
            int created = 0;
            int reset = 0;

            var pool = new NcPool<Dummy>(
                () => { created++; return new Dummy(); },
                d => { reset++; d.Value = 0; },
                initialCapacity: 2);

            var first = pool.Get();
            first.Value = 5;
            pool.Release(first);

            var second = pool.Get();

            Assert.AreSame(first, second, "Pool should reuse the same instance.");
            Assert.AreEqual(0, second.Value, "Reset should clear state.");
            Assert.AreEqual(2, created, "Warm capacity should create upfront.");
            Assert.AreEqual(1, reset, "Reset should have been invoked once.");
        }

        [Test]
        public void NcObjectPool_InvokesPolicyHooks()
        {
            var policy = new DummyPolicy();
            var pool = new NcObjectPool<Dummy>(policy, warm: 1);

            var item = pool.Rent();
            Assert.AreEqual(1, policy.Created);
            Assert.AreEqual(1, policy.Rented);
            Assert.AreEqual(0, policy.Returned);

            pool.Return(item);
            Assert.AreEqual(1, policy.Returned);
            Assert.AreEqual(1, pool.CountInactive);
        }

        [Test]
        public void NcHub_Create_ProxiesToPool()
        {
            var pool = Nc.Pool.Create(() => new Dummy());
            var a = pool.Get();
            pool.Release(a);
            var b = pool.Get();
            Assert.AreSame(a, b);
        }

        sealed class Dummy
        {
            public int Value;
        }

        sealed class DummyPolicy : INcPoolPolicy<Dummy>
        {
            public int Created;
            public int Rented;
            public int Returned;

            public Dummy Create()
            {
                Created++;
                return new Dummy();
            }

            public void OnRent(Dummy item) => Rented++;
            public void OnReturn(Dummy item) => Returned++;
            public void Destroy(Dummy item) { }
        }
    }
}
