using UnityEngine;
using System.Collections;
using System;
using System.Globalization;
using System.Threading;

public class TimeControl : MonoBehaviour {

	public static float static_TimeMultiplier =1;
	public float timeMultiplier = 1;
    //public float wheelValue;
    public TOD_Time tod_time;
	public TOD_Sky tod_sky;
	public string dt;
	public CultureInfo canadianEnglish = CultureInfo.CreateSpecificCulture("en-CA");
    //public bool speedingUp;
    //public bool slowingDown;
    
    // Use this for initialization
	void Start () {
        tod_time = FindObjectOfType<TOD_Time>();
		tod_sky = FindObjectOfType<TOD_Sky> ();
		//InvokeRepeating ("SyncTime", 0, 10);
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
		if (PhotonNetwork.isMasterClient) {
			Thread.CurrentThread.CurrentCulture = canadianEnglish;
			dt = tod_sky.Cycle.DateTime.ToString ();
			if (Input.GetKeyDown ("q")) {
				tod_time.DayLengthInMinutes = tod_time.DayLengthInMinutes * 2;
				timeMultiplier /= 2;
				Debug.Log ("One real second equals" + 1 / static_TimeMultiplier + "game seconds");
				//SyncTime ();
			}
			if (Input.GetKeyDown ("e")) {
				tod_time.DayLengthInMinutes = tod_time.DayLengthInMinutes / 2;
				timeMultiplier *= 2;
				Debug.Log ("One real second equals" + 1 / static_TimeMultiplier + "game seconds");
				//SyncTime ();
			}

			static_TimeMultiplier = timeMultiplier;
		}

        //tod_time.DayLengthInMinutes = Mathf.Clamp(tod_time.DayLengthInMinutes, 0.0001f, 9999f) / timeMultiplier;
    }

	/*[PunRPC]
	public void SyncTime()
	{
		Debug.Log("RPC SyncTime SENT");
		tod_sky.Cycle.DateTime = Convert.ToDateTime (dt);
	}*/

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(tod_time.DayLengthInMinutes);
			//stream.SendNext (tod_sky.Cycle);
			//stream.SendNext (tod_sky.Clouds);
			stream.SendNext(timeMultiplier);
			stream.SendNext (dt);
		}
		else
		{
			tod_time.DayLengthInMinutes = (float)stream.ReceiveNext();
			//tod_sky.Cycle = (TOD_CycleParameters)stream.ReceiveNext ();
			//tod_sky.Clouds = (TOD_CloudParameters)stream.ReceiveNext ();
			timeMultiplier = (float)stream.ReceiveNext();
			dt = (string)stream.ReceiveNext ();
			tod_sky.Cycle.DateTime = Convert.ToDateTime (dt);
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
