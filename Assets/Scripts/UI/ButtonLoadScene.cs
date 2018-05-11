using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLoadScene : MonoBehaviour {

	public void Load(int index) {
		if(index < 0) {
			Application.Quit();
		}
		else SceneManager.LoadScene(index);
	}
}
