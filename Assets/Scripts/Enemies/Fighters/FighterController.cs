using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterController : MonoBehaviour {

	public Transform target;
	public Transform projectile;
	public Transform projectileSpawn;
	public Transform explosion;
	public Transform[] debris;
	public float speed;
	public float rotationSpeed;
	public float fireRate;
	public float damageOnHit;
	public bool destroyOnCrash;

	private Rigidbody2D rb;
	private float lastShotTime;


	void Start() {
		if (target == null) {
			if (GetComponent<TargetHolder>().target == null) {
				target = GameObject.FindGameObjectWithTag("Player").transform;
				Debug.Log("ERROR: Target not assigned in script");
			}
			else target = GetComponent<TargetHolder>().target;
		}
		

		rb = GetComponent<Rigidbody2D>();
		lastShotTime = 0;
		rb.velocity = new Vector2(speed, 0);
	}

	void Update() {
		//Vector2 v = target.position;
		//transform.LookAt(v);
		//transform.Rotate(new Vector3(0, -90, 0), Space.Self);

		Vector2 direction = target.position - transform.position;
		direction.Normalize();

		float angleZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

		Quaternion rotation = Quaternion.Euler(0, 0, angleZ);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

		rb.AddForce(transform.right * 5);
		if (rb.velocity.magnitude > speed) {
			Vector2 vel = rb.velocity;
			if (vel.magnitude > speed) {
				vel = vel.normalized * speed;
				rb.velocity = vel;
			}
		}
		
		if (Time.time > 1 / fireRate + lastShotTime) {
			lastShotTime = Time.time;
			Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
		}
	}


	void DoDeath() {
		for (int i = 0; i < debris.Length; i++) {
			Transform t = Instantiate(debris[i], transform.position, Quaternion.identity);
			DebrisController s = t.gameObject.GetComponent<DebrisController>();
			s.velocity = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
			if (s.velocity.magnitude == 0) {
				s.velocity = new Vector2(0.5f, -1);
			}
			s.rotateSpeed = Random.Range(40f, 70f);
		}
	}
}
