using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Spinner : MonoBehaviour {
	
	public float speed = 180.0f;
	
	void Start()
	{
		Observable
			.EveryUpdate()	
			.Subscribe(_ =>
			{
				transform.Rotate(0.0f, Time.deltaTime * speed, 0.0f);
			});
	}
}
