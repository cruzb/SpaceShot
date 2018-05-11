using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	//movement
	//public float maxTurnSpeed;
	public float lowSpeed;
	public float defaultSpeed;
	public float highSpeed;
	public float turnSpeed;
	public bool canStrafe;
	public bool canBoost;

	private Vector2 velocity;
	private Rigidbody2D rb;

	/////////////////////////////////

	//combat
	public PlaySound shotSoundPlayer;
	public Transform firePosition;
	public GameObject projectile;
	public GameObject projectileAlt;
	public float coneOfFire;
	public short numProjectilePerShot;
	public float fireRate;
	public float fireRateAlt;
	public int maxAmmo;
	public int maxAmmoAlt;
	public int startingAmmo;
	public int startingAmmoAlt;
	public bool rechargeAmmo;
	public float rechargeRate;
	public bool infiniteAmmo;
	public int reloadAmt;
	public int reloadAmtAlt;

	private float lastShotTime;
	private float lastShotTimeAlt;
	private float lastRefillTime;
	private int ammoCount;
	private int ammoCountAlt;
	//bounce shot
	//explot shot


	//defense
	public float maxShield;
	public bool canOverShield;

	public float shield;


	//ui
	public Text shieldText;
	public Text ammoText;
	


	void Start () {
		shotSoundPlayer = GetComponent<PlaySound>();
		rb = GetComponent<Rigidbody2D>();
		//rb.velocity = new Vector2(0, defaultSpeed);
		lastShotTime = 0;
		lastRefillTime = 0;
		ammoCount = startingAmmo;
		ammoCountAlt = startingAmmoAlt;
		shield = maxShield;

		Text[] texts = GameObject.FindGameObjectWithTag("PlayStateUI").GetComponents<Text>();
		foreach(Text t in texts) {
			if (t.name == "ShieldText")
				shieldText = t;
			else if (t.name == "AmmoText")
				ammoText = t;
		}
		ammoText.text = ammoCount.ToString();
		shieldText.text = (100 * (shield / maxShield)).ToString("#.00");
	}
	
	void Update () {
		//fire
		if (Input.GetKey(KeyCode.Space)) {
			if (Time.time > 1/fireRate + lastShotTime && ammoCount > 0) {
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
		if (rechargeAmmo) {
			if(Time.time > 1/rechargeRate + lastRefillTime && ammoCount < maxAmmo) {
				lastRefillTime = Time.time;
				ammoCount++;
			}
		}





		//handle death
		if(shield < 0) {

		}
	}

	void FixedUpdate() {
		//rotate
		Quaternion rotation = transform.rotation;
		float z = rotation.eulerAngles.z;
		z -= Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
		rotation = Quaternion.Euler(0, 0 ,z);
		transform.rotation = rotation;
		//transform.Rotate(0, 0, -Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime);

		rb.AddForce(transform.up * 5);
		Vector2 vel = rb.velocity;
		if (Input.GetKey(KeyCode.W)) {
			if(vel.magnitude > highSpeed) {
				vel = vel.normalized * highSpeed;
				rb.velocity = vel;
			}
		}
		else if (Input.GetKey(KeyCode.S)) {
			if (vel.magnitude > lowSpeed) {
				vel = vel.normalized * lowSpeed;
				rb.velocity = vel;
			}
		}
		else {
			if (vel.magnitude > defaultSpeed) {
				vel = vel.normalized * defaultSpeed;
				rb.velocity = vel;
			}
		}
	}


	//public functions


	public void ReloadPrimary(short size) {
		if (size == 3) {
			ammoCount += 3 * reloadAmt;
		}
		else if (size == 2) {
			ammoCount += 2 * reloadAmt;
		}
		else if(size == 1) {
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

	public float GetShield() {
		return shield;
	}



	public void DoDamage(float hit) {
		shield -= hit;
		shieldText.text = (100 * (shield / maxShield)).ToString("#.00");

		Camera.main.GetComponent<CameraShake>().ShakeCamera();
		//TODO animation / sound / screen edge color or shake?
	}


	public void RefillShield(float amount) {
		shield += amount;

		if (!canOverShield) {
			if(shield > maxShield) {
				shield = maxShield;
			}
		}

		shieldText.text = (100 * (shield / maxShield)).ToString("#.00");
	}
}
