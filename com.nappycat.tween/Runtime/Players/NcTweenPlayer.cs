/*
* NAPPY CAT
*
* Copyright © 2025 Nappy Cat Games
* http://nappycat.net
*
* Author: Stan Nesi
*
* File: Packages/com.NappyCat.Tween/Runtime/OptionalPresets/NcTweenPlayer.cs
* Created: 2024-06-19
*/

using UnityEngine;

namespace NappyCat.Tween
{
    /// <summary>Optional tiny player for NcPresetLite. No per-play allocations beyond delegates.</summary>
    public sealed class NcTweenPlayer : MonoBehaviour
    {
        public NcPresetLite preset;
        public bool playOnEnable = true;
        TweenHandle _handle;

        void OnEnable()
        {
            if (playOnEnable)
            {
                Play();
            }
        }

        public void Play()
        {
            var opt = new NcTweenOptions(preset.duration, preset.delay, preset.ease);
            switch (preset.kind)
            {
                case NcPresetLite.Kind.Move:
                    _handle = NcTween.To(() => transform.position, v => transform.position = v, preset.v3, opt);
                    break;
                case NcPresetLite.Kind.LocalScale:
                    _handle = NcTween.To(() => transform.localScale, v => transform.localScale = v, preset.v3, opt);
                    break;
                case NcPresetLite.Kind.Rotate:
                    _handle = NcTween.To(() => transform.rotation, q => transform.rotation = q, Quaternion.Euler(preset.v3), opt);
                    break;
                case NcPresetLite.Kind.CanvasAlpha:
                    {
                        var cg = GetComponent<CanvasGroup>();
                        if (cg)
                            _handle = NcTween.To(() => cg.alpha, a => cg.alpha = a, preset.v3.x, opt);
                        break;
                    }
                
                case NcPresetLite.Kind.SpriteColor:
                    {
                        var sr = GetComponent<SpriteRenderer>();

                        if (sr)
                            _handle = NcTween.To(() => sr.color, c => sr.color = c, preset.color, opt);
                        break;
                    }
            }
        }

        public void Stop(bool complete)
        {
            NcTween.Kill(_handle, complete);
            _handle = default;
        }
    }
}
