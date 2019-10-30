using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoIntro : SceneHelper
{
	protected override void Start()
	{
		SaveManager.LoadHighScore();
		base.Start();
	}
}
