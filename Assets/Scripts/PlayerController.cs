﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed = 1.0f;
	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate () {
		float moveHorizonta = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizonta, 0.0f, moveVertical);

		rb.AddForce(movement * speed);
	}

	void OnCollisionEnter(Collision collision)
	{
		foreach (ContactPoint contact in collision.contacts)
		{
			var otherObject = contact.otherCollider.gameObject;
            if (otherObject.CompareTag("enemy"))
			{
				otherObject.SetActive(false);
			}
        }
    }
}
