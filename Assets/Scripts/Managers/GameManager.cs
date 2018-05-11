using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject[] ships;
	public int shipChoice;

	public bool debug;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}










	void DoSetup() {
		Instantiate(ships[shipChoice], new Vector3(0, 0, 0), Quaternion.identity);
	}
}
