/*using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.Audio;

//class to create and automate complex audio objects

public class CreateAudioWizard : ScriptableWizard
{

	public AudioSource audioSource;
	public string nickname = "Default nickname";
	public Camera editorCamera;

	[MenuItem ("My Tools/Create Audio Wizard...")]
	static void CreateWizard()
	{
		ScriptableWizard.DisplayWizard<CreateAudioWizard> ("Create Audio Object", "Create new", "Update selected");
	}

	void OnWizardCreate()
	{
		GameObject audioGO = new GameObject ();
		Instantiate (audioGo, editorCamera.transform.position, editorCamera.transform.rotation);
		AudioSource audioSourceComponent = audioGO.AddComponent<AudioSource> ();
		audioSourceComponent.name = nickname;
	
		audioGO.transform.position.Set (editorCamera.transform.position.x,editorCamera.transform.position.y,editorCamera.transform.position.z);
	}

	void OnWizardOtherButton()
	{
		AudioSource aSource = audioSource;
		if (Selection.activeTransform != null)
		{
			AudioSource audioSourceComponent = Selection.activeTransform.GetComponent<AudioSource>();

			if (audioSourceComponent != null)
			{
				audioSourceComponent = aSource;
				audioSourceComponent.name = nickname;
			}
		}
	}

	void OnWizardUpdate(){
		editorCamera = SceneView.lastActiveSceneView.camera;
	}

	void OnDrawGizmos(){
		editorCamera = SceneView.lastActiveSceneView.camera;
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere ((editorCamera.transform.position),1);
	}

	/*void OnGUI(){
		//editorCameraTransform = SceneView.currentDrawingSceneView.camera.transform;
		//editorCameraTransform = SceneView.lastActiveSceneView.camera.transform;
	}*/
//}






/*DEPRECATED
	 * 
	 * string myString = "Empty";
	AudioClip myAudioClip;
	Color myColor;
	Transform editorCameraTransform;


	[MenuItem("Window/AudioWizard")]
	public static void ShowAudioWizard(){
		EditorWindow.GetWindow (typeof(MyAudioWizard));
	}
	void OnGUI(){
		GUILayout.Label ("Settings", EditorStyles.boldLabel);
		myString = EditorGUILayout.TextField ("Name", myString);
		myAudioClip= EditorGUILayout.ObjectField (
			"AudioClip", 
			myAudioClip, 
			typeof(AudioClip), 
			false, 
			null) as AudioClip;
		myColor = EditorGUILayout.ColorField (myColor);
		editorCameraTransform = SceneView.lastActiveSceneView.camera.transform;
	}
	void OnDrawGizmos(){
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere ((editorCameraTransform.position),1);
	}

	[MenuItem("GameObject/Create Audio Wizard")]
	static void CreateAudioWizard()
	{
		ScriptableWizard.DisplayWizard<MyAudioWizard>("Create AUdio Object", "Create", "Apply");
	}

	void OnWizardCreate()
	{
		GameObject go = new GameObject("New Audio Object");
	}
}*/
