using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnOnOriginDistance : MonoBehaviour
{
	public float minDistance = 10;
    void Start()
    {
        
    }


    void Update()
    {
        if(transform.position.x * transform.position.x + transform.position.y * transform.position.y > minDistance * minDistance)
		{
			Destroy(gameObject);
		}
    }
}
