using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	//defense
	public int maxHealth;
	public int health;


	//ui
	public Text healthText;

	void Start() {
		health = maxHealth;

		Text[] texts = GameObject.FindGameObjectWithTag("PlayStateUI").GetComponents<Text>();
		foreach (Text t in texts) {
			if (t.name == "ShieldText")
				healthText = t;
		}
		healthText.text = (100 * (health / maxHealth)).ToString("###");
	}




	public float GetHealth() {
		return health;
	}

	public void DoDamage(int hit) {
		health -= hit;
		healthText.text = (100 * (health / maxHealth)).ToString("###");

		Camera.main.GetComponent<CameraShake>().ShakeCamera();
		//TODO animation / sound / screen edge color or shake?
	}

	public void RefillHealth(int amount) {
		health += amount;
		health = Mathf.Clamp(health, 0, maxHealth);
		healthText.text = (100 * (health / maxHealth)).ToString("###");
	}
}
