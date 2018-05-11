using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileDamage : MonoBehaviour {

	public float damageOnHit;
	public GameObject hitEffect;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			other.gameObject.GetComponent<PlayerController>().DoDamage(damageOnHit);
			Camera.main.GetComponent<CameraShake>().ShakeCamera();

			if (hitEffect != null)
				Instantiate(hitEffect, transform.position, transform.rotation);

			Destroy(gameObject);
		}
	}
}
