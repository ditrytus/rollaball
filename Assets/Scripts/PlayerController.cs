using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerController : MonoBehaviour
{
	public GameObject pickupSpawner;

	public float speed = 1.0f;

	public float impulse = 10;

	public float density = 4.0f / 3.0f;

	public float massGain = 3.0f;

	private Rigidbody rb;

	public int Size = 3;

	public int Collected = 0;
	public int MaximumSize = 3;

	public Text collectedText;
	public Text maxSizeText;
	public Text timeText;

	private DateTime startTime;

	public float GetMass()
	{
		return Mathf.Pow(GetDiameter(), massGain) * density * Mathf.PI;
	}

	public float GetDiameter()
	{
		return Size * 0.333f;
	}

	public enum PlayerEvent
	{
		CollectedPickup,
		HitByEnemy
	}

	private Vector3 initialScale;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		Grow();
		startTime = DateTime.Now;
		InvokeRepeating("UpdateTimer", 0.01f, 0.01f);
	}

	public void UpdateTimer()
	{
		var duration = DateTime.Now - startTime;
		var minutes = duration.Minutes.ToString("00");
		var seconds = duration.Seconds.ToString("00");
		var fractions = (duration.Milliseconds / 100).ToString("0");
		timeText.text = string.Format("TIME: {0}:{1}.{2}", minutes, seconds, fractions);
	}

	public void HandleEvent(PlayerEvent playerEvent)
	{
		switch (playerEvent)
		{
			case PlayerEvent.CollectedPickup: Size++; Collected++; break;
			case PlayerEvent.HitByEnemy: Size--; break;
		}
		MaximumSize = Mathf.Max(Size, MaximumSize);
		collectedText.text = "PILLS: " + Collected.ToString();
		maxSizeText.text = "MAX SIZE: " + MaximumSize.ToString();
		Grow();
	}

	private void Grow()
	{
		rb.mass = GetMass();
		transform.localScale = new Vector3(
			GetDiameter(),
			GetDiameter(),
			GetDiameter());
	}

	void FixedUpdate ()
	{
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
				rb.AddForce(contact.normal * impulse * GetMass(), ForceMode.Impulse);
				HandleEvent(PlayerEvent.HitByEnemy);
			}
        }
    }

	void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.CompareTag("pickup"))
		{
			DestroyObject(other.gameObject);
			pickupSpawner.GetComponent<Spawner>().Spawn();
			HandleEvent(PlayerEvent.CollectedPickup);
		}
    }
}
