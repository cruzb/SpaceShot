using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSmallController : MonoBehaviour {

	public ScoreManager sm;
	public int points;
	public Vector2 velocity;
	public float rotateSpeed;
	public float damageOnHit;

	private Rigidbody2D rb;

	void Start() {
		sm = GameObject.Find("Managers/ScoreManager").GetComponent<ScoreManager>();
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = velocity;
		rb.AddTorque(rotateSpeed);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Projectile") {
			sm.AddPoints(points);
			Destroy(gameObject);
			//TODO play sound and anim
		}

		else if(other.tag == "Player") {
			sm.AddPoints(points);
			other.gameObject.GetComponent<PlayerController>().DoDamage(damageOnHit);
			Destroy(gameObject);
		}
	}

}
