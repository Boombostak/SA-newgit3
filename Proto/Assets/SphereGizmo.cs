using UnityEngine;
using System.Collections;

public class SphereGizmo : MonoBehaviour {


	void OnDrawGizmos(){
		Gizmos.color = new Color (1f, 1f, 1f, 0.8f);
		Gizmos.DrawSphere (this.transform.position, 1);
	}
}
