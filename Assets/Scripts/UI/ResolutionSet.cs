using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionSet : MonoBehaviour {

	public Dropdown d;

	void Start() {
		d = GetComponent<Dropdown>();
		d.value = GetCurrentRes();
	}

	int GetCurrentRes() {
		for (int i = 0; i < Screen.resolutions.Length; i++) {
			if (Screen.currentResolution.width == Screen.resolutions[i].width &&
				Screen.currentResolution.height == Screen.resolutions[i].height) {
				
				return i;
			}
		}

		return -1;
	}

	public void Set(int index) {
		/*Resolution res = Screen.resolutions[d.value];
		Screen.SetResolution(res.width, res.height, Screen.fullScreen);
		Debug.Log(d.value);*/
		Resolution res = Screen.resolutions[index];
		Screen.SetResolution(res.width, res.height, Screen.fullScreen);
	}
}
