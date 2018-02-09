using UnityEngine;
using System.Collections;
using uCPf;

public class PreviewColor : MonoBehaviour {
	public GameObject colorPicker;
	public Renderer renderer;

	public void MatchColor(){
		renderer.material.color =
			colorPicker.GetComponent<ColorPicker> ().color;
	}
}
