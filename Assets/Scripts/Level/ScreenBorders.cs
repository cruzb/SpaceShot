using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBorders : MonoBehaviour {

	public Transform leftSide;
	public Transform bottomSide;
	public Transform rightSide;
	public Transform topSide;

	public bool destructionBounds;

	//TODO make sure this runs when resolution changes
	void Start () {
		Vector3 screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
		leftSide.position = new Vector3(-screenSize.x, 0, 0); //You need to consider object width and need to add or subtract some value from this value.
		bottomSide.position = new Vector3(0, -screenSize.y, 0);
		rightSide.position = new Vector3(screenSize.x, 0, 0);
		topSide.position = new Vector3(0, screenSize.y, 0);


		if (destructionBounds) {
			Vector3 size = leftSide.gameObject.GetComponent<BoxCollider2D>().size;
			size.y = 3.5f * screenSize.y;
			leftSide.gameObject.GetComponent<BoxCollider2D>().size = size;
			rightSide.gameObject.GetComponent<BoxCollider2D>().size = size;


			size = topSide.gameObject.GetComponent<BoxCollider2D>().size;
			size.x = 3.5f * screenSize.x;
			topSide.gameObject.GetComponent<BoxCollider2D>().size = size;
			bottomSide.gameObject.GetComponent<BoxCollider2D>().size = size;
		}
		else {
			Vector3 size = leftSide.gameObject.GetComponent<BoxCollider2D>().size;
			size.y = 2 * screenSize.y;
			leftSide.gameObject.GetComponent<BoxCollider2D>().size = size;
			rightSide.gameObject.GetComponent<BoxCollider2D>().size = size;


			size = topSide.gameObject.GetComponent<BoxCollider2D>().size;
			size.x = 2 * screenSize.x;
			topSide.gameObject.GetComponent<BoxCollider2D>().size = size;
			bottomSide.gameObject.GetComponent<BoxCollider2D>().size = size;
		}
	}

}
