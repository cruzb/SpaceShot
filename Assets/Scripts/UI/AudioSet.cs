using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSet : MonoBehaviour {

	public AudioMixer mixer;
	public Slider slider;

	public void SetMasterVolume(float vol) {
		vol = ((vol / slider.maxValue) * 100) - 80;
		mixer.SetFloat("MasterVolume", vol);
	}

	public void SetMusicVolume(float vol) {
		vol = ((vol / slider.maxValue) * 100) - 80;
		mixer.SetFloat("MusicVolume", vol);
	}

	public void SetEffectsVolume(float vol) {
		vol = ((vol / slider.maxValue) * 100) - 80;
		mixer.SetFloat("EffectsVolume", vol);
	}
}
