using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLight : MonoBehaviour {

	public Light light;
	public float minIntensity;
	public float maxIntensity;

	public float waitTime;
	public float strength;

	void Start() {
		if (light == null)
			light = GetComponent<Light>();
	}

	void Update () {
		StartCoroutine(Flicker());
	}

	IEnumerator Flicker() {
		light.intensity = Mathf.Lerp(light.intensity, Random.Range(minIntensity, maxIntensity), 
									strength * Time.deltaTime);

		yield return new WaitForSeconds(waitTime);
	}
}
