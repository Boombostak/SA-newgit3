using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioProximityTrigger : MonoBehaviour {

    public AudioReverbVolume targetverb;
    public string send_name;
    public float sendlevel;
    public AudioMixer mixer;
    public float sqrmag;
    public float distance;
    
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        MixReverb(this.transform.position, targetverb.transform.position, send_name);
	}

    void MixReverb(Vector3 origin, Vector3 target, string send_name)
    {
        
        sqrmag = Vector3.SqrMagnitude(target - origin);
        distance = sqrmag - Mathf.Pow(targetverb.bounds, 2);
        if (distance<0)
        {
            distance = 0;
        }
        //Debug.Log("Distance is" + distance);
        sendlevel = Mathf.Clamp((Mathf.Log(1f/distance)*20f),-80,0);
        //Debug.Log("SendLevel is" + sendlevel);
        mixer.SetFloat(send_name, sendlevel);
    }
}
