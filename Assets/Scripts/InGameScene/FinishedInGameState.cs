using UnityEngine;
using System.Collections;

public class FinishedInGameState : MonoBehaviour 
{
	public InGameSceneManager	gameSceneManager;
	public GFSCustomButton 		nextStageButton;
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
		nextStageButton.onClick += NextStageButtonClicked;
		restartButton.onClick += RestartButtonClicked;
		mainMenuButton.onClick += MainMenuButtonClicked;
	}
	
	void OnDisable()
	{
		nextStageButton.onClick -= NextStageButtonClicked;
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
	void NextStageButtonClicked(string p_name)
	{	
		if (ignoreClicks)
			return;
		StartFade (0);
	}
	
	void RestartButtonClicked(string p_name)
	{
		if (ignoreClicks)
			return;
		StartFade (1);
	}
	
	void MainMenuButtonClicked(string p_name)
	{
		if (ignoreClicks)
			return;

		StartFade (2);
	}

	void StartFade (int p_clickCall)
	{
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
		{
			if (ChaptersManager.GetInstance().chaptersList.chapters[InGameSceneManager.selectedChapter -1].stages.Count > InGameSceneManager.selectedStage)
			{
				InGameSceneManager.selectedStage ++;
				Application.LoadLevel (Application.loadedLevelName);
			}
			else
			{
				InGameSceneManager.selectedChapter ++;
				InGameSceneManager.selectedStage = 1;
				Application.LoadLevel (Application.loadedLevelName);
			}
		}
		else if (clickCall == 1)
			Application.LoadLevel (Application.loadedLevelName);
		else if (clickCall == 2)
			Application.LoadLevel ("MainMenuScene");
	}
}
