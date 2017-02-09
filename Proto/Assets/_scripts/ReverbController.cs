using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using System.Linq;

[RequireComponent(typeof(AudioSource))]
[RequireComponent (typeof(AudioReverbFilter))]
[RequireComponent(typeof(ReverbParameters))]
public class ReverbController : MonoBehaviour {

	//NOTE: Indexes of Node array, parameter array must match!!!
	//Set up nodes array length in editor!

	public AudioReverbFilter filter;
	public ReverbParameters averagedVerbParams;
	public GameObject[] nodes;
	public float[] closenesses;
	public float[] normalizedClosenesses;
	public ReverbParameters[] nodeParams;
	public AudioSource target;
	public RaycastHit hitToTestInside;
	public bool isInside;

	public float sumDryLevel;
	public float blendedDryLevel;
	public float sumRoom;
	public float blendedRoom;
	public float sumRoomHF;
	public float blendedRoomHF;
	public float sumRoomLF;
	public float blendedRoomLF;
	public float sumDecayTime;
	public float blendedDecayTime;
	public float sumHFRatio;
	public float blendedHFRatio;
	public float sumReflectionsLevel;
	public float blendedReflectionsLevel;
	public float sumReflectionsDelay;
	public float blendedReflectionsDelay;
	public float sumReverbLevel;
	public float blendedReverbLevel;
	public float sumReverbDelay;
	public float blendedReverbDelay;
	public float sumHFReference;
	public float blendedHFReference;
	public float sumLFReference;
	public float blendedLFReference;
	public float sumDiffusion;
	public float blendedDiffusion;
	public float sumDensity;
	public float blendedDensity;

	public float closest;
	public float farthest;



	public void SetUp()
	{
		int nodeCount = GameObject.Find ("reverbManager").transform.FindChild ("nodesGO").childCount;
		nodes = new GameObject[nodeCount];
		closenesses = new float[nodeCount];
		nodeParams = new ReverbParameters[nodeCount];
		normalizedClosenesses = new float[nodeCount];

		for (int i = 0; i < GameObject.Find("reverbManager").transform.FindChild("nodesGO").childCount; i++)
		{
			nodes[i] = GameObject.Find("reverbManager").transform.FindChild("nodesGO").transform.GetChild(i).gameObject;
			nodeParams [i] = nodes [i].GetComponent<ReverbParameters> ();
		}
	}

	public float FindHighestCloseness(float[] floats){
		float max = floats [0];
		for (int i = 0; i < floats.Length; i++) {
			if (floats [i] > max) {
				max = floats [i];
			}
		}
		return max;
	}

	public float FindLowestCloseness(float[] floats){
		float min = floats [0];
		for (int i = 0; i < floats.Length; i++) {
			if (floats[i]<min) {
				min = floats [i];
			}
		}
		return min;
	}

	void BlendNodeParams(){

		sumDryLevel = 0;
		for (int i = 0; i < nodeParams.Length; i++) 
		{
			sumDryLevel += (nodeParams [i].drylevel * normalizedClosenesses [i]);
		}
		blendedDryLevel = Mathf.Clamp(sumDryLevel/(float)nodeParams.Length, -10000.0f,0.0f);


		sumRoom = 0;
		for (int i = 0; i < nodeParams.Length; i++) {

			sumRoom += (nodeParams [i].room * normalizedClosenesses[i]);
		}
		blendedRoom = Mathf.Clamp(sumRoom/(float)nodeParams.Length,-10000.0f,0.0f);

		sumRoomHF = 0;
		for (int i = 0; i < nodeParams.Length; i++) {
			sumRoomHF += (nodeParams [i].roomhf * normalizedClosenesses[i]);
		}
		blendedRoomHF = Mathf.Clamp(sumRoomHF/(float)nodeParams.Length,-10000.0f,0.0f);

		sumRoomLF = 0;
		for (int i = 0; i < nodeParams.Length; i++) {
			sumRoomLF += (nodeParams [i].roomlf*normalizedClosenesses[i]);
		}
		blendedRoomLF = Mathf.Clamp(sumRoomLF/(float)nodeParams.Length,-10000.0f,0.0f);

		sumDecayTime = 0;
		for (int i = 0; i < nodeParams.Length; i++) {
			sumDecayTime += (nodeParams [i].decaytime*normalizedClosenesses[i]);
		}
		blendedDecayTime = Mathf.Clamp(sumDecayTime/(float)nodeParams.Length, 0.1f,20.0f);

		sumHFRatio = 0;
		for (int i = 0; i < nodeParams.Length; i++) {
			sumHFRatio += (nodeParams [i].decayhfratio*normalizedClosenesses[i]);
		}
		blendedHFRatio = Mathf.Clamp(sumHFRatio/(float)nodeParams.Length, 0.1f,2.0f);

		sumReflectionsLevel = 0;
		for (int i = 0; i < nodeParams.Length; i++) {
			sumReflectionsLevel += (nodeParams [i].reflectionslevel*normalizedClosenesses[i]);
		}
		blendedReflectionsLevel = Mathf.Clamp(sumReflectionsLevel/(float)nodeParams.Length, -10000.0f,1000f);

		sumReflectionsDelay = 0;
		for (int i = 0; i < nodeParams.Length; i++) {
			sumReflectionsDelay += (nodeParams [i].reflectionsdelay*normalizedClosenesses[i]);
		}
		blendedReflectionsDelay = Mathf.Clamp(sumReflectionsDelay/(float)nodeParams.Length,-10000.0f,2000.0f);

		sumReverbLevel = 0;
		for (int i = 0; i < nodeParams.Length; i++) {
			sumReverbLevel += (nodeParams [i].reverblevel*normalizedClosenesses[i]);
		}
		blendedReverbLevel = Mathf.Clamp(sumReverbLevel/(float)nodeParams.Length,-10000f,2000f);

		sumReverbDelay = 0;
		for (int i = 0; i < nodeParams.Length; i++) {
			sumReverbDelay += (nodeParams [i].reverbdelay*normalizedClosenesses[i]);
		}
		blendedReverbDelay = Mathf.Clamp(sumReverbDelay/(float)nodeParams.Length,0.0f,0.1f);

		sumHFReference = 0;
		for (int i = 0; i < nodeParams.Length; i++) {
			sumHFReference += (nodeParams [i].hfreference*normalizedClosenesses[i]);
		}
		blendedHFReference = Mathf.Clamp(sumHFReference/(float)nodeParams.Length,1000f,20000f);

		sumLFReference = 0;
		for (int i = 0; i < nodeParams.Length; i++) {
			sumLFReference += (nodeParams [i].lfreference*normalizedClosenesses[i]);
		}
		blendedLFReference = Mathf.Clamp(sumLFReference/(float)nodeParams.Length,20f,1000f);

		sumDiffusion = 0;
		for (int i = 0; i < nodeParams.Length; i++) {
			sumDiffusion += (nodeParams [i].diffusion*normalizedClosenesses[i]);
		}
		blendedDiffusion = Mathf.Clamp(sumDiffusion/(float)nodeParams.Length,0.0f,100f);

		sumDensity = 0;
		for (int i = 0; i < nodeParams.Length; i++) {
			sumDensity += (nodeParams [i].density*normalizedClosenesses[i]);
		}
		blendedDensity = Mathf.Clamp(sumDensity/(float)nodeParams.Length,0f,100f);
	}

