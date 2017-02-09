using UnityEngine;
using System.Collections;

public class BirdMovement : MonoBehaviour {

    public float rotation_amount;
    //public float timer;
    public float speed = 1;
    
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //timer += Time.deltaTime;

        transform.Rotate(new Vector3(0,rotation_amount, 0));
        transform.Translate(Vector3.forward *speed);
	}
}
