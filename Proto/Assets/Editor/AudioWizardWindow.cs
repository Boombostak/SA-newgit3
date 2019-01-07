using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.Audio;
using System.Collections.Generic;
using System;

public class AudioWizardWindow : EditorWindow {

	public GameObject GizmoGo = null;
	public GameObject obj =null;
	public GameObject tempObj = null;
	public AudioClip audioClip;
	public string nickname = "Please name the audio object";
	public Camera editorCamera;
	bool DateTimeEnabled;
	bool SunAngleEnabled;
	bool MoonAngleEnabled;
	public string targetDateTimeString;
	public DateTime targetDateTime;
	public float sunY;
	public float moonY;

		// Add menu named "My Window" to the Window menu
		[MenuItem("Window/AudioWizardWindow")]
		static void Init()
		{
			// Get existing open window or if none, make a new one:
		AudioWizardWindow window = (AudioWizardWindow)EditorWindow.GetWindow(typeof(AudioWizardWindow));
			window.Show();
		}

	public void Awake(){
		GizmoGo = new GameObject ();
		GizmoGo.name = "AudioWizardGizmoGO";
		Selection.activeGameObject = GizmoGo;
		GizmoGo.AddComponent(typeof(SphereGizmo));
		editorCamera = SceneView.lastActiveSceneView.camera;
		GizmoGo.transform.position = (editorCamera.transform.position + (editorCamera.transform.forward*3));

	}

		void OnGUI()
		{
		DateTime dateResult;

		EditorGUIUtility.labelWidth = 500;
		GUILayout.Label("Base Settings", EditorStyles.boldLabel);
		nickname = EditorGUILayout.TextField("Object Name", nickname);
		audioClip = (AudioClip)EditorGUILayout.ObjectField("Clip to trigger",audioClip,typeof(AudioClip),true);
		GizmoGo.transform.position = (Vector3)EditorGUILayout.Vector3Field ("Placement Target",GizmoGo.transform.position);



		DateTimeEnabled = EditorGUILayout.BeginToggleGroup("Trigger By DateTime", DateTimeEnabled);
		targetDateTimeString = EditorGUILayout.TextField ("Target Date/Time in format mm/dd/yyyy hh:mm:ss (24 hour clock)",targetDateTimeString);
		if (DateTime.TryParse (targetDateTimeString, out dateResult)) {
			targetDateTime = dateResult;
		}

		EditorGUILayout.EndToggleGroup();
		Debug.Log(targetDateTime.ToString());
		SunAngleEnabled = EditorGUILayout.BeginToggleGroup("Trigger By Sun Angle", SunAngleEnabled);
		EditorGUILayout.EndToggleGroup();

		MoonAngleEnabled = EditorGUILayout.BeginToggleGroup("Trigger by Moon Angle", MoonAngleEnabled);
		EditorGUILayout.EndToggleGroup();

		if (GUILayout.Button ("Add to scene")) {
			tempObj = new GameObject();
			tempObj.transform.position = GizmoGo.transform.position;
			tempObj.AddComponent<AudioSource> ();
			tempObj.GetComponent<AudioSource> ().clip = audioClip;
			tempObj.name = nickname;
		}
		}

	void OnDrawGizmos(){
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere((GizmoGo.transform.position),1);
	}

	void OnDestroy(){
		GameObject.DestroyImmediate (GizmoGo);
	}
	}