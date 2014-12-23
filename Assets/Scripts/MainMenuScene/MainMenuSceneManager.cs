using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenuSceneManager : MonoBehaviour 
{
	public static int selectedChapter = 1;
	public static int selectedStage = 1;

	//Estados
	public enum MainMenuStates
	{
		TITLE,
		OPTIONS,
		INFO,
		CHAPTER_SELECT,
		STAGE_SELECT
	}

	public MainMenuStates currentState = MainMenuStates.TITLE;

	//Controladores de estados
	public List<GameObject> panelsList;
	public List<GameObject> statesGOList;

	public OptionMenuState optionMenuState;

	//Inicializaçao
	void OnEnable()
	{
		EnableObjects (currentState);
		ChaptersManager.GetInstance ();
	}

	//Altera o estado atual
	public void ChangeToState (MainMenuStates p_futureState)
	{
		if (currentState == p_futureState)
			return;
		EnableObjects (p_futureState);
	}

	void EnableObjects(MainMenuStates p_futureState)
	{
		//Desativa o gerenciador de estado anterior
		panelsList [(int)currentState].SetActive (false);
		statesGOList [(int)currentState].SetActive (false);

		currentState = p_futureState;

		//Ativa o proximo gerenciador de estado
		panelsList [(int)currentState].SetActive (true);
		statesGOList [(int)currentState].SetActive (true);
	}
}
