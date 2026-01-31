using UnityEngine;

namespace NappyCat.Tween
{
    /// <summary>Transform tween helpers. Component-less and alloc-free.</summary>
    public static partial class NcTweenExtensions
    {
        public static TweenHandle NcMoveTo(this Transform transform, Vector3 to, float duration)
        {
            return NcTween.To(() => transform.position, v => transform.position = v, to, new NcTweenOptions(duration));
        }

        public static TweenHandle NcMoveTo(this Transform transform, Vector3 to, in NcTweenOptions options)
        {
            return NcTween.To(() => transform.position, v => transform.position = v, to, options);
        }

        public static TweenHandle NcLocalMoveTo(this Transform transform, Vector3 to, float duration)
        {
            return NcTween.To(() => transform.localPosition, v => transform.localPosition = v, to, new NcTweenOptions(duration));
        }

        public static TweenHandle NcLocalMoveTo(this Transform transform, Vector3 to, in NcTweenOptions options)
        {
            return NcTween.To(() => transform.localPosition, v => transform.localPosition = v, to, options);
        }

        public static TweenHandle NcLocalScaleTo(this Transform transform, Vector3 to, float duration)
        {
            return NcTween.To(() => transform.localScale, v => transform.localScale = v, to, new NcTweenOptions(duration));
        }

        public static TweenHandle NcLocalScaleTo(this Transform transform, Vector3 to, in NcTweenOptions options)
        {
            return NcTween.To(() => transform.localScale, v => transform.localScale = v, to, options);
        }

        public static TweenHandle NcRotateTo(this Transform transform, Quaternion to, float duration)
        {
            return NcTween.To(() => transform.rotation, v => transform.rotation = v, to, new NcTweenOptions(duration));
        }

        public static TweenHandle NcRotateTo(this Transform transform, Quaternion to, in NcTweenOptions options)
        {
            return NcTween.To(() => transform.rotation, v => transform.rotation = v, to, options);
        }

        public static TweenHandle NcLocalRotateTo(this Transform transform, Quaternion to, float duration)
        {
            return NcTween.To(() => transform.localRotation, v => transform.localRotation = v, to, new NcTweenOptions(duration));
        }

        public static TweenHandle NcLocalRotateTo(this Transform transform, Quaternion to, in NcTweenOptions options)
        {
            return NcTween.To(() => transform.localRotation, v => transform.localRotation = v, to, options);
        }
    }
}
