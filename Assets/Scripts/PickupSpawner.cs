using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour {

	public GameObject pickup;

	private const float DegreesInCircle = 360.0f;

	private float xMin = -9.0f;
	private float xMax = 9.0f;
	private float zMin = -9.0f;
	private float zMax = 9.0f;

	private float spawnY = 20.0f;

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
				spawnY,
				Random.Range(zMin, zMax)),
			Quaternion.Euler(
				GetRandomAngle(),
				GetRandomAngle(),
				GetRandomAngle()
			));
	}

	private float GetRandomAngle()
	{
		return Random.Range(0, DegreesInCircle);
	}
}
