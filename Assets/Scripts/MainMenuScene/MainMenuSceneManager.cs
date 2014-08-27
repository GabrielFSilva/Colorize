﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenuSceneManager : MonoBehaviour 
{
	public static int selectedChapter = 1;
	public static int selectedStage = 1;

	public enum MainMenuStates
	{
		TITLE,
		OPTIONS,
		INFO,
		CHAPTER_SELECT,
		STAGE_SELECT
	}

	public MainMenuStates currentState = MainMenuStates.TITLE;

	public List<GameObject> panelsList;
	public List<GameObject> statesGOList;

	public OptionMenuState optionMenuState;


	// Use this for initialization
	void Start () 
	{
		Debug.Log ("starting scene");
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	void OnEnable()
	{
		EnableObjects (currentState);
		ChaptersManager.GetInstance ();
	}

	public void ChangeToState (MainMenuStates p_futureState)
	{
		if (currentState == p_futureState)
			return;

		EnableObjects (p_futureState);
	}

	void EnableObjects(MainMenuStates p_futureState)
	{
		panelsList [(int)currentState].SetActive (false);
		statesGOList [(int)currentState].SetActive (false);
		currentState = p_futureState;
		panelsList [(int)currentState].SetActive (true);
		statesGOList [(int)currentState].SetActive (true);
	}
}
