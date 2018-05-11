using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupCreationManager : MonoBehaviour {

	public PlayerController ply;
	public GameObject[] ammo;
	public GameObject[] shield;

	public float minAmmoSpawnTime;
	private float lastAmmoSpawnTime;

	private int plyMaxAmmo;
	private float plyMaxShield;


	//One off powers
	public GameObject Bomb;
	public GameObject MultiShot;

	private Vector3 screenSize;


	// Use this for initialization
	void Start () {
		screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
		ply = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		plyMaxAmmo = ply.maxAmmo;
		lastAmmoSpawnTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > lastAmmoSpawnTime + minAmmoSpawnTime)
			DoAmmoSpawn();
	}

	void DoAmmoSpawn() {
		float ammoRatio = (float)(ply.GetAmmo(true) / plyMaxAmmo);

		if (ammoRatio > 0.8f) {
			CreateOffScreen(ammo[0]);
		}
		else {
			int percent = Random.Range(0, 10);
			if (ammoRatio > 0.5f) {
				if (percent > 4) {
					CreateOffScreen(ammo[0]);
				}
				else if (percent > 0) {
					CreateOffScreen(ammo[1]);
				}
				else CreateOffScreen(ammo[2]);
			}
			else if(ammoRatio > 0.2f){
				if (percent > 6) {
					CreateOffScreen(ammo[0]);
				}
				else if (percent > 1) {
					CreateOffScreen(ammo[1]);
				}
				else CreateOffScreen(ammo[2]);
			}
			else {
				if (percent > 8) {
					CreateOffScreen(ammo[0]);
				}
				else if (percent > 6) {
					CreateOffScreen(ammo[1]);
				}
				else CreateOffScreen(ammo[2]);
			}
		}
	}



	void CreateOffScreen(GameObject prefab) {
		//random position to start from
		Vector2 pos = new Vector2(0, 0);
		int side = Random.Range(0, 4);
		switch (side) {
			case 0://top
				pos.x = Random.Range(-screenSize.x + 1, screenSize.x - 1);
				pos.y = screenSize.y + 1.5f;
				break;
			case 1://left
				pos.y = Random.Range(-screenSize.y + 1, screenSize.y - 1);
				pos.x = -screenSize.x - 1.5f;
				break;
			case 2://bottom
				pos.x = Random.Range(-screenSize.x + 1, screenSize.x - 1);
				pos.y = -screenSize.y - 1.5f;
				break;
			case 3://right
				pos.y = Random.Range(-screenSize.y + 1, screenSize.y - 1);
				pos.x = screenSize.x + 1.5f;
				break;
		}


		Vector2 vel = new Vector2(0, 0);
		switch (side) {
			case 0://top
				vel.y = Random.Range(-0.25f, -2f);
				break;
			case 1://left
				vel.x = Random.Range(0.25f, 2f);
				break;
			case 2://bottom
				vel.y = Random.Range(0.25f, 2f);
				break;
			case 3://right
				vel.x = Random.Range(-0.25f, -2f);
				break;
		}

		GameObject go = Instantiate(prefab, pos, Quaternion.identity);
		go.gameObject.GetComponent<Rigidbody2D>().velocity = vel;
		Debug.Log(side + "    " + vel.ToString());
		lastAmmoSpawnTime = Time.time;
	}
}
