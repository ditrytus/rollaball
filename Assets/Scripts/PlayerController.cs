using UnityEngine;
using UnityEngine.UI;
using System;
using UniRx;
using UniRx.Triggers;

public class PlayerController : MonoBehaviour
{
	public GameObject pickupSpawner;

	private Rigidbody rigidBody;

	public Text collectedText;

	public Text maxSizeText;

	public float impulse = 10;

	public float density = 4.0f / 3.0f;

	public float massGain = 3.0f;

	public float speed = 1.0f;

	public IntReactiveProperty Size { get; private set; }

	public IntReactiveProperty Collected { get; private set; }

	public IntReactiveProperty MaximumSize { get; private set; }

	public ReadOnlyReactiveProperty<float> Diameter { get; private set; }

	public ReadOnlyReactiveProperty<float> Mass { get; private set; }

	void Start()
	{
		rigidBody = GetComponent<Rigidbody>();

		Size = new IntReactiveProperty(3);

		Collected = new IntReactiveProperty(0);
		Collected
			.Select(c => $"PILLS: {c}")
			.SubscribeToText(collectedText);

		MaximumSize = new IntReactiveProperty(0);
		MaximumSize
			.Select(m => $"MAX SIZE: {m}")
			.SubscribeToText(maxSizeText);

		Size
			.Subscribe(s =>
			{
				MaximumSize.Value = Math.Max(MaximumSize.Value, s);
			});

		Diameter = Size
			.Select(s => s * 0.333f)
			.ToReadOnlyReactiveProperty();
		Diameter
			.SubscribeOnMainThread()
			.Subscribe(diameter =>
			{
				transform.localScale = new Vector3(diameter, diameter, diameter);
			});

		Mass = Diameter
			.Select(d => Mathf.Pow(d, massGain) * density * Mathf.PI)
			.ToReadOnlyReactiveProperty();
		Mass
			.SubscribeOnMainThread()
			.Subscribe(mass =>
			{
				rigidBody.mass = mass;
			});

		Observable
			.EveryFixedUpdate()
			.SubscribeOnMainThread()
			.Subscribe(_ =>
			{
				float moveHorizonta = Input.GetAxis(Axes.Horizontal);
				float moveVertical = Input.GetAxis(Axes.Vertical);

				Vector3 movement = new Vector3(moveHorizonta, 0.0f, moveVertical);

				rigidBody.AddForce(movement * speed);
			});

		this.OnTriggerEnterAsObservable()
			.Where(other => other.gameObject.CompareTag(Tags.Pickup))
			.SubscribeOnMainThread()
			.Subscribe(other =>
			{
				DestroyObject(other.gameObject);
				pickupSpawner.GetComponent<Spawner>().Spawn();
				Size.Value++;
				Collected.Value++;
			});

		this.OnCollisionEnterAsObservable()
			.SelectMany(collision => collision.contacts)
			.Where(contact => contact.otherCollider.gameObject.CompareTag(Tags.Enemy))
			.SubscribeOnMainThread()
			.Subscribe(contact =>
			{
				rigidBody.AddForce(contact.normal * impulse * Mass.Value, ForceMode.Impulse);
				Size.Value--;
			});
	}
}
