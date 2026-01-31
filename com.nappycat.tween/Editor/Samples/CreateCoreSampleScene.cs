#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using NappyCat.Tween;

namespace NappyCat.Tween.Samples
{
public class SimplePlayground : MonoBehaviour
{
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 200, 150), GUI.skin.box);
        GUILayout.Label("NcTween Playground");
        var opt = new NcTweenOptions(0.35f);
        if (GUILayout.Button("Move"))
            NcTween.To(() => transform.position, v => transform.position = v, transform.position + Vector3.up * 2f, opt);
        if (GUILayout.Button("Scale"))
            NcTween.To(() => transform.localScale, v => transform.localScale = v, Vector3.one * 1.5f, opt);
        if (GUILayout.Button("Rotate"))
            NcTween.To(() => transform.rotation, v => transform.rotation = v, Quaternion.Euler(0f, 180f, 0f), opt);
        GUILayout.EndArea();
    }
}

public static class NappyCatSampleFactory
{
    [MenuItem("Create/Nappy Cat/Playground Scene")]
    public static void CreateScene()
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
        scene.name = "NcTweenPlayground";

        var cam = Camera.main ? Camera.main.gameObject : new GameObject("Main Camera", typeof(Camera));
        if (!Camera.main) cam.tag = "MainCamera";
        cam.transform.position = new Vector3(0f, 1.5f, -6f);
        cam.transform.LookAt(Vector3.zero);

        var light = Object.FindFirstObjectByType<Light>();
        
        if (!light)
        {
            var lgo = new GameObject("Directional Light", typeof(Light));
            light = lgo.GetComponent<Light>();
            light.type = LightType.Directional;
            light.transform.rotation = Quaternion.Euler(50f, -30f, 0f);
        }

        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.name = "Target";
        cube.transform.position = Vector3.zero;
        cube.AddComponent<SimplePlayground>();

        EditorSceneManager.MarkSceneDirty(scene);
    }
}
}
#endif
