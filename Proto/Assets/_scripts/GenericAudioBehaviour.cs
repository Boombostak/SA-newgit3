using UnityEngine;
using System.Collections;
using System;

public class GenericAudioBehaviour : MonoBehaviour {

	//calendars are updated by SimpleCalendar

	public GameObject sun;
	public GameObject moon;
	public bool sunBelowHorizon;
	public bool moonBelowHorizon;
	public TOD_Sky sky;
	public DateTime gregorianDateTime;
	public string gregorianDateTimeString;
	public DateTime hijriDateTime;
	public string hijriDateTimeString;
	public bool useHijri;
	public DateTime targetDateTime;
	public bool clipHasPlayed;
	//public ReverNodeZoneDetectorForListener RNZDFL;
	//public AudioLowPassFilter filter;
	//public AudioSource[] sourcesToMute;

	//public bool fadeOutOtherSound = false;
	//public float fadeTime;

	/*Tracks and allows triggering based on
	 * Sun angle
	 * Moon Angle
	 * Moonvisibility(?)
	 * DateTime (day of year, day of month, day of week, hour)
	 * Conditional relationships between all
	*/

	// Use this for initialization
	void Start () {
		//filter = this.GetComponent<AudioLowPassFilter> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Determine which reverb zones to use and apply lowpass filter.
		/*
		if (RNZDFL == null) {
			RNZDFL = GameObject.FindObjectOfType<ReverNodeZoneDetectorForListener> ();
		}
		if (RNZDFL.playerIsInside) {
			filter.enabled = true;
		} else {
			filter.enabled = false;
		}
		*/

		//is the sun visible?
		if (sun.transform.position.y < -3) {
			sunBelowHorizon = true;
		} else {
			sunBelowHorizon = false;
		}

		//is the moon visible?
		if (moon.transform.position.y < -3) {
			moonBelowHorizon = true;
		} else {
			moonBelowHorizon = false;
		}
	}
}
