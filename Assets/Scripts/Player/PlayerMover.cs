using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour {

	[Header("Speed Limits")]
	public float lowSpeed = 1.75f;
	public float defaultSpeed = 3f;
	public float highSpeed = 5;

	[Header("Linear Movement Forces")]
	public float lowForce = 8;
	public float defaultForce = 15;
	public float highForce = 25;

	[Header("Rotation Torques")]
	public bool canStrafe = false;
	public float turnSpeed = 125;
	public float strafeSpeed = 6;

	[Header("Boost (todo)")]
	public bool canBoost = false;
	public float boostForce;

	private Vector2 velocity;
	private Rigidbody2D rb;


	private void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

	private void Update() {

	}

	private void FixedUpdate() {
		//rotation
		if (canStrafe) {
			rb.AddRelativeForce(Vector2.right * -Input.GetAxis("Horizontal") * strafeSpeed);


			//rotate to follow mouse if can strafe
			Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
			direction.Normalize();
			float angleZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
			Quaternion rotation = Quaternion.Euler(0, 0, angleZ);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
		}
		else {
			Quaternion rotation = transform.rotation;
			float z = rotation.eulerAngles.z;
			z -= Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
			rotation = Quaternion.Euler(0, 0, z);
			transform.rotation = rotation;
		}



		//lateral movement
		Vector2 velocity = rb.velocity;
		if (Input.GetKey(KeyCode.W)) {
			rb.AddRelativeForce(Vector2.up * 25);
			velocity = Vector2.ClampMagnitude(velocity, highSpeed);
		}
		else if (Input.GetKey(KeyCode.S)) {
			//decelerate rather than instantly lower speed
			if (velocity.magnitude - lowSpeed < 0.01) {
				rb.AddRelativeForce(Vector2.up * 8);
				velocity = Vector2.ClampMagnitude(velocity, lowSpeed);
			}
		}
		else {
			if (velocity.magnitude - defaultSpeed < 0.01) {
				rb.AddRelativeForce(Vector2.up * 15);
				velocity = Vector2.ClampMagnitude(velocity, defaultSpeed);
			}

		}
		rb.velocity = velocity;
	}
}
