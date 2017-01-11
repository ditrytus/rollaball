using UnityEngine;
using UniRx;
using System;
using UnityEngine.UI;

public class TimeController : MonoBehaviour {

	public Text timeText;

	private DateTime startTime;
	
	// Use this for initialization
	void Start () {
		
		startTime = DateTime.Now;

		Observable
			.Interval(TimeSpan.FromMilliseconds(10))
			.Select(l => DateTime.Now - startTime)
			.Select(duration => string.Format(
				"TIME: {0}:{1}.{2}",
				duration.Minutes.ToString("00"),
				duration.Seconds.ToString("00"),
				(duration.Milliseconds / 100).ToString("0")))
			.SubscribeToText(timeText);
	}
	
}
