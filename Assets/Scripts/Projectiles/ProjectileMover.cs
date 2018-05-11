using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMover : MonoBehaviour {

	public float initForce;

	private Rigidbody2D rb;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
		rb.AddForce(transform.up * initForce);
	}

}
