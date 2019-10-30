using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIPopdown : MonoBehaviour
{
	float startY;
	
	public float disappearDistance;
	public float disappearDelay = 0;
	float delayTimer;
	public float lerpSpeed = 10;
	public bool affectedBySpeed;
    // Start is called before the first frame update
    void Start()
    {
		startY = transform.position.y;
		delayTimer = disappearDelay;
    }

    // Update is called once per frame
    void Update()
    {
		float timestep = affectedBySpeed ? GameHandler.timeStep : Time.deltaTime;
		delayTimer = Mathf.MoveTowards(delayTimer, 0, timestep);
		if(delayTimer == 0)
		{
			transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, startY + disappearDistance, timestep * lerpSpeed));
		}
    }
}