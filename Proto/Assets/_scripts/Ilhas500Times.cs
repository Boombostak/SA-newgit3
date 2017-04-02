using UnityEngine;
using System.Collections;

public class Ilhas500Times : MonoBehaviour {

	public AdhanController adhanController;
	public bool isPlaying;
	public ClipRandomizer clipRandomizer;
	public AudioSource audioSource;
	public SimpleCalendar calendar;
	public int ilhasCountdown = 500;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (adhanController.fajrWasCalled && !adhanController.dhuhrWasCalled && !isPlaying && adhanController.adhanSource.isPlaying == false && ilhasCountdown >= 1) {
			clipRandomizer.PlayAClip ();
			ilhasCountdown--;
		}
		if (adhanController.dhuhrWasCalled) {
			ilhasCountdown = 500;
		}
		if (audioSource.isPlaying) {
			isPlaying = true;
		} 
		else {
			isPlaying = false;
		}
	}
}
