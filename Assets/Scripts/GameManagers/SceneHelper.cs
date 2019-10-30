using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHelper : MonoBehaviour
{
	[Header("Scene Helper")]
	public string nextScene = "ERROR";
	[Tooltip("Set to 0 to never load")]
	public float timeUntilLoad = 0;
	public bool sceneTransition = true;
	protected float timer = 0;
	public bool affectedBySpeed;

	bool loadedNext;

	protected virtual void Start()
	{
		if(affectedBySpeed)
		{
			timeUntilLoad /= GameHandler.speed;
		}
	}
	protected virtual void Update()
    {
		UpdateTimer();
	}

	void UpdateTimer()
	{
		if (timeUntilLoad == 0)
			return;

		timer += Time.deltaTime;
		if (timer >= timeUntilLoad && !loadedNext)
		{
			LoadNextScene();
		}
	}
	protected void LoadNextScene(bool transition)
	{
		loadedNext = true;
		sceneTransition = transition;
		SceneTransitionManager.LoadScene(nextScene, sceneTransition);
	}

	protected void LoadNextScene()
	{
		loadedNext = true;
		SceneTransitionManager.LoadScene(nextScene, sceneTransition);
	}
}
