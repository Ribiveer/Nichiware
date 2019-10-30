using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineBop : MonoBehaviour
{
	public float speed = 1;
	public float intensity = 1;
	public float offset = 0;
	public Axis axis;
	public bool affectedBySpeed;

	[System.Serializable]
	public enum Axis
	{
		Y, X
	}

	Vector3 initialPostion;
    void Start()
    {
		initialPostion = transform.localPosition;
	}

    void Update()
    {
		float sinResult = Mathf.Sin(Time.time * speed * (affectedBySpeed?GameHandler.speed:1) + offset) * intensity;
		if (axis == Axis.Y)
		{
			transform.localPosition = new Vector3(transform.localPosition.x, initialPostion.y + sinResult, transform.localPosition.z);
		}
		else
		{
			transform.localPosition = new Vector3(initialPostion.x + sinResult, transform.localPosition.y, transform.localPosition.z);
		}
    }
}
