using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : MonoBehaviour {

	public ScoreManager sm;
	public int points;

	public Transform[] smallerMeteors;
	public float rotateSpeed;
	public float damageOnHit;

	private Rigidbody2D rb;
	private PlaySound playDestructionSound;
	private SpriteRenderer sr;
	private Color color;

	private bool dying;


	void Awake() {
		rb = GetComponent<Rigidbody2D>();
		rb.AddTorque(rotateSpeed);

		playDestructionSound = GetComponent<PlaySound>();

		sr = GetComponent<SpriteRenderer>();
		color = sr.color;

		dying = false;
	}

	void Start() {
		sm = GameObject.Find("Managers/ScoreManager").GetComponent<ScoreManager>();
	}

	//TODO play sound on destroy??
	void OnTriggerEnter2D(Collider2D other) {
		if (dying) { }
		else {
			if (other.tag == "Player") {
				other.gameObject.GetComponent<PlayerController>().DoDamage(damageOnHit);
				DoHit();
			}

			else if (other.tag == "Projectile") {
				DoHit();
				Destroy(other);
			}

			else if (other.tag == "PersistentProjectile") {
				DoHit();
			}
		}
	}

	void OnBecameInvisible() {
		Destroy(gameObject);
	}

	public void DoHit() {
		sm.AddPoints(points);
		dying = true;
		for (int i = 0; i < smallerMeteors.Length; i++) {
			Transform t = Instantiate(smallerMeteors[i], transform.position, Quaternion.identity);
			MeteorSmallController s = t.gameObject.GetComponent<MeteorSmallController>();
			s.velocity = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
			if (s.velocity.magnitude == 0) {
				s.velocity = new Vector2(0.5f, -1);
			}
			s.rotateSpeed = Random.Range(2, 8);
		}
		//play explosion sound
		playDestructionSound.Play();
		//go invisible for duration of sound
		color.a = 0f;
		sr.color = color;
		//destroy object after sound plays
		Destroy(gameObject, playDestructionSound.sound.length);
	}
	
}
