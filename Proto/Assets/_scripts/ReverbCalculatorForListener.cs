using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class ReverbCalculatorForListener : MonoBehaviour
{

    public AudioListener target;
    public AudioMixer mixer;
    public AudioMixerSnapshot[] snapshots;
    public GameObject[] nodes;
    public float[] closenesses;
    public static AudioMixerSnapshot[] masterSnapshots;
    public bool setUpHasRun = false;
    public bool AlsoHasAudioSource;

    // Use this for initialization
    void Start()
    {
		int nodeCount = GameObject.Find ("reverbManager").transform.FindChild ("nodesGO").childCount;
		nodes = new GameObject[nodeCount];
		closenesses = new float[nodeCount];
		snapshots = new AudioMixerSnapshot[nodeCount];

		target = this.transform.parent.GetComponentInChildren<AudioListener>();
        SetUp();

        //account for source and listener conflicts
        if (this.transform.parent.GetComponentInChildren<AudioSource>() != null)
        {
            Debug.Log("dealing with listener-source conflict");
            this.transform.parent.GetComponentInChildren<ReverbCalculator>().enabled = false;
            this.transform.parent.GetComponentInChildren<AudioSource>().spatialBlend = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (setUpHasRun)
        {
            //update positions (should this be a coroutine?)
            for (int i = 0; i < nodes.Length; i++)
            {
                closenesses[i] = 1 /
                    Vector3.Distance(nodes[i].transform.position,
                                     target.gameObject.transform.position);
            }
            BlendSnapshots();
        }
    }

    public void BlendSnapshots()
    {
        mixer.TransitionToSnapshots(snapshots, closenesses, 0.005f);
    }

    public void SetUp()
    {
        for (int i = 0; i < GameObject.Find("reverbManager").transform.FindChild("nodesGO").childCount; i++)
        {
            nodes[i] = GameObject.Find("reverbManager").transform.FindChild("nodesGO").transform.GetChild(i).gameObject;
            setUpHasRun = true;
        }
    }
}