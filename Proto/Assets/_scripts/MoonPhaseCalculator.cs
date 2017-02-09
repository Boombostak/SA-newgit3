using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoonPhaseCalculator : MonoBehaviour
{

    public GameObject sun;
    public GameObject moon;
    public float angle;
    public Text text;
    public TOD_Sky sky;
    public float visibility;
    public float distance;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        angle = AngleSigned(sun.transform.position,moon.transform.position,Vector3.zero);
        text.text = "Sun/Moon angle is " + angle.ToString();
        MoonVisibility();
        Distance();
    }

    public float AngleSigned(Vector3 v1, Vector3 v2, Vector3 n)
    {


        Debug.DrawLine(v1, n, Color.yellow, 1, false);
        Debug.DrawLine(v2, n, Color.white, 1, false);
        //return Mathf.DeltaAngle(Mathf.Atan2(v1.x * v2.y - v1.y * v2.x), Mathf.Atan2(v1.x * v2.y - v1.y * v2.x))
        return Mathf.Atan2((v1.x * v2.y - v1.y * v2.x), Vector3.Dot(v2, v1)) * Mathf.Rad2Deg;
        
    }

    public void MoonVisibility()
    {
        visibility = sky.MoonVisibility;
    }

    public void Distance()
    {
        distance = (sun.transform.position - moon.transform.position).sqrMagnitude;
    }

    
}
