using UnityEngine;
using System.Collections;

public class SunBehaviour : MonoBehaviour
{

    public Light ambientlight;
    public GameObject marker1;
    public GameObject marker2;
    public Quaternion startrot;
    public Quaternion endrot;
    public float speed;
    public float starttime;
    public float journeylength;
    public bool moonmode;
    public Color suncolor;
    public Color mooncolor;

    // Use this for initialization
    void Start()
    {
        startrot = marker1.transform.rotation;
        this.transform.rotation = marker1.transform.rotation;
        endrot = marker2.transform.rotation;
        ambientlight = this.gameObject.GetComponent<Light>();
        starttime = Time.time;
        journeylength = Vector3.Distance(marker1.transform.position, marker2.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (!moonmode)
        {
            ambientlight.color = suncolor;
            transform.Rotate(Vector3.right, speed * Time.deltaTime);
            //transform.rotation = Quaternion.Lerp(startrot, endrot, Time.time * speed);
        }
        if (moonmode)
        {
            ambientlight.color = mooncolor;
            transform.Rotate(Vector3.right, speed * Time.deltaTime);
            //transform.rotation = Quaternion.Lerp(startrot, endrot, Time.time * speed);
        }

        if (transform.eulerAngles.x >= 350 && !moonmode)
        //(Quaternion.Angle(transform.rotation, endrot) < 0.01)
        {
            transform.eulerAngles = new Vector3(0, 60, 0);
            moonmode = true;
            //this.transform.rotation = startrot;
            //moonmode = !moonmode;
        }
        if (transform.eulerAngles.x >=350 && moonmode)
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
            moonmode = false;
        }
    }
}
