#if NC_UNITY_UGUI
using UnityEngine;
using UnityEngine.UI;

namespace NappyCat.Tween
{
    /// <summary>UGUI Image helpers.</summary>
    public static partial class NcTweenExtensions
    {
        public static TweenHandle NcFillTo(this Image image, float to, float duration)
        {
            return NcTween.To(() => image.fillAmount, v => image.fillAmount = v, to, new NcTweenOptions(duration));
        }

        public static TweenHandle NcFillTo(this Image image, float to, in NcTweenOptions options)
        {
            return NcTween.To(() => image.fillAmount, v => image.fillAmount = v, to, options);
        }

        public static TweenHandle NcColorTo(this Image image, Color to, float duration)
        {
            return NcTween.To(() => image.color, v => image.color = v, to, new NcTweenOptions(duration));
        }

        public static TweenHandle NcColorTo(this Image image, Color to, in NcTweenOptions options)
        {
            return NcTween.To(() => image.color, v => image.color = v, to, options);
        }

        public static TweenHandle NcAlphaTo(this Image image, float to, float duration)
        {
            return NcTween.To(() => image.color.a, a =>
            {
                var c = image.color;
                c.a = a;
                image.color = c;
            }, to, new NcTweenOptions(duration));
        }

        public static TweenHandle NcAlphaTo(this Image image, float to, in NcTweenOptions options)
        {
            return NcTween.To(() => image.color.a, a =>
            {
                var c = image.color;
                c.a = a;
                image.color = c;
            }, to, options);
        }
    }
}
#endif
