#if NC_UNITY_TMP
using TMPro;
using UnityEngine;

namespace NappyCat.Tween
{
    /// <summary>TextMeshPro helpers.</summary>
    public static partial class NcTweenExtensions
    {
        public static TweenHandle NcColorTo(this TMP_Text text, Color to, float duration)
        {
            return NcTween.To(() => text.color, v => text.color = v, to, new NcTweenOptions(duration));
        }

        public static TweenHandle NcColorTo(this TMP_Text text, Color to, in NcTweenOptions options)
        {
            return NcTween.To(() => text.color, v => text.color = v, to, options);
        }

        public static TweenHandle NcAlphaTo(this TMP_Text text, float to, float duration)
        {
            return NcTween.To(() => text.alpha, a => text.alpha = a, to, new NcTweenOptions(duration));
        }

        public static TweenHandle NcAlphaTo(this TMP_Text text, float to, in NcTweenOptions options)
        {
            return NcTween.To(() => text.alpha, a => text.alpha = a, to, options);
        }
    }
}
#endif
