// Packages/com.nappycat.pool/Runtime/NcObjectPool.cs
using System.Collections.Generic;

namespace NappyCat.Pool
{
    /// <summary>
    /// Policy interface to customize pooled object lifecycle.
    /// </summary>
    public interface INcPoolPolicy<T>
    {
        T Create();
        void OnRent(T item);
        void OnReturn(T item);
        void Destroy(T item);
    }

    /// <summary>
    /// Policy-driven object pool.
    /// Complements <see cref="NcPool{T}"/>factory-based API.
    /// </summary>
    public sealed class NcObjectPool<T>
    {
        readonly Stack<T> _stack = new();
        readonly INcPoolPolicy<T> _policy;

        public int CountInactive => _stack.Count;

        public NcObjectPool(INcPoolPolicy<T> policy, int warm = 0)
        {
            _policy = policy;
            for (int i = 0; i < warm; i++)
                _stack.Push(_policy.Create());
        }

        public T Rent()
        {
            var item = _stack.Count > 0 ? _stack.Pop() : _policy.Create();
            _policy.OnRent(item);
            return item;
        }

        public void Return(T item)
        {
            _policy.OnReturn(item);
            _stack.Push(item);
        }

        public void Clear()
        {
            while (_stack.Count > 0)
                _policy.Destroy(_stack.Pop());
        }
    }
}
