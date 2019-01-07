using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.Audio;
using System.Collections.Generic;
using System;

public enum repeatOptions
{
	Daily=0,
	Weekly=1,
	Monthly=2,
	Yearly=3,
	NoRepeat=4
}

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
	bool isHijri;
	public string calendarType = "Gregorian";
	public string targetDateTimeString;
	public DateTime targetDateTime;
	public float sunY;
	public float moonY;
	public bool repeatDaily;
	public bool repeatWeekly;
	public bool repeatMonthly;
	public bool repeatYearly;
	public repeatOptions op;


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
		else {
			Debug.Log ("Invalid DateTime!");
		}
		if (isHijri == false) {
			calendarType = "Gregorian";
		} 
		else {
			calendarType = "Hijri";
		}
		isHijri = EditorGUILayout.Toggle ("Hijri Calendar?", isHijri);
		EditorGUILayout.SelectableLabel ("Calendar type... " + calendarType.ToString());
		op = (repeatOptions)EditorGUILayout.EnumPopup ("Repeat type", op);
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

	void SetRepeatType(repeatOptions op){
		switch (op) {
		case repeatOptions.Daily:
			repeatDaily = true;
			repeatWeekly = false;
			repeatMonthly = false;
			repeatYearly = false;
			break;
		case repeatOptions.Weekly:
			repeatDaily = false;
			repeatWeekly = true;
			repeatMonthly = false;
			repeatYearly = false;
			break;
		case repeatOptions.Monthly:
			repeatDaily = false;
			repeatWeekly = false;
			repeatMonthly = true;
			repeatYearly = false;
			break;
		case repeatOptions.Yearly:
			repeatDaily = false;
			repeatWeekly = false;
			repeatMonthly = false;
			repeatYearly = true;
			break;
		case repeatOptions.NoRepeat:
			repeatDaily = false;
			repeatWeekly = false;
			repeatMonthly = false;
			repeatYearly = false;
			break;
		default:
			Debug.Log ("Unrecognized option in SetRepeatType");
			break;
		}
	}
}