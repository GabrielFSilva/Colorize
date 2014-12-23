using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelInfo : MonoBehaviour 
{
	public static PlayerCamera					playerCamera;

	public static List<Platform> 		platformsList = new List<Platform>();
	public static Player				player;
	public static Vector3				playerSpawnPosition;

	public static List<TutorialTrigger>	tutorialTriggersList = new List<TutorialTrigger>();

	public static EnergySphere			energySphere;

	void Start()
	{
		if (Application.loadedLevelName == "LevelEditor")
		{
			platformsList = new List<Platform>(GameObject.FindObjectsOfType<Platform>());
			playerSpawnPosition = GameObject.Find ("Player").transform.position;
			player = GameObject.Find ("Player").GetComponent<Player> ();

			tutorialTriggersList = new List<TutorialTrigger>(GameObject.FindObjectsOfType<TutorialTrigger>());

			energySphere = GameObject.Find ("EnergySphere").GetComponent<EnergySphere> ();
		}
	}

	public void ClearInfo()
	{
		platformsList.Clear ();
		playerSpawnPosition = Vector3.zero;

		platformsList = new List<Platform> ();
		tutorialTriggersList = new List<TutorialTrigger> ();
		energySphere = null;

	}

	public void RestartStage()
	{
		playerCamera.cameraState = PlayerCamera.CameraState.FOLLOWING_PLAYER;
		player.transform.position = playerSpawnPosition;
		if (player.isInverted)
			player.InvertGravity();
		player.rigidbody2D.isKinematic = true;
		player.rigidbody2D.isKinematic = false;

		foreach (Shoot shoot in player.activeShootsList)
			Destroy(shoot.gameObject);
		player.activeShootsList.Clear ();

		foreach (Platform plat in platformsList)
			plat.ChangePlatformType (plat.platformStartType);

		foreach (TutorialTrigger trigger in tutorialTriggersList)
			trigger.EnableTrigger(true);
	}
}
