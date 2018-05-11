using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisController : MonoBehaviour {

	public Vector2 velocity;
	public float rotateSpeed;

	void Start() {
		Rigidbody2D rb = GetComponent<Rigidbody2D>();
		rb.velocity = velocity;
		rb.AddTorque(rotateSpeed);
	}
}
