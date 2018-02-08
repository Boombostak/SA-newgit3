using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NameEntryUI : MonoBehaviour {

	public InputField inputField;
	public Canvas canvas;

	// Use this for initialization
	void Start () {
		inputField = this.GetComponentInChildren<InputField> ();
		inputField.Select ();
	}

	void Update(){
		if (inputField.isFocused == false) {
			inputField.Select ();
		}
		if (Input.GetKeyDown ("return")) {
			canvas.enabled = false;
		}
	}
}
