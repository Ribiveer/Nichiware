using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Microgame : SceneHelper
{
	[Header("Microgame")]
	protected bool winOnIdle = true;
	[Space]
	public List<WinLoseChange> winLoseChanges;

	protected GameState currentState;

	[System.Serializable]
	public struct WinLoseChange
	{
		public GameObject gameObject;
		public GameState gameState;
		public WinLoseChange(GameObject gameObject, GameState gameState){
			this.gameObject = gameObject;
			this.gameState = gameState;
		}
	}
	public enum GameState
	{
		OnGoing, Won, Lost
	}
	protected virtual void Win()
	{
		GoIdle(GameState.Won);
	}

	protected void Lose()
	{
		GoIdle(GameState.Lost);
	}
	
	protected void GoIdle(GameState gameState)
	{
		if (gameState == GameState.OnGoing)
			return;
		ChangeGameState(gameState);
		timer = timeUntilLoad - GameHandler.IdleTime;
		GameHandler.lostLastGame = gameState == GameState.Lost;
	}

    protected override void Start()
    {
		ShowTitle();

		ChangeGameState(GameState.OnGoing);
		timeUntilLoad = GameHandler.TimeUntilIdle + GameHandler.IdleTime;

		base.Start();
    }
	
	void ShowTitle()
	{
		GameObject title = new GameObject();
		TextMesh titleText = title.AddComponent<TextMesh>();
		GUIPopdown titlePopdown = title.AddComponent<GUIPopdown>();
		Wobble titleWobble = title.AddComponent<Wobble>();
		SetTitleText(ref titleText);
		SetTitlePopdown(ref titlePopdown);
		SetTitleWobble(ref titleWobble);
	}

	void SetTitleWobble(ref Wobble titleWobble)
	{
		titleWobble.pulseSpeed = 10;
		titleWobble.pulseIntensity = 0.25f;
		titleWobble.rotateSpeed = 5;
		titleWobble.rotateIntensity = 15;
	}
	void SetTitlePopdown(ref GUIPopdown titlePopdown)
	{
		titlePopdown.affectedBySpeed = true;
		titlePopdown.disappearDelay = 1f;
		titlePopdown.disappearDistance = -5;
		titlePopdown.lerpSpeed = 10;
	}
	void SetTitleText(ref TextMesh titleText)
	{
		titleText.text = SceneTransitionManager.CurrentSceneName;
		titleText.characterSize = 0.02f;
		titleText.anchor = TextAnchor.MiddleCenter;
		titleText.alignment = TextAlignment.Center;
		titleText.fontSize = 500;
		titleText.fontStyle = FontStyle.Bold;
		titleText.richText = true;
		titleText.color = Color.black;
	}

	protected void ChangeGameState(GameState newState)
	{
		CleanUpGameStateChanges();
		currentState = newState;
		foreach (WinLoseChange winLoseChange in winLoseChanges)
		{
			winLoseChange.gameObject.SetActive(currentState == winLoseChange.gameState);
		}
	}
	void CleanUpGameStateChanges()
	{
		winLoseChanges.RemoveAll(change => change.gameObject == null);
	}
	// Update is called once per frame
	protected override void Update()
    {
		base.Update();
		if(timer >= GameHandler.TimeUntilIdle)
		{
			if(currentState == GameState.OnGoing)
			{
				if (winOnIdle)
				{
					Win();
				} else
				{
					Lose();
				}
			}
		}
    }
}
