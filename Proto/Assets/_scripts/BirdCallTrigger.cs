using UnityEngine;
using System.Collections;

public class BirdCallTrigger : MonoBehaviour {

    public float countup;
    public float basedelay;
    public AudioSource audiosource;
    public float pitchoffset;
    public float delayoffset;
    public float volumeoffset;
    public float currentdelay;

    void Awake()
    {
        audiosource = this.gameObject.GetComponent<AudioSource>();
        currentdelay = basedelay + Random.Range(-delayoffset, delayoffset);
    }
	
	// Update is called once per frame
	void Update () {
        countup += Time.deltaTime;
        if (countup>=currentdelay)
        {
            PlayBirdCall();
        }
	}

    public void PlayBirdCall()
    {
        audiosource.pitch = 1 + Random.Range(-pitchoffset, pitchoffset);
        audiosource.volume = 0.5f + Random.Range(-volumeoffset, volumeoffset);
        audiosource.Play();
        countup = 0;
        currentdelay = basedelay + Random.Range(-delayoffset, delayoffset);
    }
}
