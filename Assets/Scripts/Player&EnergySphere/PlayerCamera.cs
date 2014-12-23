using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour 
{
	public LevelInfo levelInfo;
	public InGameSceneManager	gameSceneManager;
	public ShootsUIManager		shootUIManager;

	public Player player;
	public Vector3 tutorialModePosition;
	public Vector3 overviewModePosition;

	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;


	public enum CameraState
	{
		FOLLOWING_PLAYER,
		DEAD_PLAYER,
		TUTORIAL_MODE,
		OVERVIEW_MODE
	}

	public CameraState cameraState = CameraState.FOLLOWING_PLAYER;
	// Use this for initialization
	void Start () 
	{
		//cameraState = CameraState.FOLLOWING_PLAYER;
		player = LevelInfo.player;
		LevelInfo.playerCamera = this;
		transform.position = new Vector3 (LevelInfo.playerSpawnPosition.x, LevelInfo.playerSpawnPosition.y, transform.position.z);
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (gameSceneManager.currentState == InGameSceneManager.InGameStates.LOADING)
			return;

		if (cameraState == CameraState.FOLLOWING_PLAYER)
		{
			Vector3 point = camera.WorldToViewportPoint(player.transform.position);
			Vector3 delta = player.transform.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
			Vector3 destination = transform.position + delta;
			destination.z = -10f;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);

			if (player.transform.position.y < player.yDeath)
				cameraState = CameraState.DEAD_PLAYER;

			if (player.transform.position.y > player.yDeath * -2f)
				cameraState = CameraState.DEAD_PLAYER;
		}
		else if (cameraState == CameraState.DEAD_PLAYER)
		{
			if (player.transform.position.y < player.yDeath *2f)
			{
				shootUIManager.RestartAmmo();
				levelInfo.RestartStage();
			}

			if (player.transform.position.y > player.yDeath * 5f)
			{
				shootUIManager.RestartAmmo();
				levelInfo.RestartStage();
			}

		}
		else if (cameraState == CameraState.TUTORIAL_MODE)
		{
			Vector3 point = camera.WorldToViewportPoint(tutorialModePosition);
			Vector3 delta = tutorialModePosition - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime * 5f);
		}
		else if (cameraState == CameraState.OVERVIEW_MODE)
		{
			Vector3 point = camera.WorldToViewportPoint(overviewModePosition);
			Vector3 delta = overviewModePosition - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
			Vector3 destination = transform.position + delta;
			destination.z = overviewModePosition.z;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime * 2f);
		}

	}
}
