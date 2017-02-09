using UnityEngine;
using System.Collections;

public class ReverbBake : MonoBehaviour {

	public GameObject sphereGO;
	public Mesh sphere;
	public Transform[] nodes;
	public GameObject nodesGO;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CalculateParameters(){
		LinecastToSphere ();
	}

	void LinecastToSphere ()
	{
		sphere = sphereGO.GetComponent<MeshFilter> ().sharedMesh;
		nodes = nodesGO.GetComponentsInChildren<Transform> ();

		RaycastHit hit;
		float[] currentdistances;
		currentdistances = new float[sphere.vertexCount];
		for (int i = 0; i < nodes.Length; i++) {

			for (int j = 0; j < sphere.vertexCount; j++) {
				if (Physics.Linecast (nodes [i].position, sphere.vertices [j], out hit, LayerMask.NameToLayer ("reverbbakelayer"))) {
					Debug.Log ("hit");
					currentdistances [j] = hit.distance;
				}
				nodes [i].gameObject.GetComponent<ReverbBakeDistances> ().distances = new float[currentdistances.Length];
				nodes [i].gameObject.GetComponent<ReverbBakeDistances> ().distances = 
					currentdistances;


			}
		}
	}
}
