using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMover : MonoBehaviour {

	//movetowards target then circle it

	public Transform target;
	public float radius;
	public float period;

	private Vector3 centerPosition;
	private float degrees;

	void Start() {
		centerPosition = transform.position;
	}

	void Update() {
		// Update degrees
		float degreesPerSecond = 360.0f / period;
		degrees = Mathf.Repeat(degrees + (Time.deltaTime * degreesPerSecond), 360.0f);
		float radians = degrees * Mathf.Deg2Rad;

		// Offset on circle
		Vector3 offset = new Vector3(radius * Mathf.Cos(radians), radius * Mathf.Sin(radians), 0.0f);
		transform.position = target.position + offset;
	}

}
