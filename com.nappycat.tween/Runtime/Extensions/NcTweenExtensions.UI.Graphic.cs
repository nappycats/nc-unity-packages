#if NC_UNITY_UGUI
using UnityEngine;
using UnityEngine.UI;

namespace NappyCat.Tween
{
    /// <summary>UGUI Graphic helpers for alpha.</summary>
    public static partial class NcTweenExtensions
    {
        public static TweenHandle NcColorTo(this Graphic graphic, Color to, float duration)
        {
            return NcTween.To(() => graphic.color, v => graphic.color = v, to, new NcTweenOptions(duration));
        }

        public static TweenHandle NcColorTo(this Graphic graphic, Color to, in NcTweenOptions options)
        {
            return NcTween.To(() => graphic.color, v => graphic.color = v, to, options);
        }

        public static TweenHandle NcAlphaTo(this Graphic graphic, float to, float duration)
        {
            return NcTween.To(() => graphic.color.a, a =>
            {
                var c = graphic.color;
                c.a = a;
                graphic.color = c;
            }, to, new NcTweenOptions(duration));
        }

        public static TweenHandle NcAlphaTo(this Graphic graphic, float to, in NcTweenOptions options)
        {
            return NcTween.To(() => graphic.color.a, a =>
            {
                var c = graphic.color;
                c.a = a;
                graphic.color = c;
            }, to, options);
        }
    }
}
#endif
