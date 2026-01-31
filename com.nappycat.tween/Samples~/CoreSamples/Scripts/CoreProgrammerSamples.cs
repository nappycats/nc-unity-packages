using UnityEngine;
using NappyCat.Tween;

namespace NappyCat.Tween.Samples
{
    public sealed class CoreProgrammerSamples : MonoBehaviour
    {
        public Transform target;
        void Start()
        {
            NcTween.EnsureRunner();
            var handle = transform.NcMoveTo(target ? target.position : transform.position + Vector3.right, 0.35f);
            NcTween.OnComplete(handle, () => Debug.Log("NcTween: move done"));

            new NcSequence()
                .Then(() => transform.NcMoveTo(transform.position + new Vector3(1f, 0f, 0f), 0.25f))
                .Also(() =>
                {
#if NC_UNITY_UGUI
                var img = GetComponent<UnityEngine.UI.Image>();
                return img ? img.NcFillTo(1f, 0.25f) : default;
#else
                    return default;
#endif
                })
                .Then(() => transform.NcRotateTo(Quaternion.Euler(0f, 180f, 0f), 0.2f))
                .OnComplete(() => Debug.Log("NcSequence finished"))
                .Play();
        }
    }
}
