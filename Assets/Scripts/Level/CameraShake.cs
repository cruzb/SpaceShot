using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;

	// How long the object should shake for.
	public float shakeDuration;

	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount;
	public float decreaseFactor;

	private float shakeBaseDuration;
	Vector3 originalPos;
	bool shaking;

	void Awake() {
		shakeBaseDuration = shakeDuration;
		if (camTransform == null) {
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
		originalPos = camTransform.localPosition;
		shaking = false;
	}

	public void ShakeCamera() {
		shaking = true;
	}

	void Update() {
		if (shaking) {
			if (shakeDuration > 0) {
				camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

				shakeDuration -= Time.deltaTime * decreaseFactor;
			}
			else {
				shakeDuration = shakeBaseDuration;
				camTransform.localPosition = originalPos;
				shaking = false;
			}
		}
	}
}
