using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NameEntryUI : MonoBehaviour {

	public InputField inputField;

	// Use this for initialization
	void Start () {
		inputField = this.GetComponentInChildren<InputField> ();
		inputField.Select ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
