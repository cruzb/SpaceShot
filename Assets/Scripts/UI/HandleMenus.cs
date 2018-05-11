using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleMenus : MonoBehaviour {
	//Stacks dont fucking work here for some reason

	public List<GameObject> menus;
	public GameObject baseMenu;
	public GameObject playMenu;

	void Start() {
		if(baseMenu)
			menus.Add(baseMenu);
	}

	void Update() {
		if (Input.GetKeyUp(KeyCode.Escape)) {
			if (menus.Count > 1) {
				Return();
			}
			else if(menus.Count == 0 && baseMenu == null) {
				playMenu.SetActive(true);
				menus.Add(playMenu);
				Time.timeScale = 0;
			}
			else if(menus.Count == 1 && baseMenu == null) {
				Return();
			}
		}
	}

	public void ForwardMenu(GameObject go) {
		//go.SetActive(true);
		menus[menus.Count - 1].SetActive(false);
		menus.Add(go);
		menus[menus.Count - 1].SetActive(true);

	}

	public void Return() {
		if (menus.Count > 1) {
			menus[menus.Count - 1].SetActive(false);
			menus.RemoveAt(menus.Count - 1);
			menus[menus.Count - 1].SetActive(true);
		}
		else if (menus.Count == 1) {
			if(menus[0].tag == "PlayMenu") {
				menus[0].SetActive(false);
				menus.RemoveAt(0);
				Time.timeScale = 1f;
			}
		}
		else {
			Debug.Log("Error: Size goes below 1 in HandleMenus UI Manager");
		}
	}

}

