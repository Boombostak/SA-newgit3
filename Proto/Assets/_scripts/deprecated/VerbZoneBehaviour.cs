using UnityEngine;
using System.Collections;

public class VerbZoneBehaviour : MonoBehaviour
{

    public AudioReverbFilter target;
    public AudioReverbPreset filter;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("I am colliding with" + other.gameObject);
        target = other.gameObject.GetComponentInChildren<AudioReverbFilter>();
        Debug.Log("The target reverb filter is" + target.gameObject);
    }

    void OnTriggerStay(Collider other)
    {
        target.reverbPreset = filter;
    }

    void OnTriggerExit(Collider other)
    {
        target.reverbPreset = AudioReverbPreset.Off;
        target = null;
    }
}
