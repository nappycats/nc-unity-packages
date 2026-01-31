/*
* NAPPY CAT
*
* Copyright © 2025 Nappy Cat Games
* http://nappycat.net
*
* Author: Stan Nesi
*
* File: Packages/com.NappyCat.Tween/Runtime/Sequences/NcSequence.cs
* Created: 2024-06-19
*/

using System;
using System.Collections.Generic;

namespace NappyCat.Tween
{
    /// <summary>
    /// Precise, event-driven sequences.
    /// Then() creates a new serial step;
    /// Also() adds to current parallel step.
    /// </summary>
    public sealed class NcSequence
    {
        struct Step { public List<Func<TweenHandle>> factories; }
        readonly List<Step> _steps = new List<Step>();
        Action _onComplete;

        public NcSequence Then(Func<TweenHandle> schedule)
        {
            var step = new Step
            {
                factories = new List<Func<TweenHandle>> { schedule }
            };
            
            _steps.Add(step);
            return this;
        }

        public NcSequence Also(Func<TweenHandle> schedule)
        {
            if (_steps.Count == 0)
                return Then(schedule);
            
            _steps[_steps.Count - 1].factories.Add(schedule);
            return this;
        }

        public NcSequence OnComplete(Action action)
        {
            _onComplete += action;
            return this;
        }

        public void Play()
        {
            PlayStep(0);
        }

        void PlayStep(int index)
        {
            if (index >= _steps.Count)
            {
                _onComplete?.Invoke();
                return;
            }

            var step = _steps[index];
            if (step.factories == null || step.factories.Count == 0)
            {
                PlayStep(index + 1);
                return;
            }

            int remaining = step.factories.Count;
            for (int i = 0; i < step.factories.Count; i++)
            {
                var handle = step.factories[i]();
                bool registered = handle.IsValid && NcTween.OnComplete(handle, OnOneFinished);
                if (handle.IsValid)
                {
                    NcTween.OnKill(handle, OnOneFinished);
                }
                if (!registered)
                {
                    OnOneFinished();
                }
            }

            void OnOneFinished()
            {
                remaining--;
                if (remaining == 0)
                {
                    PlayStep(index + 1);
                }
            }
        }
    }
}
