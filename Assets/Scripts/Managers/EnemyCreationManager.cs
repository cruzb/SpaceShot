using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreationManager : MonoBehaviour {

	public float timeBetweenStages;

	public Transform[] meteors;
	public int recentShipCount;
	public int maxRecentShips;
	public float recentTime;

	public float meteorSpawnRate;
	private float meteorLastSpawnTime;

	private float lastRecentShipTime;
	private Vector3 screenSize;
	private float lastStageTime;
	private int stage;


	void Start () {
		recentShipCount = 0;
		screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
		stage = 0;
		lastStageTime = Time.time;

		meteorLastSpawnTime = 0;
		lastRecentShipTime = Time.time;
	}


	public bool AttemptShipSpawn() {
		if (recentShipCount >= maxRecentShips)
			return false;
		recentShipCount++;
		return true;
	}


	
	void Update () {

		if(Time.time > lastRecentShipTime + recentTime) {
			recentShipCount--;
			if (recentShipCount < 0)
				recentShipCount = 0;
		}

		if(Time.time - lastStageTime > timeBetweenStages) {
			stage++;
		}

		switch (stage) {
			case 0: //grey meteors
				if(Time.time > meteorLastSpawnTime + 1 / meteorSpawnRate)
					CreateOffScreen(meteors[Random.Range(0, 1)], false);
				break;
			case 1: //grey or brown meteors
				if (Time.time > meteorLastSpawnTime + 1 / meteorSpawnRate)
					CreateOffScreen(meteors[Random.Range(0, 3)], false);
				break;
			case 2: //meteors + enemyship1
				if (Time.time > meteorLastSpawnTime + 1 / meteorSpawnRate)
					CreateOffScreen(meteors[Random.Range(0, 3)], false);
				break;
			case 3:
				if (Time.time > meteorLastSpawnTime + 1 / meteorSpawnRate)
					CreateOffScreen(meteors[Random.Range(0, 3)], false);
				break;
			case 4:
				if (Time.time > meteorLastSpawnTime + 1 / meteorSpawnRate)
					CreateOffScreen(meteors[Random.Range(0, 3)], false);
				break;
			case 5:
				if (Time.time > meteorLastSpawnTime + 1 / meteorSpawnRate)
					CreateOffScreen(meteors[Random.Range(0, 3)], false);
				break;
			default:
				Debug.Log("Reached default case in EnemyCreationManager: " + stage);
				break;
		}
	}

	void CreateOffScreen(Transform prefab, bool isShip) {
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

		//if no ai for obj initVel
		if (!isShip) {
			//random straight or angled movement
			if (Random.value > 0.35f) { //angled 60% of the time
				Vector2 vel = pos * -1;

				switch (side) {
					case 0://top
						//direction
						vel.x = vel.x * Random.Range(-screenSize.x + 2, screenSize.x - 2);
						
						//magnitude
						vel = vel.normalized;
						vel = vel * Random.Range(0.25f, 2f);
						break;
					case 1://left
						   //direction
						vel.y = Random.Range(-screenSize.y + 1.5f, screenSize.y - 1.5f);
						
						//magnitude
						vel = vel.normalized;
						vel = vel * Random.Range(0.25f, 2f);
						break;
					case 2://bottom
						
						//direction
						vel.x = Random.Range(-screenSize.x + 2, screenSize.x - 2);

							//magnitude
						vel = vel.normalized;
						vel = vel * Random.Range(0.25f, 2f);
						break;
					case 3://right
						   //direction
						vel.y = Random.Range(-screenSize.y + 1.5f, screenSize.y - 1.5f);

						//magnitude
						vel = vel.normalized;
						vel = vel * Random.Range(0.25f, 2f);
						break;
				}
				float torque = Random.Range(-50f, 50f);

				Transform go = Instantiate(prefab, pos, Quaternion.identity);
				go.gameObject.GetComponent<Rigidbody2D>().velocity = vel;
				go.gameObject.GetComponent<Rigidbody2D>().AddTorque(torque);
				meteorLastSpawnTime = Time.time;
			}
			else {
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
				float torque = Random.Range(-50f, 50f);

				Transform go = Instantiate(prefab, pos, Quaternion.identity);
				go.gameObject.GetComponent<Rigidbody2D>().velocity = vel;
				go.gameObject.GetComponent<Rigidbody2D>().AddTorque(torque);
				meteorLastSpawnTime = Time.time;
			}
			
		}//end spawn for ship
		
	}
}
