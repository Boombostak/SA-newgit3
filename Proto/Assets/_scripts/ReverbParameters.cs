using UnityEngine;
using System.Collections;

public class ReverbParameters : MonoBehaviour {

	[Range(-10000.0f, 10f)]
	public float drylevel;
	[Range(-10000.0f, 10f)]
	public float room;
	[Range(-10000.0f, 10f)]
	public float roomhf;
	[Range(-10000.0f, 10f)]
	public float roomlf;
	[Range(0.1f, 20f)]
	public float decaytime;
	[Range(0.1f, 2f)]
	public float decayhfratio;
	[Range(-10000.0f, 1000f)]
	public float reflectionslevel;
	[Range(-10000.0f, 2000f)]
	public float reflectionsdelay;
	[Range(-10000.0f, 2000f)]
	public float reverblevel;
	[Range(0.0f, 0.1f)]
	public float reverbdelay;
	[Range(1000f, 20000f)]
	public float hfreference;
	[Range(20f, 1000f)]
	public float lfreference;
	[Range(0f, 100f)]
	public float diffusion;
	[Range(0f, 100f)]
	public float density;
	}
