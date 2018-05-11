using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullscreenSet : MonoBehaviour {

	public void Set() {
		if(Screen.fullScreen)
			Screen.SetResolution(Screen.width, Screen.height, false);
		else Screen.SetResolution(Screen.width, Screen.height, true);
	}
}
