// Packages/com.NappyCat.Tween/Runtime/NcTweenHub.cs
using System;
using UnityEngine;
using NappyCat.Tween;

namespace NappyCat
{
    /// <summary>
    /// Nc.Tween slot on the global hub. Wraps the core NcTween static engine.
    /// </summary>
    public static partial class Nc
    {
        public static class Tween
        {
            public static TweenHandle To(Func<float> get, Action<float> set, float to, in NcTweenOptions opt, in NcTweenEvents ev = default)
                => NcTween.To(get, set, to, opt, ev);

            public static TweenHandle To(Func<Vector2> get, Action<Vector2> set, Vector2 to, in NcTweenOptions opt, in NcTweenEvents ev = default)
                => NcTween.To(get, set, to, opt, ev);

            public static TweenHandle To(Func<Vector3> get, Action<Vector3> set, Vector3 to, in NcTweenOptions opt, in NcTweenEvents ev = default)
                => NcTween.To(get, set, to, opt, ev);

            public static TweenHandle To(Func<Vector4> get, Action<Vector4> set, Vector4 to, in NcTweenOptions opt, in NcTweenEvents ev = default)
                => NcTween.To(get, set, to, opt, ev);

            public static TweenHandle To(Func<Quaternion> get, Action<Quaternion> set, Quaternion to, in NcTweenOptions opt, in NcTweenEvents ev = default)
                => NcTween.To(get, set, to, opt, ev);

            public static TweenHandle To(Func<Color> get, Action<Color> set, Color to, in NcTweenOptions opt, in NcTweenEvents ev = default)
                => NcTween.To(get, set, to, opt, ev);

            public static void Kill(TweenHandle handle, bool complete = false)
                => NcTween.Kill(handle, complete);

            public static bool Complete(TweenHandle handle, bool invokeCallbacks = true)
                => NcTween.Complete(handle, invokeCallbacks);

            public static void Pause(TweenHandle handle, bool pause = true)
                => NcTween.Pause(handle, pause);

            public static void KillGroup(int groupId, bool complete = false)
                => NcTween.KillGroup(groupId, complete);

            public static void PauseGroup(int groupId, bool pause = true)
                => NcTween.PauseGroup(groupId, pause);

            public static void KillAll(bool complete = false)
                => NcTween.KillAll(complete);

            public static void PauseAll(bool pause = true)
                => NcTween.PauseAll(pause);

            public static bool IsAlive(TweenHandle handle)
                => NcTween.IsAlive(handle);

            public static bool OnStart(TweenHandle handle, Action action)
                => NcTween.OnStart(handle, action);

            public static bool OnUpdate(TweenHandle handle, Action<float> action)
                => NcTween.OnUpdate(handle, action);

            public static bool OnLoop(TweenHandle handle, Action<int> action)
                => NcTween.OnLoop(handle, action);

            public static bool OnComplete(TweenHandle handle, Action action)
                => NcTween.OnComplete(handle, action);

            public static bool OnKill(TweenHandle handle, Action action)
                => NcTween.OnKill(handle, action);

            public static bool ClearCallbacks(TweenHandle handle)
                => NcTween.ClearCallbacks(handle);

            public static bool TryGetProgress(TweenHandle handle, out float progress)
                => NcTween.TryGetProgress(handle, out progress);

            public static bool TryGetRemainingTime(TweenHandle handle, out float remaining)
                => NcTween.TryGetRemainingTime(handle, out remaining);

            public static int ActiveCount => NcTween.ActiveCount;
            public static double LastTickMicros => NcTween.LastTickMicros;

            public static void ManualTick(float dt) => NcTween.ManualTick(dt);
            public static NcTweenRunner EnsureRunner() => NcTween.EnsureRunner();
        }
    }
}

