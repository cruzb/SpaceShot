using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour {

	public short ammoRefill;
	public bool isAmmoAlt;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			if (isAmmoAlt) {
				other.gameObject.GetComponent<PlayerController>().ReloadAlt(ammoRefill);
				//TODO sound + anim?
				Destroy(gameObject);
			}
			else {
				other.gameObject.GetComponent<PlayerController>().ReloadPrimary(ammoRefill);
				//TODO sound + anim?
				Destroy(gameObject);
			}
		}
	}
}
