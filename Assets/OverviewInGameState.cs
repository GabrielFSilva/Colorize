using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OverviewInGameState : MonoBehaviour 
{
	public InGameSceneManager		gameSceneManager;

	public GFSCustomButton			overViewButton;

	public List<GFSCustomButton>	zoomButtonsList;
	public List<UISprite>			zoomSpritesList;
	public int						selectedZoom = 1;

	public List<GFSCustomButton>	movementButtonsList;
	public float 					cameraSpeed;
	
	public List<bool>				isPressedList;
	public PlayerCamera				playerCamera;


	void Update()
	{
		if (isPressedList[0])
			playerCamera.overviewModePosition.y += selectedZoom*cameraSpeed*Time.deltaTime;
		if (isPressedList[1])
			playerCamera.overviewModePosition.y -= selectedZoom*cameraSpeed*Time.deltaTime;
		if (isPressedList[2])
			playerCamera.overviewModePosition.x -= selectedZoom*cameraSpeed*Time.deltaTime;
		if (isPressedList[3])
			playerCamera.overviewModePosition.x += selectedZoom*cameraSpeed*Time.deltaTime;
	}
	void OnEnable()
	{
		overViewButton.onClick += OverviewButtonClicked;
		foreach (GFSCustomButton button in zoomButtonsList)
			button.onClick += ZoomButtonClicked;
		foreach (GFSCustomButton button in movementButtonsList)
			button.onPress += MovementZoomButtonClicked;


		UpdateZoomSprites ();

		playerCamera.overviewModePosition = new Vector3 (playerCamera.transform.position.x, playerCamera.transform.position.y, selectedZoom * -10f);
		playerCamera.cameraState = PlayerCamera.CameraState.OVERVIEW_MODE;
	}

	void OnDisable()
	{
		foreach (GFSCustomButton button in zoomButtonsList)
			button.onClick -= ZoomButtonClicked;
		foreach (GFSCustomButton button in movementButtonsList)
			button.onPress -= MovementZoomButtonClicked;
		overViewButton.onClick -= OverviewButtonClicked;

		if (playerCamera.player != null)
		{
			if (playerCamera.player.transform.position.y < playerCamera.player.yDeath)
				playerCamera.cameraState = PlayerCamera.CameraState.DEAD_PLAYER;
			else
				playerCamera.cameraState = PlayerCamera.CameraState.FOLLOWING_PLAYER;
		}
	}

	public void ZoomButtonClicked(string p_name)
	{
		selectedZoom = int.Parse (p_name.Remove (0, 4).Remove (1));
		UpdateZoomSprites ();
		playerCamera.overviewModePosition.z = selectedZoom * -10f;
	}
	public void MovementZoomButtonClicked(bool p_isPressed, string p_name)
	{
		isPressedList[int.Parse (p_name.Remove (0, 10))] = p_isPressed;
	}
	public void OverviewButtonClicked(string p_name)
	{
		gameSceneManager.ChangeToState (InGameSceneManager.InGameStates.GAME);
	}
	public void UpdateZoomSprites()
	{
		foreach (UISprite sprite in zoomSpritesList)
		{
			if (selectedZoom -1 == zoomSpritesList.IndexOf(sprite))
				sprite.spriteName = "CameraZoomSelectedButton";
			else
				sprite.spriteName = "CameraZoomButton";
		}
	}
}
