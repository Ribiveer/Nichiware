using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIPopup : MonoBehaviour
{
	float startY;
	
	public float appearDistance;
	public float appearDelay = 0;
	float delayTimer;
	public float lerpSpeed = 10;
	public bool affectedBySpeed;
    // Start is called before the first frame update
    void Start()
    {
		startY = transform.position.y;
		transform.position = transform.position + Vector3.up * appearDistance;
		delayTimer = appearDelay;
    }

    // Update is called once per frame
    void Update()
    {
		float timestep = affectedBySpeed ? GameHandler.timeStep : Time.deltaTime;
		delayTimer = Mathf.MoveTowards(delayTimer, 0, timestep);
		if(delayTimer == 0)
		{
			transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, startY, timestep * lerpSpeed));
		}
    }
}