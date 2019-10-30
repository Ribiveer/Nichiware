using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobble : MonoBehaviour
{
	public float pulseSpeed = 1;
	public float pulseIntensity;
	public float rotateSpeed = 1;
	public float rotateIntensity;

	float timer;

    void Update()
    {
		timer += GameHandler.timeStep;
		transform.localScale = Vector3.one * (1 + Mathf.Sin(timer * pulseSpeed) * pulseIntensity);
		transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(timer * rotateSpeed) * rotateIntensity);
	}
}
