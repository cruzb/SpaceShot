using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceShot : MonoBehaviour {

	public float initForce;
	public float lifeTime;
	public float decaySpeed;

	private float startTime;
	private float angle;

	private Rigidbody2D rb;
	private SpriteRenderer sr;
	private Color color;

	void Start() {
		Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), 
									GetComponent<Collider2D>(), true);

		startTime = Time.time;
		angle = 0;

		sr = GetComponent<SpriteRenderer>();
		rb = GetComponent<Rigidbody2D>();
		rb.AddForce(transform.up * initForce);
		color = sr.color;
	}

	void Update () {
		transform.Rotate(0,0,4,Space.World);

		if(Time.time > startTime + lifeTime) {
			if (sr.color.a > 0f) {
				color.a -= decaySpeed;
				sr.color = color;
			}
			else {
				Destroy(gameObject);
			}
		}
	}

}
