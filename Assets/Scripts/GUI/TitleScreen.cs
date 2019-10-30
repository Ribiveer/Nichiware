using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : SceneHelper
{
	[Header("Title Screen")]
	public float spacebarDelay = 1;
	float spacebarDelayTimer;
	public TextMesh highScoreText;
	public AudioClip startClip;
	public AudioSource bgm;
	public GameObject startButton;

    protected override void Start()
    {
		if(highScoreText)
		{
			highScoreText.richText = true;
			highScoreText.text = "<b>Highscore: " + GameHandler.highScore + "</b>";
		}
		spacebarDelayTimer = spacebarDelay;
		base.Start();
    }


    protected override void Update()
    {
		base.Update();
		spacebarDelayTimer = Mathf.MoveTowards(spacebarDelayTimer, 0, GameHandler.timeStep);
		if(spacebarDelayTimer == 0)
		{
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
			{
				SpawnStartSound();
				PlopStartButton();
				GameHandler.InitialiseGame();
				timeUntilLoad = 1;
			}
		}
    }

	void PlopStartButton()
	{
		if (startButton == null)
			return;
		startButton.SetActive(false);
	}

	void SpawnStartSound()
	{
		if (startClip == null)
			return;
		GameObject startSoundObject = new GameObject();
		AudioSource startSoundSource = startSoundObject.AddComponent<AudioSource>();
		startSoundSource.clip = startClip;
		startSoundSource.loop = false;
		startSoundSource.Play();

		if (bgm == null)
			return;
		bgm.Stop();
	}
}
