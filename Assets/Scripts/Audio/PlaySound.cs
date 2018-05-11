using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour {

	public AudioSource source;
	public AudioClip sound;

	void Start () {
		source = GetComponent<AudioSource>();
	}
	
	public void Play() {
		source.clip = sound;
		source.Play();
	}
}
