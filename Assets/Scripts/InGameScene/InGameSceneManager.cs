using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class InGameSceneManager : MonoBehaviour 
{

	public static int selectedChapter = 1;
	public static int selectedStage = 1;
	
	public enum InGameStates
	{
		LOADING,
		GAME,
		PAUSE,
		FINISHED,
		TUTORIAL

	}

	public InGameStates currentState = InGameStates.LOADING;

	public LevelLoader 				levelLoader;
	public PauseManager				pauseManager;
	public TutorialManager			tutorialManager;
	public List<GameObject> 		panelsList;
	public List<GameObject> 		statesGOList;

	public int						currentTutorialIndex;
	public float					currentTutorialDuration;

	void Awake()
	{
		//levelLoader.LoadLevel ();
	}
	// Use this for initialization
	void Start () 
	{
		Debug.Log ("starting inGame scene. Stage " + selectedChapter + "-" + selectedStage);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	void OnEnable()
	{
		EnableObjects (currentState);
	}
	
	public void ChangeToState (InGameStates p_futureState)
	{
		if (currentState == p_futureState)
			return;

		EnableObjects (p_futureState);
	}
	
	void EnableObjects(InGameStates p_futureState)
	{
		if (currentState == InGameStates.TUTORIAL)
			tutorialManager.ChangeTutorialMode(false,currentTutorialIndex,currentTutorialDuration);

		panelsList [(int)currentState].SetActive (false);
		statesGOList [(int)currentState].SetActive (false);
		currentState = p_futureState;
		panelsList [(int)currentState].SetActive (true);
		statesGOList [(int)currentState].SetActive (true);

		if (pauseManager.isPaused)
		{
			if (currentState == InGameStates.GAME)
				pauseManager.PauseGame(false);
		}
		else
		{
			if (currentState == InGameStates.PAUSE || currentState == InGameStates.TUTORIAL || currentState == InGameStates.FINISHED)
				pauseManager.PauseGame(true);
		}

		if (currentState == InGameStates.TUTORIAL)
			tutorialManager.ChangeTutorialMode(true,currentTutorialIndex,currentTutorialDuration);
	}
}
