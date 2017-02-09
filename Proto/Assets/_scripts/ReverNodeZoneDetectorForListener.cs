using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(Rigidbody))]
public class ReverNodeZoneDetectorForListener : MonoBehaviour {

	//PUT THIS ON LISTENER

	public GameObject target;
	public ReverbNodeZone rnz;
	public List<GameObject> activeNodes;
	public ReverbControllerForListener targetController;
	public List<ReverbNodeZone> zonesCurrentlyInsideOf;
	public bool playerIsInside;
	public bool playerIsOutside;
	public ReverbControllerForListener RCFL;
	// Use this for initialization
	void Start () {
		target = this.gameObject;
		targetController = target.GetComponent<ReverbControllerForListener> ();
		targetController.nodes = activeNodes.ToArray ();
	}

	/*void OnTriggerStay(Collider other){
		Debug.Log ("triggerstay");
		rnz = other.gameObject.GetComponent<ReverbNodeZone> ();
		//activeNodes = new GameObject[rnz.nodeList.Count];
		activeNodes = rnz.nodeList;
	}*/
	void OnTriggerEnter(Collider other){
		
		rnz = other.gameObject.GetComponent<ReverbNodeZone> ();
		zonesCurrentlyInsideOf.Insert(0,rnz);
		playerIsInside = rnz.inside;
		playerIsOutside = rnz.outside;
		if (rnz.isExclusive) {
			activeNodes = zonesCurrentlyInsideOf[0].nodeList;
		} else {
			activeNodes.AddRange (rnz.nodeList.Except (activeNodes));

			activeNodes.AddRange (rnz.nodeList);
			/*foreach (var node in rnz.associatedNodes) {
				activeNodes.Remove (node);
			}
		}
		if (rnz.linkedZones!=null) {
			foreach (var zone in rnz.linkedZones) {
				activeNodes.AddRange(zone.nodeList.Except(activeNodes));
				foreach (var node in rnz.associatedNodes) {
					activeNodes.Remove (node);
				}
			}
		}*/
			
		}
		targetController.nodes = activeNodes.ToArray ();
	}


	void OnTriggerExit(Collider other){
		
		rnz = other.gameObject.GetComponent<ReverbNodeZone> ();
		zonesCurrentlyInsideOf.Remove (rnz);
		/*if (rnz.linkedZones!=null) {
			foreach (var zone in zonesCurrentlyInsideOf) {
					if (rnz.linkedZones.Contains(zone)) {
						//do nothing
					} else {
					foreach (var node in rnz.nodeList) {
						activeNodes.Remove (node);
				}
				}
			}
		} else {
		//	*///foreach (var node in rnz.nodeList) {
		//activeNodes.Remove (node);
		activeNodes = zonesCurrentlyInsideOf[0].nodeList;



		targetController.nodes = activeNodes.ToArray ();
		if (zonesCurrentlyInsideOf.Count==1) {
			playerIsInside=false;
		}
		/*if (activeNodes.Count == 0) {
			activeNodes.AddRange (rnz.associatedNodes);
		}*/
	
		//this.transform.position+= Vector3.zero; //retrigger overlapping trigger colliders?
	}

}
