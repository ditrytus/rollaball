using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour {

	public GameObject pickup;

	private float xMin = -9.0f;
	private float xMax = 9.0f;
	private float zMin = -9.0f;
	private float zMax = 9.0f;

	void Start()
	{
		SpawnPickup();
	}

	public void SpawnPickup()
	{
		Instantiate(
			pickup,
			new Vector3(
				Random.Range(xMin, xMax),
				1,
				Random.Range(zMin, zMax)),
			Quaternion.identity);
	}
}
