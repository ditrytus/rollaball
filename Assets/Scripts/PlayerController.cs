using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public GameObject pickupSpawner;

	public float speed = 1.0f;

	public float impulse = 10;

	public float density = 4.0f / 3.0f;

	public float massGain = 3.0f;

	private Rigidbody rb;

	public int Size = 3;

	public float GetMass()
	{
		return Mathf.Pow(GetDiameter(), massGain) * density * Mathf.PI;
	}

	public float GetDiameter()
	{
		return Size * 0.333f;
	}

	private Vector3 initialScale;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
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
				Size--;
				Grow();
			}
        }
    }

	void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.CompareTag("pickup"))
		{
			DestroyObject(other.gameObject);
			pickupSpawner.GetComponent<Spawner>().Spawn();
			Size++;
			Grow();
		}
    }
}
