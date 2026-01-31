using UnityEngine;

namespace NappyCat.Tween
{
    /// <summary>Light tween helpers.</summary>
    public static partial class NcTweenExtensions
    {
        public static TweenHandle NcIntensityTo(this Light light, float to, float duration)
        {
            return NcTween.To(() => light.intensity, v => light.intensity = v, to, new NcTweenOptions(duration));
        }

        public static TweenHandle NcIntensityTo(this Light light, float to, in NcTweenOptions options)
        {
            return NcTween.To(() => light.intensity, v => light.intensity = v, to, options);
        }

        public static TweenHandle NcColorTo(this Light light, Color to, float duration)
        {
            return NcTween.To(() => light.color, v => light.color = v, to, new NcTweenOptions(duration));
        }

        public static TweenHandle NcColorTo(this Light light, Color to, in NcTweenOptions options)
        {
            return NcTween.To(() => light.color, v => light.color = v, to, options);
        }
    }
}

