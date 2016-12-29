using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour {
	
	public float speed = 180.0f;
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0.0f, Time.deltaTime * speed, 0.0f);	
	}
}
