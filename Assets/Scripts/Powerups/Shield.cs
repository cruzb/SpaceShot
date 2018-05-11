using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

	public float shieldRefill;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			other.gameObject.GetComponent<PlayerController>().RefillShield(shieldRefill);
			//TODO sound + anim?
			Destroy(gameObject);
		}
	}
}
