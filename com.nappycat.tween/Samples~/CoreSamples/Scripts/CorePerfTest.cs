using UnityEngine;
using System.Diagnostics;
using NappyCat.Tween;

namespace NappyCat.Tween.Samples
{
    public sealed class CorePerfTest : MonoBehaviour
    {
        public int count = 1000;
        public float duration = 1f;

        void Start()
        {
            NcTween.EnsureRunner();
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < count; i++)
            {
                transform.NcMoveTo(transform.position + Vector3.right * 0.01f, duration);
            }
            sw.Stop();
            UnityEngine.Debug.Log($"Scheduled {count} tweens in {sw.ElapsedMilliseconds} ms. ActiveCount={NcTween.ActiveCount}");
        }
    }
}