using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Spline_Controller))]
[CanEditMultipleObjects]
public class Spline_Editor : Editor {

    public override void OnInspectorGUI()
    {
        Spline_Controller splineController = (Spline_Controller)target;

        EditorGUILayout.LabelField("Spline Settings:", EditorStyles.boldLabel);

        splineController.autoClose = EditorGUILayout.Toggle("Auto Close: ", splineController.autoClose);
        splineController.autoLoop = EditorGUILayout.Toggle("Auto Loop: ", splineController.autoLoop);

        splineController.duration = EditorGUILayout.FloatField("Duration:", splineController.duration);

        splineController.rotationMode = (orientationMode)EditorGUILayout.EnumPopup("Rotation Mode:", splineController.rotationMode);
        if (splineController.rotationMode == orientationMode.Target)
        {
            splineController.lookAtTarget = (GameObject)EditorGUILayout.ObjectField("Target to Look at:", splineController.lookAtTarget, typeof(GameObject), true);
        }

        splineController.splineRoot = (GameObject)EditorGUILayout.ObjectField("Spline Nodes:", splineController.splineRoot, typeof(GameObject), true);

        EditorGUILayout.LabelField("", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Gizmo Settings:", EditorStyles.boldLabel);

        splineController.gizmoLineColor = EditorGUILayout.ColorField("Line Color:", splineController.gizmoLineColor);

        splineController.gizmoLineSamples = EditorGUILayout.IntField("Line Samples:", splineController.gizmoLineSamples);

        splineController.gizmoRotationSamples = EditorGUILayout.IntField("Rotation Samples:", splineController.gizmoRotationSamples);

        splineController.gizmoXAxis = EditorGUILayout.Toggle("Axis - X: ", splineController.gizmoXAxis);
        splineController.gizmoYAxis = EditorGUILayout.Toggle("Axis - Y: ", splineController.gizmoYAxis);
        splineController.gizmoZAxis = EditorGUILayout.Toggle("Axis - Z: ", splineController.gizmoZAxis);

        splineController.gizmoAxisLenght = EditorGUILayout.Slider("Axis Size:", splineController.gizmoAxisLenght, 1, 20);

    }
}
