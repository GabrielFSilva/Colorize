using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TitleMenuState : MonoBehaviour 
{
	public GFSCustomButton optionsButton;
	public GFSCustomButton infoButton;
	public GFSCustomButton playButton;

	public List<Transform> transforms;
	void Start () 
	{
		
	}
	void OnEnable()
	{
		optionsButton.onClick += optionsButtonClicked;
		infoButton.onClick += infoButtonClicked;
		playButton.onClick += playButtonClicked;
	}
	void OnDisable()
	{
		optionsButton.onClick -= optionsButtonClicked;
		infoButton.onClick -= infoButtonClicked;
		playButton.onClick -= playButtonClicked;
	}
	void Update () 
	{

	}

	void optionsButtonClicked(string p_name)
	{
		gameObject.transform.parent.GetComponent<MainMenuSceneManager> ().ChangeToState(MainMenuSceneManager.MainMenuStates.OPTIONS);
	}
	void infoButtonClicked(string p_name)
	{
		gameObject.transform.parent.GetComponent<MainMenuSceneManager> ().ChangeToState(MainMenuSceneManager.MainMenuStates.INFO);
	}
	void playButtonClicked(string p_name)
	{
		gameObject.transform.parent.GetComponent<MainMenuSceneManager> ().ChangeToState(MainMenuSceneManager.MainMenuStates.CHAPTER_SELECT);
	}
}
