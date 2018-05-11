using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooter : MonoBehaviour {

	public enum WeaponType {
		Projectile,
		Targeted,
		Beam
	};

	public enum WeaponRefillStyle {
		Pickup,
		Recharge,
		Infinite,
		None
	}

	//combat
	public Transform firePosition;
	[Header("Primary Weapon")]
	public GameObject projectile;
	//public WeaponType weaponType;
	//public WeaponRefillStyle refillStyle;
	public float fireRate;
	public int maxAmmo;
	public int startingAmmo;
	public int reloadAmt;

	[Space(50)]

	[Header("Alternate Weapon")]
	public GameObject projectileAlt;
	//public WeaponType weaponTypeAlt;
	//public WeaponRefillStyle refillStyleAlt;
	public float fireRateAlt;
	public int maxAmmoAlt;
	public int startingAmmoAlt;
	public int reloadAmtAlt;
	//public float spread; currently unused
	//public short numProjectilePerShot; currently unused




	//Audio
	public PlaySound shotSoundPlayer;

	//Fire rate timing
	private float lastShotTime;
	private float lastShotTimeAlt;
	private float lastRefillTime;

	//Ammo Count
	private int ammoCount;
	private int ammoCountAlt;

	//UI
	public Text ammoText;



	private void Start() {
		shotSoundPlayer = GetComponent<PlaySound>();
		//rb.velocity = new Vector2(0, defaultSpeed);
		lastShotTime = 0;
		lastRefillTime = 0;
		ammoCount = startingAmmo;
		ammoCountAlt = startingAmmoAlt;

		Text[] texts = GameObject.FindGameObjectWithTag("PlayStateUI").GetComponents<Text>();
		foreach (Text t in texts) {
			if (t.name == "AmmoText")
				ammoText = t;
		}
		ammoText.text = ammoCount.ToString();
	}

	private void Update() {
		//fire
		if (Input.GetKey(KeyCode.Space)) {
			if (Time.time > 1 / fireRate + lastShotTime && ammoCount > 0) {
				shotSoundPlayer.Play();
				Instantiate(projectile, firePosition.position, firePosition.rotation);

				lastShotTime = Time.time;
				ammoCount--;
				ammoText.text = ammoCount.ToString();
			}
		}
		//fire2
		//TODO give seperate timer
		if (Input.GetKey(KeyCode.M)) {
			if (Time.time > 1 / fireRateAlt + lastShotTimeAlt && ammoCountAlt > 0) {
				Instantiate(projectileAlt, firePosition.position, firePosition.rotation);
				lastShotTimeAlt = Time.time;
				ammoCountAlt--;
			}
		}

		//refill ammo 
		//auto
		/*if (rechargeAmmo) {
			if (Time.time > 1 / rechargeRate + lastRefillTime && ammoCount < maxAmmo) {
				lastRefillTime = Time.time;
				ammoCount++;
			}
		}*/
	}

	public void ReloadPrimary(short size) {
		if (size == 3) {
			ammoCount += 3 * reloadAmt;
		}
		else if (size == 2) {
			ammoCount += 2 * reloadAmt;
		}
		else if (size == 1) {
			ammoCount += reloadAmt;
		}
		else {
			Debug.Log("Ammo size value in prefab not set correctly");
		}
		//TODO sound / animation
	}

	public void ReloadAlt(short size) {
		if (size == 3) {
			ammoCountAlt += 3 * reloadAmtAlt;
		}
		else if (size == 2) {
			ammoCountAlt += 2 * reloadAmtAlt;
		}
		else if (size == 1) {
			ammoCountAlt += reloadAmtAlt;
		}
		else {
			Debug.Log("AmmoAlt size value in prefab not set correctly");
		}
		//TODO sound / animation
	}

	public int GetAmmo(bool primary) {
		if (primary)
			return ammoCount;
		else return ammoCountAlt;
	}
}
