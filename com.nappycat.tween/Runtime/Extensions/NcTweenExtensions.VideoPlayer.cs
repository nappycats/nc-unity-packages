using UnityEngine;
#if UNITY_2018_3_OR_NEWER
using UnityEngine.Video;
#endif

namespace NappyCat.Tween
{
    /// <summary>VideoPlayer tween helpers.</summary>
    public static partial class NcTweenExtensions
    {
#if UNITY_2018_3_OR_NEWER
        public static TweenHandle NcPlaybackSpeedTo(this VideoPlayer vp, float to, float duration)
        {
            return NcTween.To(() => (float)vp.playbackSpeed, v => vp.playbackSpeed = v, to, new NcTweenOptions(duration));
        }

        public static TweenHandle NcPlaybackSpeedTo(this VideoPlayer vp, float to, in NcTweenOptions options)
        {
            return NcTween.To(() => (float)vp.playbackSpeed, v => vp.playbackSpeed = v, to, options);
        }

        public static TweenHandle NcTimeTo(this VideoPlayer vp, float to, float duration)
        {
            // Bridge double VideoPlayer.time using a float tween
            return NcTween.To(() => (float)vp.time, v => vp.time = v, to, new NcTweenOptions(duration));
        }

        public static TweenHandle NcTimeTo(this VideoPlayer vp, float to, in NcTweenOptions options)
        {
            return NcTween.To(() => (float)vp.time, v => vp.time = v, to, options);
        }
#endif
    }
}
