using UnityEngine;
using NappyCat.Tween;

namespace NappyCat.Tween.Samples
{
    [AddComponentMenu("Nappy Cat/Tween/Samples/NcPlayground")]
    public class NcPlayground : MonoBehaviour
    {
        public Transform target;
        public Vector3 moveTo = new Vector3(0f, 2f, 0f);
        public Vector3 scaleTo = new Vector3(1.2f, 1.2f, 1.2f);
        public Quaternion rotateTo = Quaternion.Euler(0f, 180f, 0f);

        void OnGUI()
        {
            if (!target)
                return;

            GUILayout.BeginArea(new Rect(10, 10, 200, 200), GUI.skin.box);
            if (GUILayout.Button("Move"))
                target.NcMoveTo(target.position + moveTo, 0.35f, new NcTweenOptions(0.35f).WithGroup(1));
            if (GUILayout.Button("Scale"))
                target.NcLocalScaleTo(scaleTo, 0.35f, new NcTweenOptions(0.35f).WithGroup(1));
            if (GUILayout.Button("Rotate"))
                target.NcRotateTo(rotateTo, 0.35f, new NcTweenOptions(0.35f).WithGroup(1));
            if (GUILayout.Button("Pause Group 1")) NcTween.PauseGroup(1, true);
            if (GUILayout.Button("Kill Group 1")) NcTween.KillGroup(1, false);
            GUILayout.Label($"Active: {NcTween.ActiveCount}");
            GUILayout.EndArea();
        }
    }
}