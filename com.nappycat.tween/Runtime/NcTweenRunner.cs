using UnityEngine;

namespace NappyCat.Tween
{
    /// <summary>
    /// Single hidden runner that drives NcTween.Tick(). Can be persistent or scene-local.
    /// </summary>
    [DefaultExecutionOrder(-32000)]
    public sealed class NcTweenRunner : MonoBehaviour
    {
        [Tooltip("If true, the runner persists across scene loads (DontDestroyOnLoad).")]
        public bool persistentMode = true;

        private static NcTweenRunner _instance;
        public static NcTweenRunner Instance
        {
            get
            {
                if (_instance == null)
                {
                    var obj = new GameObject("~NcTweenRunner");
                    obj.hideFlags = HideFlags.HideAndDontSave;
                    _instance = obj.AddComponent<NcTweenRunner>();
                }
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;

            if (gameObject.name != "~NcTweenRunner")
            {
                gameObject.name = "~NcTweenRunner";
                gameObject.hideFlags = HideFlags.HideAndDontSave;
            }

            if (persistentMode)
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        void Update()
        {
            NcTween.Tick(Time.deltaTime, Time.unscaledDeltaTime);
        }
    }
}
