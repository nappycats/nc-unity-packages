// Packages/com.nappycat.pool/Runtime/NcPool.Hub.cs
using System;
using NappyCat.Pool;

namespace NappyCat
{
    /// <summary>
    /// Nc.Pool slot on the global hub.
    /// </summary>
    public static partial class Nc
    {
        public static class Pool
        {
            public static NcPool<T> Create<T>(Func<T> factory, Action<T> reset = null, int initialCapacity = 0)
                where T : class
                => new NcPool<T>(factory, reset, initialCapacity);
        }
    }
}
