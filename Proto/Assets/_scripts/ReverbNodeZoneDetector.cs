using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class ReverbNodeZoneDetector : MonoBehaviour {

	//PUT THIS ON THINGS THAT MAKE SOUNDS

	public GameObject target;
	public ReverbNodeZone current_rnz;
	public ReverbNodeZone previous_rnz;
	public List<GameObject> activeNodes;
	public ReverbController targetController;
	// Use this for initialization
	void Start () {
		target = this.gameObject;
		targetController = target.GetComponent<ReverbController> ();
	}
	
	/*void OnTriggerStay(Collider other){
		Debug.Log ("triggerstay");
		rnz = other.gameObject.GetComponent<ReverbNodeZone> ();
		//activeNodes = new GameObject[rnz.nodeList.Count];
		activeNodes = rnz.nodeList;
	}*/
	void OnTriggerEnter(Collider other){
		//Debug.Log ("triggerenter");
		current_rnz = other.gameObject.GetComponent<ReverbNodeZone> ();
		//activeNodes = new GameObject[rnz.nodeList.Count];
		if (current_rnz.isExclusive) {
			activeNodes.Clear ();
		}
		activeNodes.AddRange(current_rnz.nodeList);
		targetController.nodes = activeNodes.ToArray();

	}
	void OnTriggerExit(Collider other){
		current_rnz = other.gameObject.GetComponent<ReverbNodeZone> ();
		foreach (var node in current_rnz.nodeList) {
			activeNodes.Remove (node);
		}
		targetController.nodes = activeNodes.ToArray();
		}

	}

