using UnityEngine;
using System.Collections;

public class PausedInGameState : MonoBehaviour 
{
	public InGameSceneManager	gameSceneManager;
	public GFSCustomButton 		continueButton;
	public GFSCustomButton 		restartButton;
	public GFSCustomButton 		mainMenuButton;

	public UITexture			fadeTexture;
	
	public float 	fadeDuration;
	private bool 	ignoreClicks;
	private int		clickCall = 0;
	private bool	fadeActive = false;
	private float	fadeTimeCount;

	void OnEnable()
	{
		continueButton.onClick += ContinueButtonClicked;
		restartButton.onClick += RestartButtonClicked;
		mainMenuButton.onClick += MainMenuButtonClicked;
	}
	
	void OnDisable()
	{
		continueButton.onClick -= ContinueButtonClicked;
		restartButton.onClick -= RestartButtonClicked;
		mainMenuButton.onClick -= MainMenuButtonClicked;
	}
	void Update()
	{
		if (fadeActive)
		{
			fadeTimeCount += Time.deltaTime/fadeDuration*1.2f;
			fadeTexture.alpha = fadeTimeCount;
		}
	}
	void ContinueButtonClicked(string p_name)
	{
		gameSceneManager.ChangeToState (InGameSceneManager.InGameStates.GAME);
	}

	void RestartButtonClicked(string p_name)
	{
		StartFade (0);
	}

	void MainMenuButtonClicked(string p_name)
	{
		StartFade (1);
	}

	void StartFade (int p_clickCall)
	{
		if (ignoreClicks)
			return;

		clickCall = p_clickCall;
		ignoreClicks = true;
		fadeActive = true;
		fadeTimeCount = 0.0f;
		StartCoroutine (WaitFade ());
	}
	
	IEnumerator WaitFade()
	{
		yield return new WaitForSeconds (fadeDuration);
		ignoreClicks = false;

		if (clickCall == 0)
			Application.LoadLevel (Application.loadedLevelName);
		else if (clickCall == 1)
			Application.LoadLevel ("MainMenuScene");
	}
}
