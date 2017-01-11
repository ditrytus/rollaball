using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class CameraController : MonoBehaviour {

	public GameObject player;
	
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;

		Observable.EveryLateUpdate().Subscribe(_ => {
			transform.position = player.transform.position + offset;
		});
	}
}
