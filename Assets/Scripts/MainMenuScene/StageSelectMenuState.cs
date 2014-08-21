using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class StageSelectMenuState : MonoBehaviour 
{

	public GFSCustomButton returnButton;
	public GFSCustomButton selectButton;
	public GameObject stageButtonPrefab;
	public UIGrid stageGrid;
	public int _stageAmount = 4;
	
	public List<GFSCustomButton> stageButtonsList;
	
	private int _selectStage;
	
	void Start () 
	{
	}
	void OnEnable()
	{
		returnButton.onClick += ReturnButtonClicked;
		selectButton.onClick += SelectButtonClicked;
		
		CreatestageButtons ();
		
	}
	void OnDisable()
	{
		returnButton.onClick -= ReturnButtonClicked;
		selectButton.onClick -= SelectButtonClicked;
		
		foreach(GFSCustomButton button in stageButtonsList)
		{
			if (button != null)
				Destroy(button.gameObject);
		}
		stageButtonsList.Clear ();
	}
	void Update () 
	{
	}
	
	void ReturnButtonClicked (string p_name)
	{
		transform.parent.GetComponent<MainMenuSceneManager> ().ChangeToState (MainMenuSceneManager.MainMenuStates.CHAPTER_SELECT);
	}
	void StageButtonClicked(string p_name)
	{
		Debug.Log (p_name);
		_selectStage = int.Parse (p_name);
	}
	void SelectButtonClicked(string p_name)
	{
		Debug.Log ("Select Button. Stage " + _selectStage);
		MainMenuSceneManager.selectedStage = _selectStage;
		Application.LoadLevel ("IntroScene");
	}
	void CreatestageButtons ()
	{
		GameObject __temp;
		for(int i = 0; i < _stageAmount; i ++)
		{
			__temp = (GameObject)Instantiate(stageButtonPrefab);
			__temp.transform.parent = stageGrid.transform;
			__temp.transform.localScale = Vector3.one;
			__temp.name = (1+i).ToString();
			__temp.GetComponentInChildren<UILabel>().text = "Stage " + MainMenuSceneManager.selectedChapter + "-" + (1+i).ToString();
			stageButtonsList.Add(__temp.GetComponent<GFSCustomButton>());
			__temp.GetComponent<GFSCustomButton>().onClick += StageButtonClicked;
		}
		stageGrid.repositionNow = true;
	}
}
