using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerShot : MonoBehaviour {

	public Transform target;
	public float initForce;
	public float speed;
	public float rotationSpeed;
	public float waitTime;
	public float distanceStop;

	public GameObject boost;
	public AudioClip boostSound;

	private ParticleSystem exhaust;
	private Light engineLight;
	private Rigidbody2D rb;
	private float startTime;
	private bool active;
	private bool beginHoming; //TODO rename

	void Start () {
		active = true;
		beginHoming = true;

		startTime = Time.time;
		rb = GetComponent<Rigidbody2D>();
		rb.AddForce(transform.up * initForce);
		exhaust = GetComponentInChildren<ParticleSystem>();
		engineLight = GetComponentInChildren<Light>();
	}
	
	void FixedUpdate () {
		if (active) {
			if (Vector2.Distance(transform.position, target.position) > distanceStop) {
				if (Time.time > startTime + waitTime) {
					if (beginHoming) {
						beginHoming = false;
						Instantiate(boost, transform.position, transform.rotation);
						exhaust.Play();
						engineLight.enabled = true;
					}
					Vector2 direction = target.position - transform.position;
					direction.Normalize();

					float angleZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

					Quaternion rotation = Quaternion.Euler(0, 0, angleZ - 90);
					transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

					rb.velocity = transform.up * speed;
				}
			}
			else {
				//end particle effects
				active = false;
				exhaust.Stop();
				engineLight.enabled = false;
			}
		}
	}
}
