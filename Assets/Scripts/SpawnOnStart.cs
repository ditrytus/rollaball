using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnStart : MonoBehaviour {

	public GameObject spawner;

	// Use this for initialization
	void Start () {
		spawner.GetComponent<Spawner>().Spawn();
	}
}
