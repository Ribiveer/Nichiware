using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Microsong : MonoBehaviour
{
	AudioSource au;
    void Start()
    {
		au = GetComponent<AudioSource>();
    }


    void Update()
    {
		au.pitch = GameHandler.MusSpeed;
    }
}
