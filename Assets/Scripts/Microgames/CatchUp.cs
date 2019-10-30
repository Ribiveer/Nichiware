using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchUp : Microgame
{
	[Header("Catch Up!")]
	public GameObject yuuko;
	public GameObject mio;

	public float mioAccelerateGravity = 40;
	public float mioIdleGravity = 0.25f;
	public float mioJumpForce = 5;

	public float minDistance = 1.5f;

	public GameObject backgroundScroller;
	public GameObject backgroundSpawner;
	public GameObject backgroundObject;
	public float backgroundSpeed = 1;
	public float backgroundWidth = 3;

	float mioVelocity;

	protected override void Start()
	{
		if (!mio || !yuuko)
			enabled = false;

		winOnIdle = false;

		base.Start();
	}

	protected override void Update()
	{
		base.Update();


		if (currentState == GameState.OnGoing)
		{
			if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
			{
				mioVelocity = -mioJumpForce;
			}

			UpdateBackground(GameHandler.timeStep);
		}
	}

	void UpdateBackground(float timeStep)
	{
		backgroundScroller.transform.position += Vector3.right * timeStep * backgroundSpeed;
		if(backgroundSpawner.transform.position.x > -10)
		{
			Instantiate(backgroundObject, backgroundSpawner.transform.position, Quaternion.identity, backgroundScroller.transform);
			backgroundSpawner.transform.position += Vector3.left * backgroundWidth;
		}
	}

	void FixedUpdate()
	{
		if(currentState == GameState.OnGoing)
			UpdateMio(GameHandler.fixedTimeStep);
	}

	void UpdateMio(float timeStep)
	{
		float gravity = mioVelocity < 0 ? mioAccelerateGravity : mioIdleGravity;
		mioVelocity += gravity * Time.fixedDeltaTime;
		mio.transform.position += Vector3.right * mioVelocity * timeStep;

		if(Mathf.Abs(mio.transform.position.x - yuuko.transform.position.x) < minDistance)
		{
			Win();
		}
	}
}