	// Use this for initialization
	void Start () {
		filter = this.gameObject.GetComponent<AudioReverbFilter>();
		target = this.GetComponentInChildren<AudioSource>();
		averagedVerbParams = this.GetComponent<ReverbParameters> ();
		SetUp();
	}

	public void AssignBlendsToParams(){
		averagedVerbParams.drylevel = blendedDryLevel;
		averagedVerbParams.room = blendedRoom;
		averagedVerbParams.roomhf = blendedRoomHF;
		averagedVerbParams.roomlf = blendedRoomLF;
		averagedVerbParams.decaytime = blendedDecayTime;
		averagedVerbParams.decayhfratio = blendedHFRatio;
		averagedVerbParams.reflectionslevel = blendedReflectionsLevel;
		averagedVerbParams.reflectionsdelay = blendedReflectionsDelay;
		averagedVerbParams.reverblevel = blendedReverbLevel;
		averagedVerbParams.reverbdelay = blendedReverbDelay;
		averagedVerbParams.hfreference = blendedHFReference;
		averagedVerbParams.lfreference = blendedLFReference;
		averagedVerbParams.diffusion = blendedDiffusion;
		averagedVerbParams.density = blendedDensity;
	}


	[ExecuteInEditMode]
	public void AssignParamsToVerb(){
		filter.dryLevel = blendedDryLevel;
		filter.room = blendedRoom;
		filter.roomHF = blendedRoomHF;
		filter.roomLF = blendedRoomLF;
		filter.decayTime = blendedDecayTime;
		filter.decayHFRatio = blendedHFRatio;
		filter.reflectionsLevel = blendedReflectionsLevel;
		filter.reflectionsDelay = blendedReflectionsDelay;
		filter.reverbLevel = blendedReverbLevel;
		filter.reverbDelay = blendedReverbDelay;
		filter.hfReference = blendedHFReference;
		filter.lfReference = blendedLFReference;
		filter.diffusion = blendedDiffusion;
		filter.density = blendedDensity;
	}
	
	// Update is called once per frame
	void Update ()
	{
		AssignBlendsToParams ();
		AssignParamsToVerb ();
		closest = FindHighestCloseness (closenesses);
		farthest = FindLowestCloseness (closenesses);
		BlendNodeParams ();


		/*if (hitToTestInside.collider.bounds.Contains(target.gameObject.transform.position))
		{
			isInside = true;
		}
		else
		{
			isInside = false;
		}*/

		//update positions (should this be a coroutine?)


		for (int i = 0; i < nodes.Length; i++) {
			closenesses [i] =

				1 /
				
			(Vector3.Distance (nodes [i].transform.position, 
				target.transform.position));
		}

		//normalize values
		for (int i = 0; i < closenesses.Length; i++) {
			normalizedClosenesses [i] = (closenesses [i] - farthest) / (closest - farthest);
		}
	}
}
