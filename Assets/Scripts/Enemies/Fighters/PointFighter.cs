using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointFighter : MonoBehaviour {

	//Fly to a point, turn to target, shoot, repeat

	//movement
	public Transform target;
	public float minSpeed;
	public float maxSpeed;
	public float radius;
	public float timeWait;
	public float rotateSpeed;

	private float beginRotationTime;
	private float speed;
	private ushort stage;
	private Vector3 movePoint;

	//shooting
	public GameObject projectile;
	public Transform firePosition;
	public ushort numShots;
	public float timeBetweenShots;
	public float damage;

	private float lastShotTime;
	private ushort shotsFired;

	//death
	//audio
	//anim

	// Use this for initialization
	void Start () {
		if (target == null) {
			if (GetComponent<TargetHolder>().target == null) {
				target = GameObject.FindGameObjectWithTag("Player").transform;
				Debug.Log("ERROR: Target not assigned in script");
			}
			else target = GetComponent<TargetHolder>().target;
		}

		movePoint = GenerateMovePoint();
		speed = Random.Range(minSpeed, maxSpeed);
		lastShotTime = 0;

		shotsFired = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//move to point radius from target
		if(stage == 0) {
			Vector3 targetDir = movePoint - transform.position;
			float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg + 90f;
			Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);

			if (Time.time > beginRotationTime + 2f) {
				stage++;
			}
		}

		else if(stage == 1) {
			transform.position = Vector3.MoveTowards(transform.position, movePoint, speed);

			if(transform.position == movePoint) {
				stage++;
				beginRotationTime = Time.time;
			}
			//stage++;
		}

		//turn to
		else if(stage == 2) {
			Vector3 targetDir = target.position - transform.position;
			float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg + 90f;
			Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);

			if(Time.time > beginRotationTime + 2f) {
				stage++;
				beginRotationTime = Time.time;
			}
		}

		//shoot
		else if(stage == 3){
			if (Time.time > lastShotTime + timeBetweenShots) {
				if (shotsFired < numShots) {
					shotsFired++;
					Instantiate(projectile, firePosition.position, firePosition.rotation);
				}
				else {
					shotsFired = 0;
					stage = 0;
					movePoint = GenerateMovePoint();
				}
			}
		}

		else {
			Debug.Log("Error: Point Fighter reaches stage 3 or higher");
		}
	}


	Vector3 GenerateMovePoint() {
		//point is in quadrant opposite to player
		float quadrant;
		if(target.position.x > 0 && target.position.y > 0) {
			quadrant = 2;
		}
		else if(target.position.x > 0 && target.position.y < 0) {
			quadrant = 1;
		}
		else if(target.position.x < 0 && target.position.y > 0) {
			quadrant = 3;
		}
		else {
			quadrant = 0;
		}


		float degrees = Random.Range(0, 91);
		degrees *= quadrant;
		//now radians
		degrees = degrees * Mathf.PI / 180f;

		return new Vector3(Mathf.Cos(degrees), Mathf.Sin(degrees), 0) * Random.Range(0f, 5f);
	}
}
