using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
	public static int score = 0;
	public static int highScore = 0;
	const float timeUntilIdle = 10;
	const float idleTime = 2;
	public static float TimeUntilIdle => timeUntilIdle / speed;
	public static float IdleTime => idleTime / speed;
	public static float speed = 1;
	public static float MusSpeed => Mathf.Lerp(1, maxMusSpeed, (speed - 1) / maxSpeed);
	public const int initialLives = 4;
	public static int lives = initialLives;
	public static bool lostLastGame = false;

	public static float timeStep => Time.deltaTime * speed;
	public static float fixedTimeStep => Time.fixedDeltaTime * speed;

	public static void InitialiseGame()
	{
		lives = initialLives;
		speed = 1;
		score = 0;
	}

	public static void GameOver()
	{
		speed = 1;
		if (score > highScore)
			highScore = score;

		SaveManager.SaveHighScore();
	}

	public const float speedIncrements = 0.05f;
	public const float maxSpeed = 2;
	public const float maxMusSpeed = 2;
	public static void IncreaseSpeed()
	{
		speed = Mathf.MoveTowards(speed, maxSpeed, speedIncrements);
	}
	public static void LoseLife()
	{
		if (lives <= 0)
			return;
		lives--;
	}
}