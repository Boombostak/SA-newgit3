using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ReverbCalculator))]
public class ReverbCalculatorEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ReverbCalculator myScript = (ReverbCalculator)target;
        if (GUILayout.Button("Build Object"))
        {
            myScript.SetUp();
        }

        if (GUILayout.Button("Set these snapshots to static WARNING"))
        {
            myScript.SetMaster();
        }

        if (GUILayout.Button("Get these snapshots from static WARNING"))
        {
            myScript.GetMaster();
        }
    }
}
