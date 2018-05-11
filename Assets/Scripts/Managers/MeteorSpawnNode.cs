using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawnNode : MonoBehaviour {

	public GameObject[] meteors;
	public Vector2 minVelocity;
	public Vector2 maxVelocity;
	public float minSpawnTime;
	public float maxSpawnTime;
	public float minTorque;
	public float maxTorque;
	private float lastSpawnTime;
	private float nextSpawnTime;

	void Start () {
		lastSpawnTime = Time.time;
		nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime) + lastSpawnTime; 
	}
	
	
	void Update () {
		if(Time.time > lastSpawnTime + nextSpawnTime) {
			//setup
			Transform prefab = meteors[Random.Range(0, meteors.Length)].transform;
			Vector2 velocity = new Vector2(Random.Range(minVelocity.x, maxVelocity.x), Random.Range(minVelocity.y, maxVelocity.y));
			float torque = Random.Range(minTorque, maxTorque);

			//timing
			lastSpawnTime = Time.time;
			nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime) + lastSpawnTime;

			//spawn
			Transform go = Instantiate(prefab, transform.position, Quaternion.identity);
			go.gameObject.GetComponent<Rigidbody2D>().velocity = velocity;
			go.gameObject.GetComponent<Rigidbody2D>().AddTorque(torque);
		}
	}
}
