using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
	static SceneTransitionManager instance;
	public float transitionTime = 1;
	public float transitionRotationSpeed = 90;

	static float transitionTimer;
	static float previousTransitionTimer;
	static float transitionSize;
	static GameObject transitionObject;
	static string pendingScene = string.Empty;
	public static bool Pending => pendingScene != string.Empty;

	public static string CurrentSceneName => SceneManager.GetActiveScene().name;
	private void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}
	void Start()
    {
		if(transform.GetChild(0))
			transitionObject = transform.GetChild(0).gameObject;

		transitionTimer = transitionTime;
		transitionSize = transitionObject.transform.localScale.x;
    }

    void Update()
    {
		previousTransitionTimer = transitionTimer;
		transitionTimer = Mathf.MoveTowards(transitionTimer, Pending?0:transitionTime, Time.deltaTime);
		transitionObject.transform.localScale = Vector3.one * transitionTimer / transitionTime * transitionSize;
		transitionObject.transform.rotation = Quaternion.Euler(0,0,Time.time * transitionRotationSpeed);

		if (Pending && transitionTimer == 0 && previousTransitionTimer == 0)
		{
			LoadPending();
		}

    }
	void LoadPending()
	{
		Debug.Log("Loading pending scene " + pendingScene);
		SceneManager.LoadScene(pendingScene);
		pendingScene = string.Empty;
	}

	public static void LoadScene(string name, bool transition = true)
	{
		if(!transition)
		{
			Debug.Log("Loading " + name + " instantly");
			SceneManager.LoadScene(name);
			return;
		}

		pendingScene = name;
	}
}
