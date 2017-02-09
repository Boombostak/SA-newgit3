using UnityEngine;
using System.Collections;

public class NightLightController : MonoBehaviour {

	public Light[] nightLights;
	public TOD_Sky sky;

	// Use this for initialization
	void Start () {
		nightLights = this.gameObject.GetComponentsInChildren<Light> ();
		sky = GameObject.FindObjectOfType<TOD_Sky>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!sky.IsDay) {
			for (int i = 0; i < nightLights.Length; i++) {
				nightLights [i].enabled = true;
			}
			}
			else {
			for (int i = 0; i < nightLights.Length; i++) {
				nightLights [i].enabled = false;
			}
			}
		}
	}

