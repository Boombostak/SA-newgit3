


	using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(SkyController))]
public class SkyControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SkyController myScript = (SkyController)target;
        if (GUILayout.Button("Add Current Cloud Snapshot to Array"))
        {
            myScript.AddCloudSnapshot();
        }
    }
}