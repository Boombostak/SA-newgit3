using UnityEngine;
using System.Collections;

public class HatipHafiz : MonoBehaviour {

	public AdhanController adhanController;
	public bool isPlaying;
	public ClipRandomizer clipRandomizer;
	public AudioSource audioSource;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (adhanController.asrWasCalled && !isPlaying) {
			clipRandomizer.PlayAClipWithRandomDelay ();
			isPlaying = true;
		}
		if (adhanController.maghribWasCalled && !isPlaying) {
			clipRandomizer.PlayAClipWithRandomDelay ();
			isPlaying = true;
		}
		if (!audioSource.isPlaying) {
			isPlaying = false;
		}
	}
}
