using UnityEngine;
using UnityEditor;
using System.Collections;

public class MyAudioWizard : AudioWizard {
	[MenuItem("Window/AudioWizard")]
	public static void ShowAudioWizard(){
		EditorWindow.GetWindow (typeof(MyAudioWizard));
	}
	void OnGUI(){
	}
}
