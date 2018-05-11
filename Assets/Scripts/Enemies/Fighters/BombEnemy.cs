using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEnemy : MonoBehaviour {

	public SpriteRenderer sr;
	public Rigidbody2D rb;
	public float damage;
	public float bombRadius;
	public float minExplodeTime;
	public float maxExplodetime;
	public float minSpeed;
	public float maxSpeed;

	private float explodeTime;
	private float speed;
	private float startTime;

	void Start() {
		sr = GetComponent<SpriteRenderer>();
		rb = GetComponent<Rigidbody2D>();
		speed = Random.Range(minSpeed, maxSpeed);
		rb.velocity = new Vector2(speed, 0);
		explodeTime = Random.Range(minExplodeTime, maxExplodetime);
	}

	void Update() {
		if(Time.time > startTime + explodeTime) {
			//TODO explosion animation and sound
			Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, bombRadius);
			foreach(Collider2D coll in colliders) {
				if(coll.gameObject.layer == 9) {
					coll.gameObject.GetComponent<MeteorController>().DoHit();
				}
				if(coll.tag == "Player") {
					coll.gameObject.GetComponent<PlayerController>().DoDamage(damage);
				}
			}
		}

	}



	public void goLeft() {
		rb.velocity = -rb.velocity;
	}
}
