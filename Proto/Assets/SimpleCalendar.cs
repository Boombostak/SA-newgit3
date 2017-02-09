using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Globalization;
using System.Threading;

public class SimpleCalendar : MonoBehaviour {

	public TOD_Sky Sky;
	public string dotw;
	public Text textGregorian;
	public Text textHijri;
	public HijriCalendar hijriCal;
	public CultureInfo turkey = CultureInfo.CreateSpecificCulture("tr-TR");
	public CultureInfo canadianEnglish = CultureInfo.CreateSpecificCulture("en-CA");


	void Start()
	{
		hijriCal = new HijriCalendar ();
	}
	//Gregorian
	public void CheckDay ()
	{
		Thread.CurrentThread.CurrentCulture = canadianEnglish;
		dotw = Sky.Cycle.DateTime.ToString ();
		textGregorian.text = "Gregorian" + dotw;
	}

	public void ConvertToHijri()
	{
		Thread.CurrentThread.CurrentCulture = turkey;
		//DateTime date1 = Sky.Cycle.DateTime;
		turkey.DateTimeFormat.Calendar = hijriCal;
		dotw = Sky.Cycle.DateTime.ToString ();
		textHijri.text = "Hijri"+dotw;
	}

	void Update ()
	{
		CheckDay ();
		ConvertToHijri ();
	}
}



