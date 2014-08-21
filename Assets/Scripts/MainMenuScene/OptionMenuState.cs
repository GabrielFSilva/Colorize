using UnityEngine;
using System.Collections;

public class OptionMenuState : MonoBehaviour 
{
	public GFSCustomButton returnButton;
	void Start () 
	{
	
	}
	void OnEnable()
	{
		returnButton.onClick += returnButtonClicked;
	}
	void OnDisable()
	{
		returnButton.onClick -= returnButtonClicked;
	}
	void Update () 
	{
	}

	void returnButtonClicked (string p_name)
	{
		transform.parent.GetComponent<MainMenuSceneManager> ().ChangeToState (MainMenuSceneManager.MainMenuStates.TITLE);
	}
}
