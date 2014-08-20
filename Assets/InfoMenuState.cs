using UnityEngine;
using System.Collections;

public class InfoMenuState : MonoBehaviour 
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
		Debug.Log ("Return Clicked");
		transform.parent.GetComponent<MainMenuSceneManager> ().ChangeToState (MainMenuSceneManager.MainMenuStates.TITLE);
	}
}
