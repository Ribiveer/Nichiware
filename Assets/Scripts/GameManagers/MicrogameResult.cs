using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrogameResult : SceneHelper
{
	[Header("Microgame Result")]
	public GameObject life;
	public float lifeHeight = 0;
	public float lifeArea = 8;
	public float lifeOffset = 0.5f;
	public float lifeKillSpeed = 1;
	[Space]
	public Sprite happyFace;
	public Sprite sadFace;
	[Space]
	public string gameOverScene = "ERROR";

    protected override void Start()
    {

		Vector3 origin = new Vector3(-lifeArea * 0.5f, lifeHeight, 0);
		float spacing = lifeArea / (GameHandler.initialLives - 1);
		for (int i = 0; i < GameHandler.lives; i++)
		{
			GameObject thislife = Instantiate(life, origin + Vector3.right * spacing * i, Quaternion.identity, this.transform);

			SineBop thisBop = thislife.GetComponentInChildren<SineBop>();
			GUIPopdown thisPopdown = thislife.GetComponentInChildren<GUIPopdown>();
			Transform thisFace = thislife.transform.Find("Sprite").Find("Face");
			SpriteRenderer thisFaceSprite = thisFace.GetComponent<SpriteRenderer>();

			thisFaceSprite.sprite = GameHandler.lostLastGame ? sadFace : happyFace;
			thisBop.offset = i * lifeOffset;
			thisPopdown.disappearDelay += (GameHandler.initialLives - i) * lifeOffset / thisBop.speed;

			if (i == GameHandler.lives - 1)
				latestLife = thislife;
		}
		Debug.Log("Did the result showing!");
		if(GameHandler.lostLastGame)
		{
			GameHandler.LoseLife();
			if (GameHandler.lives == 0)
				nextScene = gameOverScene;
		}
		base.Start();
	}

	GameObject latestLife;

	protected override void Update()
    {
		base.Update();
		
		if(GameHandler.lostLastGame)
		{
			if(timer >= timeUntilLoad * 0.5f)
			{
				latestLife.transform.localScale = new Vector3(
					Mathf.MoveTowards(latestLife.transform.localScale.x, 0, GameHandler.timeStep * lifeKillSpeed),
					Mathf.MoveTowards(latestLife.transform.localScale.y, 0, GameHandler.timeStep * lifeKillSpeed),
					Mathf.MoveTowards(latestLife.transform.localScale.z, 0, GameHandler.timeStep * lifeKillSpeed)
					);
			}
		}
    }
}
