using UnityEngine;

namespace NappyCat.Tween
{
    public enum NcClock { Scaled, Unscaled, Manual }

    public enum NcLoop { Once, Loop, Yoyo }

    /// <summary>
    /// Immutable tween options used at schedule time. Use the constructor to set fields.
    /// </summary>
    public readonly struct NcTweenOptions
    {
        public readonly float Duration;
        public readonly float Delay;
        public readonly NcEase Ease;
        public readonly AnimationCurve CustomCurve;
        public readonly NcLoop Loop;
        public readonly int LoopCount;
        public readonly NcClock Clock;
        public readonly int GroupId;

        public NcTweenOptions(
            float duration,
            float delay = 0f,
            NcEase ease = NcEase.OutQuad,
            AnimationCurve customCurve = null,
            NcLoop loop = NcLoop.Once,
            int loopCount = 1,
            NcClock clock = NcClock.Scaled,
            int groupId = 0)
        {
            Duration = Mathf.Max(0.000001f, duration);
            Delay = Mathf.Max(0f, delay);
            Ease = ease;
            CustomCurve = customCurve;
            Loop = loop;
            LoopCount = Mathf.Max(0, loopCount);
            Clock = clock;
            GroupId = groupId;
        }

        public NcTweenOptions WithDuration(float duration)
            => new NcTweenOptions(duration, Delay, Ease, CustomCurve, Loop, LoopCount, Clock, GroupId);

        public NcTweenOptions WithDelay(float delay)
            => new NcTweenOptions(Duration, delay, Ease, CustomCurve, Loop, LoopCount, Clock, GroupId);

        public NcTweenOptions WithEase(NcEase ease, AnimationCurve customCurve = null)
            => new NcTweenOptions(Duration, Delay, ease, customCurve, Loop, LoopCount, Clock, GroupId);

        public NcTweenOptions WithLoop(NcLoop loop, int loopCount = 1)
            => new NcTweenOptions(Duration, Delay, Ease, CustomCurve, loop, loopCount, Clock, GroupId);

        public NcTweenOptions WithClock(NcClock clock)
            => new NcTweenOptions(Duration, Delay, Ease, CustomCurve, Loop, LoopCount, clock, GroupId);

        public NcTweenOptions WithGroup(int groupId)
            => new NcTweenOptions(Duration, Delay, Ease, CustomCurve, Loop, LoopCount, Clock, groupId);
    }
}
