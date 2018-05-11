using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinWaveMover : MonoBehaviour {

	public float speed;
	public float amplitude;
	public float period;

	private Vector3 centerPosition;
	private float degrees;

	void Start() {
		centerPosition = transform.position;
	}

	void Update() {
		float deltaTime = Time.deltaTime;

		// Move center along x axis
		//velocity move towards
		centerPosition.x += deltaTime * speed;

		// Update degrees
		float degreesPerSecond = 360.0f / period;
		degrees = Mathf.Repeat(degrees + (deltaTime * degreesPerSecond), 360.0f);
		float radians = degrees * Mathf.Deg2Rad;

		// Offset by sin wave
		Vector3 offset = new Vector3(0.0f, amplitude * Mathf.Sin(radians), 0.0f);
		transform.position = centerPosition + offset;
	}
}
