using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	public enum Controls {
		Left = 0,
		Right = 1,
		Forward = 2,
		Backward = 3,
		Shoot = 4,
		ShootAlt = 5
	};

	public Dictionary<int, KeyCode> keybinds = new Dictionary<int, KeyCode>();

	private KeyCode[] defaultBinds = new KeyCode[] { KeyCode.A, KeyCode.D, KeyCode.W, KeyCode.S, KeyCode.Space, KeyCode.LeftControl };

	void Start() {
		Settings();
	}

	void Awake() {
		DontDestroyOnLoad(gameObject);
	}

	/**
     *      Sets up the control bindings.
     **/
	void Settings() {
		/*  For each control we have, set the binding*/
		for (int i = 0; i < 6; i++) {
			/*  If the control bindings have already been set up:*/
			if (PlayerPrefs.HasKey(i.ToString())) {
				KeyCode binding = (KeyCode)PlayerPrefs.GetInt(i.ToString());
				keybinds.Add(i, binding);
			}

			/*  If they haven't:*/
			else {
				keybinds.Add(i, defaultBinds[i]);
				PlayerPrefs.SetInt(i.ToString(), (int)keybinds[i]);
			}
		}
	}


	public void SetKey(int i, KeyCode k) {
		keybinds.Add(i, k);
		PlayerPrefs.SetInt(i.ToString(), (int)k);
	}
}


	/*
	public static Dictionary<string, KeyCode> keybinds = new Dictionary<string, KeyCode>();

	public string[] inputTerms;

	public KeyCode[] defaultCodes;

	void Start() {
		InitTerms();
		InitDefault();
	}

	void InitTerms() {
		inputTerms = new string[6];
		inputTerms[0] = "Left";
		inputTerms[1] = "Right";
		inputTerms[2] = "Forward";
		inputTerms[3] = "Backward";
		inputTerms[4] = "Shoot";
		inputTerms[5] = "ShootAlt";
	}

	void InitDefault() {
		defaultCodes = new KeyCode[6];
		defaultCodes[0] = KeyCode.A;
		defaultCodes[1] = KeyCode.D;
		defaultCodes[2] = KeyCode.W;
		defaultCodes[3] = KeyCode.S;
		defaultCodes[4] = KeyCode.Space;
		defaultCodes[5] = KeyCode.LeftControl;
	}


	public void SetKeyBind(string k, KeyCode v) {
		keybinds[k] = v;
	}

	public KeyCode GetKeyBind(string s) {
		return keybinds[s];
	}

	public void SaveToFile() {

	}

	public void ReadFromFile() {

	}

	public void ResetToDefault() {

	}*/
