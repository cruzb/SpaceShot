using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public Text scoreText;

	private int score;

	void Start() {
		score = 0;
		scoreText.text = score.ToString("00000000");
	}

	public void CheckHighScore() {
		if (PlayerPrefs.HasKey("Highscore")) {
			if (score > PlayerPrefs.GetInt("Highscore"))
				PlayerPrefs.SetInt("Highscore", score);
		}
		else {
			PlayerPrefs.SetInt("Highscore", 0);
		}
	}


	public void AddPoints(int amt) {
		score += amt;
		scoreText.text = score.ToString("000000000");
	}



}
