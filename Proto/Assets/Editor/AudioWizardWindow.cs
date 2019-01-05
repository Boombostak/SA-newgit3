using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.Audio;

public class AudioWizardWindow : EditorWindow {

	public GameObject obj =null;
	public GameObject tempObj = null;
	public Vector3 vec3 = new Vector3(0,0,0);
	public AudioClip audioClip;
	public string nickname = "Please name the audio object";
	public Camera editorCamera;
	bool DateTimeEnabled;
	bool SunAngleEnabled;
	bool MoonAngleEnabled;

		// Add menu named "My Window" to the Window menu
		[MenuItem("Window/AudioWizardWindow")]
		static void Init()
		{
			// Get existing open window or if none, make a new one:
		AudioWizardWindow window = (AudioWizardWindow)EditorWindow.GetWindow(typeof(AudioWizardWindow));
			window.Show();
		}

		void OnGUI()
		{
		GUILayout.Label("Base Settings", EditorStyles.boldLabel);
		nickname = EditorGUILayout.TextField("Object Name", nickname);
		audioClip = (AudioClip)EditorGUILayout.ObjectField(audioClip,typeof(AudioClip),true);
		editorCamera = SceneView.lastActiveSceneView.camera;
		vec3 = EditorGUILayout.Vector3Field ("Placement Target", vec3);

		DateTimeEnabled = EditorGUILayout.BeginToggleGroup("Trigger By DateTime", DateTimeEnabled);
		EditorGUILayout.EndToggleGroup();

		SunAngleEnabled = EditorGUILayout.BeginToggleGroup("Trigger By Sun Angle", SunAngleEnabled);
		EditorGUILayout.EndToggleGroup();

		MoonAngleEnabled = EditorGUILayout.BeginToggleGroup("Trigger by Moon Angle", MoonAngleEnabled);
		EditorGUILayout.EndToggleGroup();

		if (GUILayout.Button ("Add to scene")) {
			tempObj = (GameObject)Instantiate (tempObj, vec3, Quaternion.identity);
			tempObj.AddComponent<AudioSource> ();
			tempObj.GetComponent<AudioSource> ().clip = audioClip;
			tempObj.name = nickname;
		}
		}

	void OnDrawGizmos(){
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere((vec3),1);
	}
	}