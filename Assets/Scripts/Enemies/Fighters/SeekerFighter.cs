using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerFighter : MonoBehaviour {

	public Transform target;
	public Transform projectileSpawn;
	public GameObject projectile;

	public float distanceBeginCircle;
	public float speed;
	public float rotationSpeed;
	public float fireRate;


	private Rigidbody2D rb;
	private float lastShotTime;

	void Start () {
		if (target == null) {
			if (GetComponent<TargetHolder>().target == null) {
				target = GameObject.FindGameObjectWithTag("Player").transform;
				Debug.Log("ERROR: Target not assigned in script");
			}
			else target = GetComponent<TargetHolder>().target;
		}

		rb = GetComponent<Rigidbody2D>();
		lastShotTime = 0;
	}
	

	void Update () {
		//shoot
		if(Time.time > lastShotTime + 1 / fireRate) {
			lastShotTime = Time.time;
			GameObject go = Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
			go.GetComponent<SeekerShot>().target = target;
		}


		//move
		Vector2 direction = target.position - transform.position;

		float offset;
		if (direction.magnitude > distanceBeginCircle)
			offset = 90;
		else offset = 0;

		direction.Normalize();

		float angleZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - offset;

		Quaternion rotation = Quaternion.Euler(0, 0, angleZ);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

		rb.velocity = transform.up * speed;
	}
}
