using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePanel : MonoBehaviour {

	public HandleMenus hm;
	public GameObject nextPanel;

	// Use this for initialization
	void Start() {
		hm = GameObject.FindGameObjectWithTag("UIManager").GetComponent<HandleMenus>();
	}

	public void NextPanel() {
		hm.ForwardMenu(nextPanel);
	}

	public void Return() {
		hm.Return();
	}
}
