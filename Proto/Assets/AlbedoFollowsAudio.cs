using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AlbedoFollowsAudio : MonoBehaviour {

	public Renderer rend;
	private Material mat;
	public AudioSource source;
	private AudioClip clip;
	public bool isPlaying;
	public float[] samples;

	// Use this for initialization
	void Start () {
		samples = new float[256];
		mat = rend.material;
	}
	
	// Update is called once per frame
	void Update () {
		if (source.isPlaying) {
			isPlaying = true;
			clip = source.clip;
		} else {
			isPlaying = false;
		}

		if (isPlaying) {
			source.GetSpectrumData (samples, 0, FFTWindow.BlackmanHarris);
			Color color = mat.color;
			float sample = samples [0]*8;
			mat.SetFloat("_Glossiness", sample);
	}
}
}