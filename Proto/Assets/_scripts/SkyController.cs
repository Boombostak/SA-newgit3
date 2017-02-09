using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class SkyController : MonoBehaviour {

    public TOD_Sky sky;
    private TOD_CloudParameters skyCloudParams;
    public List<TOD_CloudParameters> cloudSnapshots;
    private TOD_CloudParameters currentCloud;
    
    // Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    
    void Update () {
        skyCloudParams = sky.Clouds;
        Debug.Log("editor updated skyCloudParams!");
	}

    //clouds

    /*[System.Serializable]
    public class CloudSnapshot
    {
        [Range(0,10)]
        public float size;
        [Range(0, 1)]
        public float opacity;
        [Range(0, 1)]
        public float coverage;
        [Range(0, 1)]
        public float sharpness;
        [Range(0, 1)]
        public float attenuation;
        [Range(0, 1)]
        public float saturation;
        [Range(0, 1)]
        public float scattering;
        [Range(0, 1)]
        public float brightness;

        
    }*/


    //not currently working. Instances are not properly separated.
    public void AddCloudSnapshot()
    {
        currentCloud = new TOD_CloudParameters();
        currentCloud = skyCloudParams;
        cloudSnapshots.Add(currentCloud);
    }
}
