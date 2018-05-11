using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundHit : MonoBehaviour {

	public float damageOnHit;
	public float timeBetweenDamage;

	private float lastHit;

	void Start() {
		lastHit = 0;
	}

	void OnCollisionStay2D(Collision2D collision) {
		if (Time.time > lastHit + timeBetweenDamage) {
			collision.gameObject.GetComponent<PlayerController>().DoDamage(damageOnHit);
			lastHit = Time.time;
			Camera.main.GetComponent<CameraShake>().ShakeCamera();
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Player") {
			collision.gameObject.GetComponent<PlayerController>().DoDamage(damageOnHit);
			Camera.main.GetComponent<CameraShake>().ShakeCamera();
		}
	}
}
