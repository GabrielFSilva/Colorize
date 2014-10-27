using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour 
{
	public InGameSceneManager		gameSceneManager;
	public ShootsUIManager			shootUIManager;
	public PlayerCamera				playerCamera;
	public bool 					isOnTutorialMode = false;

	public static int					currentTutorialIndex;
	public static float					currentTutorialDuration;
	public static Vector3				currentTutorialCameraPosition;

	public void ChangeTutorialMode(bool p_tutorialMode)
	{
		isOnTutorialMode = p_tutorialMode;
		foreach (Platform platform in LevelInfo.platformsList)
			platform.TutorialMode (p_tutorialMode, currentTutorialIndex);
		LevelInfo.energySphere.TutorialMode(p_tutorialMode, currentTutorialIndex);
		shootUIManager.TutorialMode (p_tutorialMode, currentTutorialIndex);

		if (p_tutorialMode)
		{
			playerCamera.tutorialModePosition = currentTutorialCameraPosition;
			playerCamera.cameraState = PlayerCamera.CameraState.TUTORIAL_MODE;
			LevelInfo.player.ResetVelocity();
			StartCoroutine (TutorialWait (currentTutorialDuration));
		}
		else
		{
			playerCamera.cameraState = PlayerCamera.CameraState.FOLLOWING_PLAYER;
		}
	}

	IEnumerator TutorialWait(float p_waitDuration)
	{
		yield return new WaitForSeconds (p_waitDuration);
		gameSceneManager.ChangeToState (InGameSceneManager.InGameStates.GAME);
	}
}
