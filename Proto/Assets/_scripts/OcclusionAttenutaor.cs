using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//UNSTABLE!!!
public class OcclusionAttenutaor : MonoBehaviour {


	public AudioListener listener;
	public AudioLowPassFilter lopass;
	public List <AudioSource> sources;

	// Use this for initialization
	void Start () {
		sources.AddRange(GameObject.FindObjectsOfType<AudioSource> ());
	}
	
	// Update is called once per frame
	void Update () {
		if (sources!=null) {
			foreach (var source in sources) {
				if (source.isPlaying) {
					if (!Physics.Linecast(listener.transform.position,source.transform.position)) {
						CalculateOcclusion (source);
					}
				}	
			}
		}

	}

	public void CalculateOcclusion(AudioSource source){
		RaycastHit hitFromListener;
		Vector3 listenerImpact;
		RaycastHit hitfromSource;
		Vector3 sourceImpact;
		float distanceBetweenImpacts = 0;


		if (Physics.Linecast (listener.transform.position, source.transform.position, out hitFromListener)) {
			listenerImpact = hitFromListener.point;
		}
		if (Physics.Linecast (source.transform.position, listener.transform.position, out hitfromSource)) {
			sourceImpact = hitfromSource.point;
		}
		
			distanceBetweenImpacts = Vector3.Distance (hitFromListener.transform.position, hitfromSource.transform.position);

			lopass.cutoffFrequency = distanceBetweenImpacts / 0.125f;
		}
	}

