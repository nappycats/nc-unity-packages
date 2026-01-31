/*
* NAPPY CAT
*
* Copyright © 2025 Nappy Cat Games
* http://nappycat.net
*
* Author: Stan Nesi
*
* File: Packages/com.NappyCat.Tween/Runtime/NcTween.cs
* Created: 2024-06-19
*/


using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace NappyCat.Tween
{
    /// <summary>
    /// Opaque handle to a scheduled tween. Id encodes index+generation to avoid stale references.
    /// </summary>
    public readonly struct TweenHandle
    {
        public readonly int Id;
        public bool IsValid => Id != 0;
        public TweenHandle(int id)
        {
            Id = id;
        }
        public override string ToString() => $"TweenHandle({Id})";
    }

    internal enum TweenType : byte { Float, V2, V3, V4, Quat, Color }

    internal struct Tween
    {
        public int generation;
        public TweenType type;
        public int groupId;
        public NcClock clock;
        public float duration;
        public float delayRemaining;
        public float elapsed;
        public NcEase ease;
        public AnimationCurve customCurve;
        public NcLoop loop;
        public int loopCount;
        public bool yoyoDir;
        public bool paused;
        public bool started;
        public bool killed;
        public Delegate getter;
        public Delegate setter;
        public float f0, f1;
        public Vector2 v20, v21;
        public Vector3 v30, v31;
        public Vector4 v40, v41;
        public Quaternion q0, q1;
        public Color c0, c1;
        public Action onStart;
        public Action<float> onUpdate;
        public Action<int> onLoop;
        public Action onComplete;
        public Action onKill;

        public void ResetEventsFrom(in NcTweenEvents ev)
        {
            onStart = ev.OnStart;
            onUpdate = ev.OnUpdate;
            onLoop = ev.OnLoop;
            onComplete = ev.OnComplete;
            onKill = ev.OnKill;
        }
    }

    /// <summary>
    /// engine. Component-less scheduling and pooled ticking.
    /// Use extension methods for ergonomic one-liners.
    /// </summary>
    public static class NcTween
    {
        const int kInitialCapacity = 256;
        static Tween[] s_Tweens = new Tween[kInitialCapacity];
        static int[] s_Next = new int[kInitialCapacity];
        static int s_FreeHead;
        static int s_Capacity = kInitialCapacity;
        static int s_Generation = 1;
        static bool s_Initialized;

        static float s_ManualDelta;
        static int s_ActiveCount;
        public static int ActiveCount => s_ActiveCount;

        static readonly Stopwatch s_TickWatch = new Stopwatch();
        static double s_LastTickMicros;
        public static double LastTickMicros => s_LastTickMicros;

        static NcTween()
        {
            EnsureInitialized();
        }

        public static bool AnyActive => s_ActiveCount > 0;

        static void EnsureInitialized()
        {
            if (s_Initialized) return;
            s_Initialized = true;
            s_FreeHead = 0;
            for (int i = 1; i < s_Capacity; i++) FreeSlot(i);
        }

        static void Grow()
        {
            int oldCap = s_Capacity;
            int newCap = Mathf.Max(64, oldCap * 2);
            Array.Resize(ref s_Tweens, newCap);
            Array.Resize(ref s_Next, newCap);
            for (int i = newCap - 1; i >= oldCap; i--) FreeSlot(i);
            s_Capacity = newCap;
        }

        static void FreeSlot(int idx)
        {
            s_Next[idx] = s_FreeHead;
            s_FreeHead = idx;
        }

        static int Alloc()
        {
            if (s_FreeHead == 0)
                Grow();

            int idx = s_FreeHead;
            s_FreeHead = s_Next[idx];
            ref var tw = ref s_Tweens[idx];
            tw.generation = ++s_Generation;
            tw.started = false;
            tw.killed = false;
            tw.paused = false;
            tw.elapsed = 0f;
            tw.delayRemaining = 0f;
            tw.yoyoDir = false;
            tw.onStart = null;
            tw.onUpdate = null;
            tw.onLoop = null;
            tw.onComplete = null;
            tw.onKill = null;
            s_ActiveCount++;
            return idx;
        }

        static TweenHandle MakeHandle(int idx) => new TweenHandle((s_Tweens[idx].generation << 16) | (idx & 0xFFFF));

        static bool TryGetIndex(TweenHandle handle, out int idx)
        {
            idx = handle.Id & 0xFFFF;
            int gen = handle.Id >> 16;
            if (idx <= 0 || idx >= s_Capacity)
                return false;
            ref var tw = ref s_Tweens[idx];
            if (tw.generation != gen || tw.killed)
                return false;
            return true;
        }

        static void Release(int idx, bool fireKill)
        {
            ref var tw = ref s_Tweens[idx];
            if (fireKill && tw.onKill != null)
            {
                try { tw.onKill(); } catch { }
            }
            tw.killed = true;
            s_ActiveCount--;
            FreeSlot(idx);
        }

        public static TweenHandle To(Func<float> get, Action<float> set, float to, in NcTweenOptions opt, in NcTweenEvents ev = default)
            => Schedule(TweenType.Float, get, set, to, default, default, default, default, default, opt, ev);
        public static TweenHandle To(Func<Vector2> get, Action<Vector2> set, Vector2 to, in NcTweenOptions opt, in NcTweenEvents ev = default)
            => Schedule(TweenType.V2, get, set, default, to, default, default, default, default, opt, ev);
        public static TweenHandle To(Func<Vector3> get, Action<Vector3> set, Vector3 to, in NcTweenOptions opt, in NcTweenEvents ev = default)
            => Schedule(TweenType.V3, get, set, default, default, to, default, default, default, opt, ev);
        public static TweenHandle To(Func<Vector4> get, Action<Vector4> set, Vector4 to, in NcTweenOptions opt, in NcTweenEvents ev = default)
            => Schedule(TweenType.V4, get, set, default, default, default, to, default, default, opt, ev);
        public static TweenHandle To(Func<Quaternion> get, Action<Quaternion> set, Quaternion to, in NcTweenOptions opt, in NcTweenEvents ev = default)
            => Schedule(TweenType.Quat, get, set, default, default, default, default, to, default, opt, ev);
        public static TweenHandle To(Func<Color> get, Action<Color> set, Color to, in NcTweenOptions opt, in NcTweenEvents ev = default)
            => Schedule(TweenType.Color, get, set, default, default, default, default, default, to, opt, ev);

        static TweenHandle Schedule(
            TweenType type,
            Delegate getter,
            Delegate setter,
            float fTo,
            Vector2 v2To,
            Vector3 v3To,
            Vector4 v4To,
            Quaternion qTo,
            Color cTo,
            in NcTweenOptions opt,
            in NcTweenEvents ev)
        {
            int idx = Alloc();
            ref var tw = ref s_Tweens[idx];
            tw.type = type;
            tw.getter = getter;
            tw.setter = setter;
            tw.duration = Mathf.Max(0.000001f, opt.Duration);
            tw.delayRemaining = opt.Delay;
            tw.ease = opt.Ease;
            tw.customCurve = opt.CustomCurve;
            tw.loop = opt.Loop;
            tw.loopCount = opt.LoopCount;
            tw.clock = opt.Clock;
            tw.groupId = opt.GroupId;
            tw.ResetEventsFrom(ev);

            switch (type)
            {
                case TweenType.Float:
                    tw.f1 = fTo;
                    tw.f0 = ((Func<float>)getter)();
                    break;
                case TweenType.V2:
                    tw.v21 = v2To;
                    tw.v20 = ((Func<Vector2>)getter)();
                    break;
                case TweenType.V3:
                    tw.v31 = v3To;
                    tw.v30 = ((Func<Vector3>)getter)();
                    break;
                case TweenType.V4:
                    tw.v41 = v4To;
                    tw.v40 = ((Func<Vector4>)getter)();
                    break;
                case TweenType.Quat:
                    tw.q1 = qTo;
                    tw.q0 = ((Func<Quaternion>)getter)();
                    break;
                case TweenType.Color:
                    tw.c1 = cTo;
                    tw.c0 = ((Func<Color>)getter)();
                    break;
            }

            EnsureRunner();
            return MakeHandle(idx);
        }

        public static void Kill(TweenHandle handle, bool complete = false)
        {
            if (!TryGetIndex(handle, out var idx))
                return;

            ref var tw = ref s_Tweens[idx];
            
            if (complete)
                Apply(tw, tw.yoyoDir ? 0f : 1f);
            
            Release(idx, fireKill: true);
        }

        public static bool Complete(TweenHandle handle, bool invokeCallbacks = true)
        {
            if (!TryGetIndex(handle, out var idx))
                return false;

            ref var tw = ref s_Tweens[idx];
            Apply(tw, tw.yoyoDir ? 0f : 1f);
            tw.elapsed = tw.duration;

            if (invokeCallbacks && tw.onComplete != null)
            {
                try { tw.onComplete(); }
                catch { }
            }

            Release(idx, fireKill: false);
            return true;
        }

        public static void Pause(TweenHandle handle, bool pause = true)
        {
            if (!TryGetIndex(handle, out var idx))
                return;

            ref var tw = ref s_Tweens[idx];
            tw.paused = pause;
        }

        public static void KillGroup(int groupId, bool complete = false)
        {
            for (int i = 1; i < s_Capacity; i++)
            {
                ref var tw = ref s_Tweens[i];
                if (tw.generation == 0 || tw.killed || tw.groupId != groupId)
                    continue;

                if (complete)
                    Apply(tw, tw.yoyoDir ? 0f : 1f);
                
                Release(i, fireKill: true);
            }
        }

        public static void PauseGroup(int groupId, bool pause = true)
        {
            for (int i = 1; i < s_Capacity; i++)
            {
                ref var tw = ref s_Tweens[i];
                if (tw.generation == 0 || tw.killed || tw.groupId != groupId)
                    continue;
                
                tw.paused = pause;
            }
        }

        public static void KillAll(bool complete = false)
        {
            for (int i = 1; i < s_Capacity; i++)
            {
                ref var tw = ref s_Tweens[i];
                if (tw.generation == 0 || tw.killed)
                    continue;

                if (complete)
                    Apply(tw, tw.yoyoDir ? 0f : 1f);

                Release(i, fireKill: true);
            }
        }

        public static void PauseAll(bool pause = true)
        {
            for (int i = 1; i < s_Capacity; i++)
            {
                ref var tw = ref s_Tweens[i];
                if (tw.generation == 0 || tw.killed)
                    continue;

                tw.paused = pause;
            }
        }

        public static bool IsAlive(TweenHandle handle) => TryGetIndex(handle, out _);

        public static bool OnStart(TweenHandle handle, Action action)
        {
            if (!TryGetIndex(handle, out var idx))
                return false;
            
            ref var tw = ref s_Tweens[idx];
            tw.onStart += action;
            return true;
        }

        public static bool OnUpdate(TweenHandle handle, Action<float> action)
        {
            if (!TryGetIndex(handle, out var idx))
                return false;
            
            ref var tw = ref s_Tweens[idx];
            tw.onUpdate += action;
            return true;
        }

        public static bool OnLoop(TweenHandle handle, Action<int> action)
        {
            if (!TryGetIndex(handle, out var idx))
                return false;
            
            ref var tw = ref s_Tweens[idx];
            tw.onLoop += action;
            return true;
        }

        public static bool OnComplete(TweenHandle handle, Action action)
        {
            if (!TryGetIndex(handle, out var idx))
                return false;
            
            ref var tw = ref s_Tweens[idx];
            tw.onComplete += action;
            return true;
        }

        public static bool OnKill(TweenHandle handle, Action action)
        {
            if (!TryGetIndex(handle, out var idx))
                return false;
            
            ref var tw = ref s_Tweens[idx];
            tw.onKill += action;
            return true;
        }

        public static bool ClearCallbacks(TweenHandle handle)
        {
            if (!TryGetIndex(handle, out var idx))
                return false;

            ref var tw = ref s_Tweens[idx];
            tw.onStart = null;
            tw.onUpdate = null;
            tw.onLoop = null;
            tw.onComplete = null;
            tw.onKill = null;
            return true;
        }

        public static bool TryGetProgress(TweenHandle handle, out float progress)
        {
            if (!TryGetIndex(handle, out var idx))
            {
                progress = 0f;
                return false;
            }

            ref var tw = ref s_Tweens[idx];
            progress = tw.duration <= 0f ? 1f : Mathf.Clamp01(tw.elapsed / tw.duration);
            return true;
        }

        public static bool TryGetRemainingTime(TweenHandle handle, out float remaining)
        {
            if (!TryGetIndex(handle, out var idx))
            {
                remaining = 0f;
                return false;
            }

            ref var tw = ref s_Tweens[idx];
            remaining = Mathf.Max(0f, tw.duration - tw.elapsed);
            return true;
        }

        internal static void Tick(float dtScaled, float dtUnscaled)
        {
            s_TickWatch.Restart();
            float manual = s_ManualDelta;
            s_ManualDelta = 0f;

            for (int i = 1; i < s_Capacity; i++)
            {
                ref var tw = ref s_Tweens[i];
                if (tw.generation == 0 || tw.killed || tw.paused)
                    continue;

                float dt = tw.clock == NcClock.Scaled ? dtScaled : (tw.clock == NcClock.Unscaled ? dtUnscaled : manual);
                
                if (dt <= 0f)
                    continue;

                if (tw.delayRemaining > 0f)
                {
                    tw.delayRemaining -= dt;
                    
                    if (tw.delayRemaining > 0f)
                        continue;

                    tw.started = true;

                    if (tw.onStart != null)
                    {
                        try {
                            tw.onStart();
                        }
                        catch { } 
                    }
                }

                tw.elapsed += dt;
                float t = Mathf.Clamp01(tw.elapsed / tw.duration);
                float eased = NcTweenEase.Evaluate(tw.ease, tw.yoyoDir ? 1f - t : t, tw.customCurve);
                Apply(tw, eased);

                if (tw.onUpdate != null)
                {
                    try {
                        tw.onUpdate(eased);
                    }
                    catch { }
                }

                if (tw.elapsed >= tw.duration)
                {
                    bool looping = tw.loop != NcLoop.Once && (tw.loopCount == 0 || tw.loopCount > 1);
                    if (looping)
                    {
                        if (tw.loopCount > 1)
                            tw.loopCount--;
                        
                        tw.elapsed = 0f;
                        tw.delayRemaining = 0f;

                        if (tw.loop == NcLoop.Yoyo)
                            tw.yoyoDir = !tw.yoyoDir;
                        
                        if (tw.onLoop != null)
                        {
                            try
                            {
                                tw.onLoop(tw.loopCount);
                            }
                            catch { }
                        }

                        if (tw.loop == NcLoop.Loop)
                        {
                            switch (tw.type)
                            {
                                case TweenType.Float:
                                    tw.f0 = ((Func<float>)tw.getter)();
                                    break;
                                
                                case TweenType.V2:
                                    tw.v20 = ((Func<Vector2>)tw.getter)();
                                    break;

                                case TweenType.V3:
                                    tw.v30 = ((Func<Vector3>)tw.getter)();
                                    break;

                                case TweenType.V4:
                                    tw.v40 = ((Func<Vector4>)tw.getter)();
                                    break;

                                case TweenType.Quat:
                                    tw.q0 = ((Func<Quaternion>)tw.getter)();
                                    break;
                                
                                case TweenType.Color:
                                    tw.c0 = ((Func<Color>)tw.getter)();
                                    break;
                            }
                        }
                        continue;
                    }

                    Apply(tw, tw.yoyoDir ? 0f : 1f);
                    if (tw.onComplete != null)
                    {
                        try
                        {
                            tw.onComplete();
                        }
                        catch { }
                    }
                    
                    Release(i, fireKill: false);
                }
            }

            s_TickWatch.Stop();
            s_LastTickMicros = s_TickWatch.ElapsedTicks * (1_000_000.0 / Stopwatch.Frequency);
        }

        public static void ManualTick(float dt)
        {
            s_ManualDelta += Mathf.Max(0f, dt);
        }

        static void Apply(in Tween tw, float e)
        {
            switch (tw.type)
            {
                case TweenType.Float:
                    ((Action<float>)tw.setter)(Mathf.LerpUnclamped(tw.f0, tw.f1, e));
                    break;
                case TweenType.V2:
                    ((Action<Vector2>)tw.setter)(Vector2.LerpUnclamped(tw.v20, tw.v21, e));
                    break;
                case TweenType.V3:
                    ((Action<Vector3>)tw.setter)(Vector3.LerpUnclamped(tw.v30, tw.v31, e));
                    break;
                case TweenType.V4:
                    ((Action<Vector4>)tw.setter)(Vector4.LerpUnclamped(tw.v40, tw.v41, e));
                    break;
                case TweenType.Quat:
                    ((Action<Quaternion>)tw.setter)(Quaternion.SlerpUnclamped(tw.q0, tw.q1, e));
                    break;
                case TweenType.Color:
                    ((Action<Color>)tw.setter)(Color.LerpUnclamped(tw.c0, tw.c1, e));
                    break;
            }
        }

        public static NcTweenRunner EnsureRunner()
        {
            // Prefer explicit runner; else fall back to foundation runner.
            if (NcTweenRunner.Instance != null)
                return NcTweenRunner.Instance;
#if NC_PKG_FOUNDATION
            // Create a hidden runner via NcMonoRunner if no NcTweenRunner exists.
            var runner = NappyCat.Foundation.NcMonoRunner.Ensure("[NcTweenRunner]");
            runner.StartSafeCoroutine(NcTweenCoroutine());
            return null;
#else
            return NcTweenRunner.Instance;
#endif
        }

        static System.Collections.IEnumerator NcTweenCoroutine()
        {
            while (true)
            {
                float dt = Time.deltaTime;
                float udt = Time.unscaledDeltaTime;
                NcTween.Tick(dt, udt);
                yield return null;
            }
        }
    }
}
