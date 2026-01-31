/*
* NAPPY CAT
*
* Copyright © 2025 Nappy Cat Games
* http://nappycat.net
*
* Author: Stan Nesi
*
* File: Packages/com.NappyCat.Tween/Runtime/NcTweenEvents.cs
* Created: 2024-06-19
*/

using System;

namespace NappyCat.Tween
{
    /// <summary>Optional callbacks stored on the tween instance. Attach at schedule time or later via handle.</summary>
    public readonly struct NcTweenEvents
    {
        public readonly Action OnStart;
        public readonly Action<float> OnUpdate;
        public readonly Action<int> OnLoop;
        public readonly Action OnComplete;
        public readonly Action OnKill;

        public NcTweenEvents(Action onStart = null, Action<float> onUpdate = null, Action<int> onLoop = null, Action onComplete = null, Action onKill = null)
        {
            OnStart = onStart;
            OnUpdate = onUpdate;
            OnLoop = onLoop;
            OnComplete = onComplete;
            OnKill = onKill;
        }
    }
}
