using UnityEngine;

namespace NappyCat.Tween
{
    /// <summary>Built-in easing functions. CustomCurve uses AnimationCurve.</summary>
    public enum NcEase
    {
        Linear, LinearAnti,
        AlmostIdentity, AlmostIdentityAnti,
        InQuad, OutQuad, InOutQuad,
        InCubic, OutCubic, InOutCubic,
        InQuart, OutQuart, InOutQuart,
        InQuint, OutQuint, InOutQuint,
        InSine, OutSine, InOutSine,
        InExpo, OutExpo, InOutExpo,
        InCirc, OutCirc, InOutCirc,
        InBack, OutBack, InOutBack,
        InElastic, OutElastic, InOutElastic,
        InBounce, OutBounce, InOutBounce,
        CustomCurve
    }

    /// <summary>Evaluator for NcEase values.</summary>
    public static class NcTweenEase
    {
        public static float Evaluate(NcEase ease, float t, AnimationCurve curve = null)
        {
            return Evaluate(ease, t, ref curve);
        }

        static float Evaluate(NcEase ease, float t, ref AnimationCurve curve)
        {
            switch (ease)
            {
                default:
                case NcEase.Linear: return t;
                case NcEase.LinearAnti: return 1f - t;

                case NcEase.AlmostIdentity: return t * t * (3f - 2f * t);
                case NcEase.AlmostIdentityAnti: return 1f - (1f - t) * (1f - t) * (3f - 2f * (1f - t));

                case NcEase.InQuad: return t * t;
                case NcEase.OutQuad: return t * (2f - t);
                case NcEase.InOutQuad: return t < 0.5f ? 2f * t * t : -1f + (4f - 2f * t) * t;

                case NcEase.InCubic: return t * t * t;
                case NcEase.OutCubic: t -= 1f; return t * t * t + 1f;
                case NcEase.InOutCubic: return t < 0.5f ? 4f * t * t * t : (t - 1f) * (2f * t - 2f) * (2f * t - 2f) + 1f;

                case NcEase.InQuart: return t * t * t * t;
                case NcEase.OutQuart: t -= 1f; return 1f - t * t * t * t;
                case NcEase.InOutQuart: return t < 0.5f ? 8f * t * t * t * t : 1f - 8f * (t - 1f) * (t - 1f) * (t - 1f) * (t - 1f);

                case NcEase.InQuint: return t * t * t * t * t;
                case NcEase.OutQuint: t -= 1f; return 1f + t * t * t * t * t;
                case NcEase.InOutQuint: return t < 0.5f ? 16f * t * t * t * t * t : 1f + 16f * (t - 1f) * (t - 1f) * (t - 1f) * (t - 1f) * (t - 1f);

                case NcEase.InSine: return 1f - Mathf.Cos(t * Mathf.PI * 0.5f);
                case NcEase.OutSine: return Mathf.Sin(t * Mathf.PI * 0.5f);
                case NcEase.InOutSine: return 0.5f * (1f - Mathf.Cos(Mathf.PI * t));

                case NcEase.InExpo: return t <= 0f ? 0f : Mathf.Pow(2f, 10f * (t - 1f));
                case NcEase.OutExpo: return t >= 1f ? 1f : 1f - Mathf.Pow(2f, -10f * t);
                case NcEase.InOutExpo:
                    if (t <= 0f) return 0f;
                    if (t >= 1f) return 1f;
                    t *= 2f;
                    return t < 1f
                        ? 0.5f * Mathf.Pow(2f, 10f * (t - 1f))
                        : 0.5f * (2f - Mathf.Pow(2f, -10f * (t - 1f)));

                case NcEase.InCirc: return 1f - Mathf.Sqrt(1f - t * t);
                case NcEase.OutCirc: return Mathf.Sqrt(1f - (t - 1f) * (t - 1f));
                case NcEase.InOutCirc: return t < 0.5f
                    ? (1f - Mathf.Sqrt(1f - 4f * t * t)) * 0.5f
                    : (Mathf.Sqrt(1f - (2f * t - 2f) * (2f * t - 2f)) + 1f) * 0.5f;

                case NcEase.InBack: return t * t * (2.70158f * t - 1.70158f);
                case NcEase.OutBack: return 1f + (t -= 1f) * t * (2.70158f * t + 1.70158f);
                case NcEase.InOutBack:
                    {
                        float s = 1.70158f * 1.525f;
                        t *= 2f;
                        return t < 1f
                            ? 0.5f * (t * t * ((s + 1f) * t - s))
                            : 0.5f * ((t -= 2f) * t * ((s + 1f) * t + s) + 2f);
                    }

                case NcEase.InElastic:
                    if (t == 0f || t == 1f) return t;
                    return -Mathf.Pow(2f, 10f * (t - 1f)) * Mathf.Sin((t - 1.075f) * (2f * Mathf.PI) / 0.3f);
                case NcEase.OutElastic:
                    if (t == 0f || t == 1f) return t;
                    return Mathf.Pow(2f, -10f * t) * Mathf.Sin((t - 0.075f) * (2f * Mathf.PI) / 0.3f) + 1f;
                case NcEase.InOutElastic:
                    if (t == 0f || t == 1f) return t;
                    t *= 2f;
                    return t < 1f
                        ? -0.5f * Mathf.Pow(2f, 10f * (t - 1f)) * Mathf.Sin((t - 1.1125f) * (2f * Mathf.PI) / 0.45f)
                        : Mathf.Pow(2f, -10f * (t - 1f)) * Mathf.Sin((t - 1.1125f) * (2f * Mathf.PI) / 0.45f) * 0.5f + 1f;

                case NcEase.InBounce: return 1f - Evaluate(NcEase.OutBounce, 1f - t, ref curve);
                case NcEase.OutBounce:
                    if (t < 1f / 2.75f) return 7.5625f * t * t;
                    if (t < 2f / 2.75f) { t -= 1.5f / 2.75f; return 7.5625f * t * t + 0.75f; }
                    if (t < 2.5f / 2.75f) { t -= 2.25f / 2.75f; return 7.5625f * t * t + 0.9375f; }
                    t -= 2.625f / 2.75f;
                    return 7.5625f * t * t + 0.984375f;
                case NcEase.InOutBounce:
                    return t < 0.5f
                        ? 0.5f * Evaluate(NcEase.InBounce, t * 2f, ref curve)
                        : 0.5f * Evaluate(NcEase.OutBounce, t * 2f - 1f, ref curve) + 0.5f;

                case NcEase.CustomCurve: return curve == null ? t : Mathf.Clamp01(curve.Evaluate(t));
            }
        }
    }
}
