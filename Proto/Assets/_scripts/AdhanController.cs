using UnityEngine;
using System.Collections;

public class AdhanController : MonoBehaviour {

    public GameObject sun;
    public bool sunBelowHorizon;
	public AudioSource adhanSource;
	public AudioClip[] adhanClips;
	public AudioClip[] fajrClips;
    public TOD_Sky sky;
	public ReverNodeZoneDetectorForListener RNZDFL;
	public AudioLowPassFilter filter;
	public AudioSource[] sourcesToMute;

    public bool fajrWasCalled = false;
    public bool dhuhrWasCalled = false;
    public bool asrWasCalled = false;
    public bool maghribWasCalled = false;
    public bool ishaWasCalled = false;

	public bool fadeOutOtherSound = false;
	public float fadeTime;

    // Use this for initialization
    void Start () {
		RNZDFL = GameObject.FindObjectOfType<ReverNodeZoneDetectorForListener> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (RNZDFL == null) {
			RNZDFL = GameObject.FindObjectOfType<ReverNodeZoneDetectorForListener> ();
		}
		if (RNZDFL.playerIsInside) {
			filter.enabled = true;
		} else {
			filter.enabled = false;
		}

		//is the sun visible?
		if (sun.transform.position.y < -3) {
			sunBelowHorizon = true;
		} else {
			sunBelowHorizon = false;
		}

		//is it just before sunrise?
		if (sunBelowHorizon == false && sky.Cycle.Hour < 12 && fajrWasCalled == false) {
			CallToPrayer ();
			fajrWasCalled = true;
		}
        
		//is it noon?
		if (sky.Cycle.Hour >= 12 && dhuhrWasCalled == false) {
			CallToPrayer ();
			fajrWasCalled = true;
			dhuhrWasCalled = true;
		}

		//is it afternoon?
		if (sky.Cycle.Hour >= 15 && asrWasCalled == false) {
			CallToPrayer ();
			fajrWasCalled = true;
			asrWasCalled = true;
		}

		if (sunBelowHorizon == true && maghribWasCalled == false && sky.Cycle.Hour >= 12) {
			CallToPrayer ();
			fajrWasCalled = true;
			maghribWasCalled = true;
		}

		if (sky.Cycle.Hour >= 22 && ishaWasCalled == false) {
			CallToPrayer (); 
			fajrWasCalled = true;
			ishaWasCalled = true;
		}

		//reset at midnght
		if (sky.Cycle.Hour < 1) {
			fajrWasCalled = false;
			dhuhrWasCalled = false;
			asrWasCalled = false;
			maghribWasCalled = false;
			ishaWasCalled = false;
		}
		//fade other audio when adhan happens
		if (fadeOutOtherSound) {
			for (int i = 0; i < sourcesToMute.Length; i++) {
				if (sourcesToMute [i].volume >= 0) {
					sourcesToMute [i].volume -= fadeTime * Time.deltaTime;
				}
				if (sourcesToMute[i].volume <= 0) {
					sourcesToMute [i].Stop();
					sourcesToMute [i].volume = 1f;
					fadeOutOtherSound = false;
				}
			}
		}
	}

    void CallToPrayer ()
	{
		fadeOutOtherSound = true;

		if (!fajrWasCalled) {
			adhanSource.clip = fajrClips [Random.Range (0, fajrClips.Length)];
			adhanSource.Play ();
			fajrWasCalled = true;
		} else {
			adhanSource.clip = adhanClips [Random.Range (0, adhanClips.Length)];
			adhanSource.Play ();
		}
	}
}
