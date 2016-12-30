using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnInterval : MonoBehaviour {

	public GameObject spawner;

	public float delay = 1.0f;
	public float interval = 1.0f;

	// Use this for initialization
	void Start () {
		InvokeRepeating("Spawn", delay, interval);
	}

	public void Spawn()
	{
		spawner.GetComponent<Spawner>().Spawn();
	}
}
