// Packages/com.nappycat.pool/Runtime/NcPool.cs
using System;
using System.Collections.Generic;

namespace NappyCat.Pool
{
    public sealed class NcPool<T> where T : class
    {
        readonly Stack<T> _stack; readonly Func<T> _factory; readonly Action<T> _reset;
        public int CountInactive => _stack.Count;
        public NcPool(Func<T> factory, Action<T> reset = null, int initialCapacity = 0)
        { _factory = factory; _reset = reset; _stack = new Stack<T>(initialCapacity); for (int i=0;i<initialCapacity;i++) _stack.Push(_factory()); }
        public T Get() => _stack.Count>0 ? _stack.Pop() : _factory();
        public void Release(T item){ _reset?.Invoke(item); _stack.Push(item); }
        public void Clear()=>_stack.Clear();
    }
}

