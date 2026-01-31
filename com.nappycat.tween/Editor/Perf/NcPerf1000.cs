/*
* NAPPY CAT
*
* Copyright © 2025 Nappy Cat Games
* http://nappycat.net
*
* Author: Stan Nesi
*
* File: Packages/com.NappyCat.Tween/Editor/Perf/NcPerf1000.cs
* Created: 2024-06-19
*/

#if UNITY_EDITOR
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

namespace NappyCat.Tween
{
    public sealed class NcPerf1000 : EditorWindow
    {
        const int kCount = 1000;
        const int kSampleFrames = 300;

        readonly List<TweenHandle> _handles = new List<TweenHandle>(kCount);
        readonly Stopwatch _stopwatch = new Stopwatch();

        double _accumulatedMicros;
        int _frames;
        int _gc0Start, _gc1Start, _gc2Start;
        bool _running;

        [MenuItem("Nappy Cat/Perf/Run 1000 Tween Test", priority = 0)]
        public static void Run()
        {
            GetWindow<NcPerf1000>("NcTween Perf 1000").Show();
        }

        void OnEnable()
        {
            _running = false;
        }

        void OnDisable()
        {
            if (_running)
            {
                EditorApplication.update -= Tick;
                _running = false;
            }
        }

        void OnGUI()
        {
            EditorGUILayout.HelpBox("Spawns 1000 tweens and manual-ticks them for ~5s to confirm 0 GC.", MessageType.Info);

            using (new EditorGUI.DisabledScope(_running))
            {
                if (GUILayout.Button("Spawn 1000 Tweens (Manual Clock)"))
                {
                    SpawnTweens();
                }
            }

            if (_running)
            {
                GUILayout.Space(8);
                GUILayout.Label($"Frames: {_frames}");
                GUILayout.Label($"Average µs/tick: {(_frames > 0 ? _accumulatedMicros / _frames : 0d):F2}");
                GUILayout.Label($"Active Tweens: {NcTween.ActiveCount}");
            }
        }

        void SpawnTweens()
        {
            _handles.Clear();

            for (int i = 0; i < kCount; i++)
            {
                float value = 0f;
                var handle = NcTween.To(
                    () => value,
                    v => value = v,
                    1f,
                    new NcTweenOptions(duration: 1f, delay: 0f, ease: NcEase.OutQuad, customCurve: null, loop: NcLoop.Once, loopCount: 1, clock: NcClock.Manual, groupId: 0));
                _handles.Add(handle);
            }

            _accumulatedMicros = 0;
            _frames = 0;
            _gc0Start = System.GC.CollectionCount(0);
            _gc1Start = System.GC.CollectionCount(1);
            _gc2Start = System.GC.CollectionCount(2);

            if (!_running)
            {
                EditorApplication.update += Tick;
                _running = true;
            }
        }

        void Tick()
        {
            _stopwatch.Restart();
            NcTween.ManualTick(Time.deltaTime);
            _stopwatch.Stop();

            _accumulatedMicros += _stopwatch.ElapsedTicks * (1_000_000.0 / Stopwatch.Frequency);
            _frames++;

            if (_frames >= kSampleFrames)
            {
                EditorApplication.update -= Tick;
                _running = false;

                int gc0 = System.GC.CollectionCount(0) - _gc0Start;
                int gc1 = System.GC.CollectionCount(1) - _gc1Start;
                int gc2 = System.GC.CollectionCount(2) - _gc2Start;

                UnityEngine.Debug.Log($"[NcPerf1000] Average µs/tick = {_accumulatedMicros / _frames:F2}");
                UnityEngine.Debug.Log($"[NcPerf1000] GC Collections (Gen0/1/2) => {gc0}/{gc1}/{gc2}");

                if (gc0 == 0 && gc1 == 0 && gc2 == 0)
                {
                    UnityEngine.Debug.Log("[NcPerf1000] PASS: No GC during test.");
                }
                else
                {
                    UnityEngine.Debug.LogWarning("[NcPerf1000] WARNING: GC occurred; investigate external allocations.");
                }
            }
        }
    }
}
#endif
