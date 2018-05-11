using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDebrisOverTime : MonoBehaviour {

	public float decaySpeed;
	
	private SpriteRenderer sr;
	private Color color;

	void Start() {
		sr = GetComponent<SpriteRenderer>();
		color = sr.color;
	}

	void Update() {
		if(sr.color.a > 0f) {
			color.a -= decaySpeed;
			sr.color = color;
		}
		else {
			Destroy(gameObject);
		}
	}
}
