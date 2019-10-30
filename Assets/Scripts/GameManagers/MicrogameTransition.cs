using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrogameTransition : SceneHelper
{
	[Header("Microgame Transition")]
	public string[] microgames;
	bool addedScore;

	public TextMesh scoreText;

	static int lastGame = -1;
    protected override void Start()
	{
		if(GameHandler.score > 0)
			GameHandler.IncreaseSpeed();
		if(microgames.Length <= 0)
		{
			enabled = false;
			return;
		}

		int gameToChoose = 0;
		do
		{
			gameToChoose = Random.Range(0, microgames.Length);
		} while (microgames.Length != 1 && gameToChoose == lastGame);
		lastGame = gameToChoose;
		nextScene = microgames[gameToChoose];


		scoreText = scoreText.GetComponent<TextMesh>();
		RefreshScoreText();

		base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
		base.Update();
		if (!addedScore && timer >= timeUntilLoad * 0.5f)
		{
			addedScore = true;
			GameHandler.score++;
			RefreshScoreText();
		}
    }

	void RefreshScoreText()
	{
		scoreText.text = GameHandler.score.ToString();
	}
}
