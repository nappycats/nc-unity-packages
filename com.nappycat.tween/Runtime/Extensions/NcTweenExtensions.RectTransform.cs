using UnityEngine;

namespace NappyCat.Tween
{
    /// <summary>RectTransform helpers.</summary>
    public static partial class NcTweenExtensions
    {
        public static TweenHandle NcAnchoredPositionTo(this RectTransform rt, Vector2 to, float duration)
        {
            return NcTween.To(() => rt.anchoredPosition, v => rt.anchoredPosition = v, to, new NcTweenOptions(duration));
        }

        public static TweenHandle NcAnchoredPositionTo(this RectTransform rt, Vector2 to, in NcTweenOptions options)
        {
            return NcTween.To(() => rt.anchoredPosition, v => rt.anchoredPosition = v, to, options);
        }

        public static TweenHandle NcSizeDeltaTo(this RectTransform rt, Vector2 to, float duration)
        {
            return NcTween.To(() => rt.sizeDelta, v => rt.sizeDelta = v, to, new NcTweenOptions(duration));
        }

        public static TweenHandle NcSizeDeltaTo(this RectTransform rt, Vector2 to, in NcTweenOptions options)
        {
            return NcTween.To(() => rt.sizeDelta, v => rt.sizeDelta = v, to, options);
        }
    }
}

