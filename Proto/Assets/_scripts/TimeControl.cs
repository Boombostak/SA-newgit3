using UnityEngine;
using System.Collections;

public class TimeControl : MonoBehaviour {

	public static float static_TimeMultiplier =1;
	public float timeMultiplier = 1;
    //public float wheelValue;
    public TOD_Time tod_time;
	public TOD_Sky tod_sky;
    //public bool speedingUp;
    //public bool slowingDown;
    
    // Use this for initialization
	void Start () {
        tod_time = FindObjectOfType<TOD_Time>();
		tod_sky = FindObjectOfType<TOD_Sky> ();
	}
	
	// Update is called once per frame
	void Update () {
        /*wheelValue = Input.GetAxis ("Mouse ScrollWheel");
        if (wheelValue > 0)
        {
            speedingUp = true;
        }
        else
        {
            speedingUp = false;
        }

        if (wheelValue < 0)
        {
            slowingDown = true;
        }
        else
        {
            slowingDown = false;
        }
        if (speedingUp)
        {
            Mathf.Lerp(tod_time.DayLengthInMinutes, tod_time.DayLengthInMinutes / 2, 0.5f);
        }
        if (slowingDown)
        {
            Mathf.Lerp(tod_time.DayLengthInMinutes, tod_time.DayLengthInMinutes * 2, 0.5f);
        }
        */
        if (Input.GetKeyDown("q"))
        {
            tod_time.DayLengthInMinutes= tod_time.DayLengthInMinutes * 2;
			timeMultiplier /= 2;
			Debug.Log ("One real second equals" +1/static_TimeMultiplier+"game seconds");
        }
        if (Input.GetKeyDown("e"))
        {
            tod_time.DayLengthInMinutes= tod_time.DayLengthInMinutes / 2;
			timeMultiplier *= 2;
			Debug.Log ("One real second equals" +1/static_TimeMultiplier+"game seconds");
        }

		static_TimeMultiplier = timeMultiplier;

        //tod_time.DayLengthInMinutes = Mathf.Clamp(tod_time.DayLengthInMinutes, 0.0001f, 9999f) / timeMultiplier;
    }

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(tod_time.DayLengthInMinutes);
			//stream.SendNext (tod_sky.Cycle);
			//stream.SendNext (tod_sky.Clouds);
			stream.SendNext(timeMultiplier);
		}
		else
		{
			tod_time.DayLengthInMinutes = (float)stream.ReceiveNext();
			//tod_sky.Cycle = (TOD_CycleParameters)stream.ReceiveNext ();
			//tod_sky.Clouds = (TOD_CloudParameters)stream.ReceiveNext ();
			timeMultiplier = (float)stream.ReceiveNext();
		}
	}


	//Periodically or when timemultiplier changes, send the new multiplier and time of day to all clients
	//lerp from current value to the master value
	/*
	[PunRPC]
	public void SyncTimeOfDay(PhotonMessageInfo info)
	{
		tod_time
	}
	*/
}
