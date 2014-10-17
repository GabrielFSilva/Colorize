using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour 
{
	public InGameSceneManager		gameSceneManager;
	public bool 					isOnTutorialMode = false;
	public int 						currentTutorialIndex;


	public void ChangeTutorialMode(bool p_tutorialMode,int p_tutorialIndex, float p_tutorialDuration)
	{
		currentTutorialIndex = p_tutorialIndex;
		isOnTutorialMode = p_tutorialMode;
		foreach (Platform platform in LevelInfo.platformsList)
			platform.TutorialMode (p_tutorialMode, p_tutorialIndex);

		if (p_tutorialMode)
			StartCoroutine (TutorialWait (p_tutorialDuration));
	}

	IEnumerator TutorialWait(float p_waitDuration)
	{
		yield return new WaitForSeconds (p_waitDuration);
		gameSceneManager.ChangeToState (InGameSceneManager.InGameStates.GAME);
	}
}
