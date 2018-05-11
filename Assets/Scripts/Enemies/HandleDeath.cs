using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleDeath : MonoBehaviour {

	public GameObject explosion;
	public bool destroyOnCrash;
	public float damageOnHit;

	public int points;
	public ScoreManager sm;

	void Start() {
		sm = GameObject.Find("Managers/ScoreManager").GetComponent<ScoreManager>();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			sm.AddPoints(points);
			other.GetComponent<PlayerController>().DoDamage(damageOnHit);
			if (destroyOnCrash) {
				Instantiate(explosion, transform.position, other.transform.rotation);
				gameObject.SendMessage("DoDeath");
				Destroy(gameObject);
			}
		}
		else if (other.tag == "Projectile") {
			//Play death anim + sound
			sm.AddPoints(points);
			gameObject.SendMessage("DoDeath");
			Instantiate(explosion, transform.position, other.transform.rotation);
			Destroy(gameObject);
			Destroy(other.gameObject);
		}
		else if (other.tag == "PersistentProjectile") {
			sm.AddPoints(points);
			gameObject.SendMessage("DoDeath");
			Instantiate(explosion, transform.position, other.transform.rotation);
			Destroy(gameObject);
		}
	}
}
