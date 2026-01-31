using UnityEngine;

namespace NappyCat.Tween
{
    /// <summary>TrailRenderer tween helpers.</summary>
    public static partial class NcTweenExtensions
    {
        public static TweenHandle NcTimeTo(this TrailRenderer tr, float to, float duration)
        {
            return NcTween.To(() => tr.time, v => tr.time = v, to, new NcTweenOptions(duration));
        }

        public static TweenHandle NcTimeTo(this TrailRenderer tr, float to, in NcTweenOptions options)
        {
            return NcTween.To(() => tr.time, v => tr.time = v, to, options);
        }

        public static TweenHandle NcStartColorTo(this TrailRenderer tr, Color to, float duration)
        {
            return NcTween.To(() => tr.startColor, v => tr.startColor = v, to, new NcTweenOptions(duration));
        }

        public static TweenHandle NcStartColorTo(this TrailRenderer tr, Color to, in NcTweenOptions options)
        {
            return NcTween.To(() => tr.startColor, v => tr.startColor = v, to, options);
        }

        public static TweenHandle NcEndColorTo(this TrailRenderer tr, Color to, float duration)
        {
            return NcTween.To(() => tr.endColor, v => tr.endColor = v, to, new NcTweenOptions(duration));
        }

        public static TweenHandle NcEndColorTo(this TrailRenderer tr, Color to, in NcTweenOptions options)
        {
            return NcTween.To(() => tr.endColor, v => tr.endColor = v, to, options);
        }

        public static TweenHandle NcWidthMultiplierTo(this TrailRenderer tr, float to, float duration)
        {
            return NcTween.To(() => tr.widthMultiplier, v => tr.widthMultiplier = v, to, new NcTweenOptions(duration));
        }

        public static TweenHandle NcWidthMultiplierTo(this TrailRenderer tr, float to, in NcTweenOptions options)
        {
            return NcTween.To(() => tr.widthMultiplier, v => tr.widthMultiplier = v, to, options);
        }
    }
}

