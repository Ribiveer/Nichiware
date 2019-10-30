using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resist : Microgame
{
	[Space]
	public GameObject brain;
	Vector3 brainOrigin;
	bool BrainGrounded => brain.transform.position.y * Mathf.Sign(brainGravity) <= brainOrigin.y * Mathf.Sign(brainGravity);
	public float brainGravity;
	public float brainJumpPower;
	float brainVelocity;
	[Space]
	public GameObject toy;
	Vector3 toyOrigin;
	float toyTimer;
	public float toyOffset;
	public float toySpeed = 1;
	public float toyMoveSpace = 3;
	[Space]
	public float hitSpace = 0.1f;
	bool ToyHit => Mathf.Abs(toy.transform.position.x - brainOrigin.x) < hitSpace && BrainGrounded;
    protected override void Start()
    {
		if (!brain || !toy)
			enabled = false;

		winOnIdle = true;
		brainOrigin = brain.transform.position;
		toyOrigin = toy.transform.position;
		toy.transform.position += Vector3.right * toyOffset;

		base.Start();
    }
	protected override void Update()
	{
		if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && BrainGrounded)
		{
			brainVelocity = brainJumpPower * Mathf.Sign(brainGravity);
		}

		base.Update();
	}
	void FixedUpdate()
    {
		if(currentState == GameState.OnGoing)
		{
			UpdateBrain(GameHandler.fixedTimeStep);
			UpdateToy(GameHandler.fixedTimeStep);

			if (ToyHit)
			{
				Lose();
			}
		}
    }

	void UpdateToy(float timeStep)
	{
		toyTimer += timeStep;
		toy.transform.position = new Vector3(Mathf.Sin(toyTimer * toySpeed + toyOffset) * toyMoveSpace, toy.transform.position.y, toy.transform.position.z);
	}

	void UpdateBrain(float timeStep)
	{
		brainVelocity -= brainGravity * timeStep;

		brain.transform.position += Vector3.up * brainVelocity * timeStep;

		if (BrainGrounded)
		{
			brain.transform.position = new Vector3(brain.transform.position.x, brainOrigin.y, brain.transform.position.z);
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawLine(new Vector3(brainOrigin.x + hitSpace * 0.5f, 100), new Vector3(brainOrigin.x + hitSpace * 0.5f, -100));
		Gizmos.DrawLine(new Vector3(brainOrigin.x - hitSpace * 0.5f, 100), new Vector3(brainOrigin.x - hitSpace * 0.5f, -100));
	}
}
