using UnityEngine;

namespace NappyCat.Tween
{
    /// <summary>LineRenderer tween helpers.</summary>
    public static partial class NcTweenExtensions
    {
        public static TweenHandle NcStartColorTo(this LineRenderer lr, Color to, float duration)
        {
            return NcTween.To(() => lr.startColor, v => lr.startColor = v, to, new NcTweenOptions(duration));
        }

        public static TweenHandle NcStartColorTo(this LineRenderer lr, Color to, in NcTweenOptions options)
        {
            return NcTween.To(() => lr.startColor, v => lr.startColor = v, to, options);
        }

        public static TweenHandle NcEndColorTo(this LineRenderer lr, Color to, float duration)
        {
            return NcTween.To(() => lr.endColor, v => lr.endColor = v, to, new NcTweenOptions(duration));
        }

        public static TweenHandle NcEndColorTo(this LineRenderer lr, Color to, in NcTweenOptions options)
        {
            return NcTween.To(() => lr.endColor, v => lr.endColor = v, to, options);
        }

        public static TweenHandle NcWidthMultiplierTo(this LineRenderer lr, float to, float duration)
        {
            return NcTween.To(() => lr.widthMultiplier, v => lr.widthMultiplier = v, to, new NcTweenOptions(duration));
        }

        public static TweenHandle NcWidthMultiplierTo(this LineRenderer lr, float to, in NcTweenOptions options)
        {
            return NcTween.To(() => lr.widthMultiplier, v => lr.widthMultiplier = v, to, options);
        }
    }
}

