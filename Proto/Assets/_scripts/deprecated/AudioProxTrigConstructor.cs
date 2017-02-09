using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioProxTrigConstructor : MonoBehaviour {

    public AudioReverbVolume[] volumes;
    public AudioProximityTrigger[] triggers;
    public AudioMixer mixer;
    
    // Use this for initialization
	void Start () {
        volumes = FindObjectsOfType<AudioReverbVolume>();
        for (int i = 0; i < volumes.Length; i++)
        {
            gameObject.AddComponent<AudioProximityTrigger>();
            triggers = gameObject.GetComponents<AudioProximityTrigger>();
            triggers[i].targetverb = volumes[i];
            triggers[i].mixer = mixer;
            triggers[i].send_name = volumes[i].send_name;
        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
