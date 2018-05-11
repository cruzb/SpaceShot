using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreationManager2 : MonoBehaviour {

	public GameObject player;
	
	public GameObject[] ships;
	public Transform[] spawnNodes;
	public int[] spawnPower;
	public int zeroPower;
	public float[] timeUntilIntroduced; //must be in increasing order
	public float maxSpawnTime;
	public float minSpawnTime;

	private int maxShipsIndex;
	private float lastSpawnTime;
	private float nextSpawnTime;

	private Vector3 screenSize;
	private int currentMaxForSelect;


	void Start() {
		maxShipsIndex = -1;

		lastSpawnTime = Time.time;
		nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);

		currentMaxForSelect = 0;
	}

	void Update() {
		if(Time.time > timeUntilIntroduced[maxShipsIndex + 1]) {
			maxShipsIndex++;
		}

		if (Time.time > lastSpawnTime + nextSpawnTime) {
			for (int i = 0; i < spawnNodes.Length; i++) {
				GameObject prefab = SelectShip(Random.Range(0, currentMaxForSelect));
				if (prefab != null) {
					prefab = Instantiate(prefab, spawnNodes[i].position, Quaternion.identity);
					//assign player target here
				}
			}

			nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
		}
	}



	GameObject SelectShip(int rand) {
		int sum = zeroPower;

		if (rand < zeroPower)
			return null;

		for(int i = 0; i < maxShipsIndex; i++) {
			sum += spawnPower[i];
			if(rand < sum) {
				return ships[i];
			}
		}
		Debug.Log("Reached end of SelectShip loop in EnemyCreationManager2");
		return null;
	}

}
