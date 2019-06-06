using UnityEngine;
using UnityEditor;

public class ColorMaterial_Editor : EditorWindow {

    Color color;

    [MenuItem("Window/Color Material")]
    public static void ShowWindow()
    {
        GetWindow<ColorMaterial_Editor>("Color Material");
    }

    void OnGUI()
    {
        GUILayout.Label("Color the selected objects:", EditorStyles.boldLabel);

        color = EditorGUILayout.ColorField("Color", color);

        if (GUILayout.Button("Apply"))
        {
            Colorize();
        }
    }

    void Colorize()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.sharedMaterial.color = color;
            }
        }
    }
}
