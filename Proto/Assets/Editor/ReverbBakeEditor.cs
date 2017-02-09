using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (ReverbBake))]
public class ReverbBakeEditor : Editor {

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		ReverbBake myScript = (ReverbBake)target;
		if(GUILayout.Button("Calculate Parameters"))
		{
			myScript.CalculateParameters();
		}
	}
}
