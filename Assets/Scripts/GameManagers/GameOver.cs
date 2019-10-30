using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : SceneHelper
{
	[Header("Game Over")]
	public TextMesh gameOverText;
	protected override void Start()
	{
		GameHandler.GameOver();

		if(gameOverText)
		{
			gameOverText.richText = true;
			gameOverText.text = "<b>Game Over</b>\nScore: " + GameHandler.score + "\nHighscore: " + GameHandler.highScore;
		}

		base.Start();
	}
	protected override void Update()
    {
		base.Update();
    }


}
