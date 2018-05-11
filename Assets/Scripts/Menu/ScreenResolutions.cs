using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenResolutions : MonoBehaviour {
	Resolution[] resolutions;
	public Dropdown dropdownMenu;
	/*
	void Start() {
		resolutions = Screen.resolutions;
		dropdownMenu.onValueChanged.AddListener(delegate { Screen.SetResolution(resolutions[dropdownMenu.value].width, resolutions[dropdownMenu.value].height, false); });
		for (int i = 0; i < resolutions.Length; i++) {
			dropdownMenu.options[i].text = resolutions[i].ToString();
			dropdownMenu.value = i;
			dropdownMenu.options.Add(new Dropdown.OptionData(dropdownMenu.options[i].text));

		}
	}*/

	private void Start() {
		resolutions = Screen.resolutions;
		List<string> options = new List<string>();

		foreach (Resolution r in resolutions) {
			options.Add(r.ToString());
		}

		dropdownMenu.AddOptions(options);
	}
}
