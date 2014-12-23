using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour 
{
	public InGameSceneManager		gameSceneManager;
	public ShootsUIManager			shootUIManager;
	public UITexture				messageTexture;
	public GFSCustomButton			okButton;
	public PlayerCamera				playerCamera;
	public bool 					isOnTutorialMode = false;
	public float					tutorialTimer = 0.0f;


	public static int					currentTutorialIndex;
	public static float					currentTutorialDuration;
	public static Vector3				currentTutorialCameraPosition;
	public static string				currentTutorialImageName;

	void Start()
	{
		okButton.onClick += delegate(string obj) 
		{

			if (isOnTutorialMode && tutorialTimer > 2.0f)
			{
				foreach (TutorialTrigger trigger in LevelInfo.tutorialTriggersList)
					if (trigger.tutorialIndex == currentTutorialIndex)
						trigger.EnableTrigger(false);

				gameSceneManager.ChangeToState (InGameSceneManager.InGameStates.GAME);
			}

		};
	}

	void Update()
	{
		if (isOnTutorialMode)
			tutorialTimer += Time.deltaTime;
	}
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

			if (currentTutorialImageName != "")
			{
				messageTexture.mainTexture = Resources.Load<Texture>("TutorialImages/" + currentTutorialImageName);
				messageTexture.gameObject.SetActive(true);
			}
			else
				messageTexture.gameObject.SetActive(false);

			tutorialTimer = 0.0f;
		}
		else
		{
			playerCamera.cameraState = PlayerCamera.CameraState.FOLLOWING_PLAYER;
			messageTexture.gameObject.SetActive(false);
		}
	}


}
