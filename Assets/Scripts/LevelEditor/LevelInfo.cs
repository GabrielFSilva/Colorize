using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelInfo : MonoBehaviour 
{
	public static List<Platform> 		platformsList;
	public static Player				player;
	public static Vector3				playerSpawnPosition;

	public static List<TutorialTrigger>	tutorialTriggersList;

	public static EnergySphere			energySphere;

	void Start()
	{
		platformsList = new List<Platform>(GameObject.FindObjectsOfType<Platform>());
		playerSpawnPosition = GameObject.Find ("Player").transform.position;
		player = GameObject.Find ("Player").GetComponent<Player> ();

		tutorialTriggersList = new List<TutorialTrigger>(GameObject.FindObjectsOfType<TutorialTrigger>());

		energySphere = GameObject.Find ("EnergySphere").GetComponent<EnergySphere> ();
	}

	public void ClearInfo()
	{
		platformsList.Clear ();
		playerSpawnPosition = Vector3.zero;
	}

	public void RestartStage()
	{
		player.transform.position = playerSpawnPosition;
		player.rigidbody2D.isKinematic = true;
		player.rigidbody2D.isKinematic = false;

		foreach (Platform plat in platformsList)
			plat.ChangePlatformType (plat.platformStartType);

		foreach (TutorialTrigger trigger in tutorialTriggersList)
			trigger.isActive = true;
	}
}
