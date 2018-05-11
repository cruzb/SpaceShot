using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RebindButton : MonoBehaviour {

	public InputManager inputManager;
	public int controlEnum;
	public Button button;

	private bool waitingForKey;

	void Start() {
		inputManager = GameObject.FindGameObjectWithTag("InputManager").GetComponent<InputManager>();
		button = GetComponent<Button>();
		button.GetComponentInChildren<Text>().text = PlayerPrefs.GetInt(controlEnum.ToString()).ToString();
	}

	private void Awake() {
		
	}

	private void Press() {
		waitingForKey = true;
	}

	private void Update() {
		if (waitingForKey) {
			foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode))) {
				if (Input.GetKeyDown(kcode)) {
					inputManager.SetKey(controlEnum, kcode);
					button.GetComponentInChildren<Text>().text = kcode.ToString();
					waitingForKey = false;
					break;
				}
			}
		}
	}
}
