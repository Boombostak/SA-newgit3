using UnityEngine;
using System.Collections;

public class RandomColor : MonoBehaviour {

    public Color randomColor;
    
    // Use this for initialization
    void Start()
    {
        randomColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 0.60f);
        this.GetComponent<MeshRenderer>().material.SetColor("_Color", randomColor);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
