using UnityEngine;
using System.Collections;

public class TitleMenuState : MonoBehaviour 
{
	public GFSCustomButton optionsButton;
	void Start () 
	{
		
	}
	void OnEnable()
	{
		optionsButton.onClick += optionsButtonClicked;
	}
	void OnDisable()
	{
		optionsButton.onClick -= optionsButtonClicked;
	}
	void Update () 
	{
	}

	void optionsButtonClicked(string p_name)
	{
		Debug.Log ("Options Clicked");
		gameObject.transform.parent.GetComponent<MainMenuSceneManager> ().ChangeToState(MainMenuSceneManager.MainMenuStates.OPTIONS);
	}
}
