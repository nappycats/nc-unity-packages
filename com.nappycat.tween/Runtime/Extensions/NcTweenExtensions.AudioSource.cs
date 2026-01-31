using UnityEngine;

namespace NappyCat.Tween
{
    /// <summary>AudioSource tween helpers.</summary>
    public static partial class NcTweenExtensions
    {
        public static TweenHandle NcVolumeTo(this AudioSource source, float to, float duration)
        {
            return NcTween.To(() => source.volume, v => source.volume = v, to, new NcTweenOptions(duration));
        }

        public static TweenHandle NcVolumeTo(this AudioSource source, float to, in NcTweenOptions options)
        {
            return NcTween.To(() => source.volume, v => source.volume = v, to, options);
        }

        public static TweenHandle NcPitchTo(this AudioSource source, float to, float duration)
        {
            return NcTween.To(() => source.pitch, v => source.pitch = v, to, new NcTweenOptions(duration));
        }

        public static TweenHandle NcPitchTo(this AudioSource source, float to, in NcTweenOptions options)
        {
            return NcTween.To(() => source.pitch, v => source.pitch = v, to, options);
        }

        public static TweenHandle NcStereoPanTo(this AudioSource source, float to, float duration)
        {
            return NcTween.To(() => source.panStereo, v => source.panStereo = v, to, new NcTweenOptions(duration));
        }

        public static TweenHandle NcStereoPanTo(this AudioSource source, float to, in NcTweenOptions options)
        {
            return NcTween.To(() => source.panStereo, v => source.panStereo = v, to, options);
        }
    }
}

