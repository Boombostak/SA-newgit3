using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReverbNodeZone : MonoBehaviour {

	public Collider activeCollider;
	public List<GameObject> nodeList;
	public static GameObject[] allNodes;
	public Collider[] colliders;
	public bool isExclusive;
	public List<GameObject> associatedNodes;
	public ReverbNodeZone[] linkedZones;
	public bool inside;
	public bool outside;

	//USE ONTRIGGERSTAY!!! Test if the player is staying in the trigger and adjust accordingly.

	//for loop if the node is inside the collider it goes into the array of nodes associated with this collider. 
	//If the player is inside the collider, the colliders node array becomes the players node array.


	public void CalculateAssociatedNodes(){
		int count;
		if (isExclusive) {
			nodeList.AddRange (associatedNodes);
		}
			if (!isExclusive) {
			for (int i = 0; i < allNodes.Length; i++) {

				if (activeCollider.bounds.Contains (allNodes [i].transform.position)
					&& !associatedNodes.Contains(allNodes [i])) {
					nodeList.Add (allNodes[i]);

				}
			}

			}
		}


	// Use this for initialization
	void Start () {
		activeCollider = this.GetComponent<Collider> ();
		allNodes = GameObject.FindGameObjectsWithTag("reverbnode");
		CalculateAssociatedNodes ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	}
}
