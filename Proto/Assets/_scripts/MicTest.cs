using UnityEngine;
using System.Collections;

public class MicTest : MonoBehaviour {

    public string[] devices;
    public static string device;
    //public IEnumerator RequestAuthorize();
    public bool authorization = false;
    public AudioClip clip;

	// Use this for initialization
	void Start () {
        StartCoroutine("RequestAuthorize");
	}

    IEnumerator RequestAuthorize()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.Microphone);
    }

    void Update()
    {
        if (Application.HasUserAuthorization(UserAuthorization.Microphone) && !authorization)
        {
            InitMic();
            StopCoroutine("RequestAuthorize");
        }
    }

    void InitMic()
    {
        devices = new string[Microphone.devices.Length];
        //Debug.Log(Microphone.devices.Length);
        devices = Microphone.devices;
        for (int i = 0; i < devices.Length; i++)
        {
            Debug.Log(devices[i]);
        }
		if (devices.Length!=0) {
			device = devices[0];
			authorization = true;
		}
        
    }
}
