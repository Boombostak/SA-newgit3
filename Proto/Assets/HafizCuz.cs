using UnityEngine;
using System.Collections;

public class HafizCuz : MonoBehaviour {

	public AdhanController adhanController;
	public bool isPlaying;
	public ClipRandomizer clipRandomizer;
	public AudioSource audioSource;
	public SimpleCalendar calendar;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (adhanController.ishaWasCalled && !isPlaying) {
			clipRandomizer.PlayAClipWithRandomDelay ();
			isPlaying = true;
		}
		if (!audioSource.isPlaying) {
			isPlaying = false;
		}
	}
}
