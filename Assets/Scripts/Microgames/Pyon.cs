using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pyon : Microgame
{
	[Header("Pyon!")]
	public GameObject annaka;
	public float annakaJumpPower = 20;
	public float annakaGravity = 75;

	float annakaHeight = 0;
	float annakaVelocity = 0;
	float annakaPosition
	{
		get
		{
			return annaka.transform.position.y;
		}
		set
		{
			annaka.transform.position = new Vector3(annaka.transform.position.x, value, annaka.transform.position.z);
		}
	}
	Animator annakaAnim;
	BoxCollider2D annakaCollider;
	bool AnnakaInAir
	{
		get
		{
			return annakaAnim.GetBool("InAir");
		}
		set
		{
			annakaAnim.SetBool("InAir", value);
		}
	}

	public GameObject[] obstacles;
	public float minObstacleSpawnTime = 0.5f;
	public float maxObstacleSpawnTime = 1.25f;
	public float obstacleSpeed = 5;

	List<GameObject> currentObstacles = new List<GameObject>();
	float obstacleTimer;

	protected override void Start()
	{
		if (!annaka)
			enabled = false;

		winOnIdle = true;
		annakaHeight = annakaPosition;

		annakaAnim = annaka.GetComponent<Animator>();
		annakaCollider = annaka.GetComponent<BoxCollider2D>();

		base.Start();
	}
	protected override void Update()
	{
		if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !AnnakaInAir)
		{
			annakaVelocity = annakaJumpPower;
			AnnakaInAir = true;
		}

		base.Update();
	}
	void FixedUpdate()
	{
		if (currentState == GameState.OnGoing)
		{
			UpdateAnnaka(GameHandler.fixedTimeStep);
			CheckObstacleSpawn(GameHandler.fixedTimeStep);
			for(int i = 0; i < currentObstacles.Count; i++)
			{
				UpdateObstacle(currentObstacles[i], GameHandler.fixedTimeStep);
			}
		}
	}

	void UpdateObstacle(GameObject obstacle, float timeStep)
	{
		obstacle.transform.position = obstacle.transform.position + Vector3.left * timeStep * obstacleSpeed;
		if(obstacle.transform.position.x <= -6)
		{
			DestroyObstacle(obstacle);
		}
		if(annakaCollider.bounds.Contains(obstacle.transform.position))
		{
			Lose();
		}
	}
	void DestroyObstacle(GameObject obstacle)
	{
		currentObstacles.Remove(obstacle);
		Destroy(obstacle);
	}

	void CheckObstacleSpawn(float timeStep)
	{
		obstacleTimer -= timeStep;
		if(obstacleTimer <= 0)
		{
			obstacleTimer += Random.Range(minObstacleSpawnTime,maxObstacleSpawnTime);
			SpawnObstacle();
		}
	}

	void SpawnObstacle()
	{
		if (obstacles.Length == 0)
			return;
		GameObject thisObstacle = Instantiate(obstacles[Random.Range(0, obstacles.Length)], new Vector2(6, (Random.value > 0.5f) ? -1.5f : 1.5f), Quaternion.identity, transform);
		currentObstacles.Add(thisObstacle);
		winLoseChanges.Add(new WinLoseChange(thisObstacle, GameState.OnGoing));
	}

	void UpdateAnnaka(float timeStep)
	{
		annakaVelocity -= annakaGravity * timeStep;
		annakaPosition += annakaVelocity * timeStep;

		if(annakaPosition <= annakaHeight)
		{
			annakaPosition = annakaHeight;
			AnnakaInAir = false;
		}
	}
}
