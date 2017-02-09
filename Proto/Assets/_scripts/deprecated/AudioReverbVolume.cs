using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioReverbVolume : MonoBehaviour {

    public AudioMixer mixer;
    public string send_name;
    public float bounds;
    public float distanceToPlayer;
    public GameObject player;
    public float sqrmag;
    
    // Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        
	}
	
	// Update is called once per frame
	void Update () {
        //put this in a coroutine
        sqrmag = Vector3.SqrMagnitude(player.transform.position - this.transform.position);
        distanceToPlayer = Mathf.Abs(sqrmag - Mathf.Pow(sqrmag - bounds, 2));
	}
}
