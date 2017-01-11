using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;

public class SpawnOnInterval : MonoBehaviour {

	public GameObject spawner;

	public float delay = 1.0f;
	public float interval = 1.0f;

	// Use this for initialization
	void Start ()
	{
		Observable
			.Interval(TimeSpan.FromSeconds(interval))
			.Delay(TimeSpan.FromSeconds(delay))
			.Subscribe(_ => 
			{
				spawner.GetComponent<Spawner>().Spawn();
			});
	}
}
