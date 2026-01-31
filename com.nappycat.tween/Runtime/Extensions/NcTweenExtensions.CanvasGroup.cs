using UnityEngine;

namespace NappyCat.Tween
{
    /// <summary>CanvasGroup tweens.</summary>
    public static partial class NcTweenExtensions
    {
        public static TweenHandle NcAlphaTo(this CanvasGroup group, float to, float duration)
        {
            return NcTween.To(() => group.alpha, v => group.alpha = v, to, new NcTweenOptions(duration));
        }

        public static TweenHandle NcAlphaTo(this CanvasGroup group, float to, in NcTweenOptions options)
        {
            return NcTween.To(() => group.alpha, v => group.alpha = v, to, options);
        }
    }
}
