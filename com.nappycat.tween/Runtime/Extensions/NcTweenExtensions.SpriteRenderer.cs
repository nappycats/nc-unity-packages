using UnityEngine;

namespace NappyCat.Tween
{
    /// <summary>SpriteRenderer tween helpers.</summary>
    public static partial class NcTweenExtensions
    {
        public static TweenHandle NcColorTo(this SpriteRenderer renderer, Color to, float duration)
        {
            return NcTween.To(() => renderer.color, v => renderer.color = v, to, new NcTweenOptions(duration));
        }

        public static TweenHandle NcColorTo(this SpriteRenderer renderer, Color to, in NcTweenOptions options)
        {
            return NcTween.To(() => renderer.color, v => renderer.color = v, to, options);
        }

        public static TweenHandle NcAlphaTo(this SpriteRenderer renderer, float to, float duration)
        {
            return NcTween.To(() => renderer.color.a, a =>
            {
                var c = renderer.color;
                c.a = a;
                renderer.color = c;
            }, to, new NcTweenOptions(duration));
        }

        public static TweenHandle NcAlphaTo(this SpriteRenderer renderer, float to, in NcTweenOptions options)
        {
            return NcTween.To(() => renderer.color.a, a =>
            {
                var c = renderer.color;
                c.a = a;
                renderer.color = c;
            }, to, options);
        }
    }
}
