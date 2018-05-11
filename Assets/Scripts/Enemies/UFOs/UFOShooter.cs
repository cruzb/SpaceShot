using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOShooter : MonoBehaviour {

	//move speed
	public float minSpeed;
	public float maxSpeed;
	private float speed;
	private Rigidbody2D rb;

	//shooting
	public float fireRate;
	public Transform projectile;
	public Transform projectileSpawn;
	public int numShots;

	private float lastShotTime;
	private float rotationAmount;

	// Use this for initialization
	void Start() {
		speed = Random.Range(minSpeed, maxSpeed);
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = new Vector2(speed, 0);

		lastShotTime = 0;
		rotationAmount = 360 / numShots;
	}

	// Update is called once per frame
	void Update() {
		if (Time.time > 1 / fireRate + lastShotTime) {
			lastShotTime = Time.time;
			for (int i = 0; i < numShots; i++) {
				Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
				projectileSpawn.Rotate(new Vector3(0, 0, rotationAmount));
			}
		}
	}

}
