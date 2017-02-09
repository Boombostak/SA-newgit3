using UnityEngine;
using System.Collections;

public class WeatherRandomizer : MonoBehaviour {


	public TimeControl TC;
	public TOD_Sky todsky;
	public TOD_Animation todanim;
	public float waitTime;
	public float perc;
	public float elapsedTime;
	public float[] cloudSnapshot;
	public float[] cloudRNG;

	// Use this for initialization
	void Start () {
		cloudSnapshot = new float[10];
		cloudRNG = new float[10];
	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime * TimeControl.static_TimeMultiplier;
		if (elapsedTime >= (waitTime)) {
			for (int i = 0; i < cloudSnapshot.Length; i++) {
				cloudSnapshot [i] = cloudRNG [i];
			}
			cloudRNG[0] = Random.Range(0f,1f);
			cloudRNG[1] = Random.Range(0f,30f);
			cloudRNG[2] = Random.Range(0f,1f);
			cloudRNG[3] = Random.Range(0f,1f);
			cloudRNG[4] = Random.Range(0f,1f);
			cloudRNG[5] = Random.Range(0f,30f);
			cloudRNG[6] = Random.Range(0f,1f);
			cloudRNG[7] = Random.Range(0f,30f);
			cloudRNG[8] = Random.Range(0f,80f);
			cloudRNG[9] = Random.Range(0f,360f);
			elapsedTime = 0f;
			}
		if (FindObjectOfType<PlayerInput>() != null) {
			TC = GameObject.FindObjectOfType<TimeControl> ();
		}

		perc = elapsedTime / (waitTime);
		todsky.Clouds.Attenuation = Mathf.Lerp (cloudSnapshot[0], cloudRNG[0], perc);
		todsky.Clouds.Brightness = Mathf.Lerp (cloudSnapshot[1], cloudRNG[1], perc);
		todsky.Clouds.Coverage = Mathf.Lerp (cloudSnapshot[2], cloudRNG[2], perc);
		todsky.Clouds.Opacity = Mathf.Lerp (cloudSnapshot[3], cloudRNG[3], perc);
		todsky.Clouds.Saturation = Mathf.Lerp (cloudSnapshot[4], cloudRNG[4], perc);
		todsky.Clouds.Scattering = Mathf.Lerp (cloudSnapshot[5], cloudRNG[5], perc);
		todsky.Clouds.Sharpness = Mathf.Lerp (cloudSnapshot[6], cloudRNG[6], perc);
		todsky.Clouds.Size = Mathf.Lerp (cloudSnapshot[7], cloudRNG[7], perc);
		todanim.WindSpeed = Mathf.Lerp (cloudSnapshot[8], cloudRNG[8], perc);
		todanim.WindDegrees = Mathf.Lerp (cloudSnapshot[9], cloudRNG[9], perc);
		
	}
}
