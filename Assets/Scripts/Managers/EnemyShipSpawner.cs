using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipSpawner : MonoBehaviour {

	public EnemyCreationManager ecm;
	public Transform player;
	public GameObject[] ships;
	public float[] ratios;
	public float maxInterval;
	public float minInterval;

	private float lastSpawnTime;
	private float ratioSum;

	void Start () {
		lastSpawnTime = Time.time + Random.Range(minInterval, maxInterval);

		//setup ratios for probability
		ratioSum = 0;
		foreach(float f in ratios) {
			ratioSum += f;
		}
		for(int i = 0; i < ratios.Length; i++) {
			ratios[i] = ratios[i] / ratioSum;
		}
	}
	
	void Update () {
		if (Time.time > lastSpawnTime + Random.Range(minInterval, maxInterval))
			if (ecm.AttemptShipSpawn()) {
				float r = Random.value;
				int index = 0;
				float runningTotal = 0;
				for(int i = 0; i < ratios.Length; i++) {
					if(r < runningTotal + ratios[i]) {
						index = i;
						break;
					}
				}

				GameObject go = Instantiate(ships[index], transform.position, transform.rotation);
				go.GetComponent<TargetHolder>().target = player;
				lastSpawnTime = Time.time;
			}
	}


	//every node has its own timer
	//when spawning it adds 1 to the counter in the manager
	//every x seconds the manager subtracts to prevent too many spawns in a time period
}
