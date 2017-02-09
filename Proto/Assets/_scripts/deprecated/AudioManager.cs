using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public List<AudioReverbVolume> volumelist;
    public AudioReverbVolume[] volumearray;
    public GameObject verbClosestToPlayer;
    
    // Use this for initialization
	void Start () {
        volumelist = new List<AudioReverbVolume>();
        volumearray = new AudioReverbVolume[FindObjectsOfType<AudioReverbVolume>().Length];
        volumearray = FindObjectsOfType<AudioReverbVolume>();
        
        
        for (int i = 0; i < volumearray.Length; i++)
        {
            volumelist.Add(volumearray[i]);
        }
	}
	
	// Update is called once per frame
	void Update () {
	volumelist.Sort(delegate(AudioReverbVolume v1, AudioReverbVolume v2){
        return v1.distanceToPlayer.CompareTo(v2.distanceToPlayer);
    });
    verbClosestToPlayer = volumelist[0].gameObject;

}
	}
