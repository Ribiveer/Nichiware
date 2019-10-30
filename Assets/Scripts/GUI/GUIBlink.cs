using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIBlink : MonoBehaviour
{
	public float delayToBlink = 1;
	float delayTimer;
	public float blinkTime = 0.5f;
	float blinkTimer;
	SpriteRenderer sr;
    void Start()
    {
		sr = GetComponent<SpriteRenderer>();
		blinkTimer = blinkTime;
		delayTimer = delayToBlink;
    }

    // Update is called once per frame
    void Update()
    {
		delayTimer = Mathf.MoveTowards(delayTimer, 0, GameHandler.timeStep);

		if(delayTimer == 0)
		{
			blinkTimer -= GameHandler.timeStep;
			if (blinkTimer <= 0)
			{
				blinkTimer += blinkTime;
				sr.enabled = !sr.enabled;
			}
		}
    }
}
