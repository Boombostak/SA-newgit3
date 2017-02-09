using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class ReverbToPlayer : MonoBehaviour {

	public AudioReverbFilter nodeFilter;
	public AudioReverbFilter playerFilter;
	public Transform[] verbnodes;
	public GameObject closestNode;


	// Use this for initialization
	void Start () {
		playerFilter = GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<AudioReverbFilter>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		closestNode = GetClosestNode (verbnodes).gameObject;
		playerFilter = closestNode.GetComponent<AudioReverbFilter> ();
	}

	Transform GetClosestNode(Transform[] nodes)
	{
		Transform tMin = null;
		float minDist = Mathf.Infinity;
		Vector3 currentPos = playerFilter.gameObject.transform.position;
		foreach (Transform t in nodes)
		{
			float dist = Vector3.Distance(t.position, currentPos);
			if (dist < minDist)
			{
				tMin = t;
				minDist = dist;
			}
		}
		return tMin;
	}
}
