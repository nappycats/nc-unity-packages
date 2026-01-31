using UnityEngine;

namespace NappyCat.Tween
{
    /// <summary>Camera tween helpers.</summary>
    public static partial class NcTweenExtensions
    {
        public static TweenHandle NcFieldOfViewTo(this Camera camera, float to, float duration)
        {
            return NcTween.To(() => camera.fieldOfView, v => camera.fieldOfView = v, to, new NcTweenOptions(duration));
        }

        public static TweenHandle NcFieldOfViewTo(this Camera camera, float to, in NcTweenOptions options)
        {
            return NcTween.To(() => camera.fieldOfView, v => camera.fieldOfView = v, to, options);
        }

        public static TweenHandle NcBackgroundColorTo(this Camera camera, Color to, float duration)
        {
            return NcTween.To(() => camera.backgroundColor, v => camera.backgroundColor = v, to, new NcTweenOptions(duration));
        }

        public static TweenHandle NcBackgroundColorTo(this Camera camera, Color to, in NcTweenOptions options)
        {
            return NcTween.To(() => camera.backgroundColor, v => camera.backgroundColor = v, to, options);
        }
    }
}

