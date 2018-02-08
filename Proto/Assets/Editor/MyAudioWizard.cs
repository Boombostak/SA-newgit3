using UnityEngine;
using UnityEditor;
using System.Collections;

public class MyAudioWizard : AudioWizard {
	string myString = "Empty";
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
}
