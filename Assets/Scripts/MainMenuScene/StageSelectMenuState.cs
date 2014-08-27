using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class StageSelectMenuState : MonoBehaviour 
{

	public GFSCustomButton 				returnButton;
	public GFSCustomButton 				selectButton;
	public GameObject 					stageButtonPrefab;
	public UIGrid 						stageGrid;
	
	public List<GFSCustomButton> 		stageButtonsList;
	public List<UISprite> 				stageSpritesList;

	public Color 						selectedColor;
	public Color						unselectedColor;
	public Color						lockedColor;

	private int 						_selectStage = 1;
	
	void Start () 
	{
	}
	void OnEnable()
	{
		returnButton.onClick += ReturnButtonClicked;
		selectButton.onClick += SelectButtonClicked;
		
		CreateStageButtons ();
		UpdateStageButtonSprites ();
		
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
		stageSpritesList.Clear ();
	}
	void Update () 
	{
	}
	void UpdateStageButtonSprites()
	{
		ChapterDescriptor __tempChapterDescriptor = ChaptersManager.GetInstance ().chaptersList.chapters[MainMenuSceneManager.selectedChapter-1 ];
		for(int i = 0; i < stageSpritesList.Count; i++)
		{
			if (!__tempChapterDescriptor.stages[i].isUnlocked)
				stageSpritesList[i].color = lockedColor;
			else if (i == _selectStage - 1)
				stageSpritesList[i].color = selectedColor;
			else
				stageSpritesList[i].color = unselectedColor;
		}
	}

	void ReturnButtonClicked (string p_name)
	{
		transform.parent.GetComponent<MainMenuSceneManager> ().ChangeToState (MainMenuSceneManager.MainMenuStates.CHAPTER_SELECT);
	}
	void StageButtonClicked(string p_name)
	{
		_selectStage = int.Parse (p_name);
		UpdateStageButtonSprites ();
	}
	void SelectButtonClicked(string p_name)
	{
		if (ChaptersManager.GetInstance ().chaptersList.chapters[MainMenuSceneManager.selectedChapter -1].stages[_selectStage -1].isUnlocked)
		{
			Debug.Log ("Select Button. Stage " + _selectStage);
			MainMenuSceneManager.selectedStage = _selectStage;
			Application.LoadLevel ("InGameScene");
		}
		else
		{
			Debug.Log("Locked");
		}
	}
	void CreateStageButtons ()
	{
		GameObject __temp;
		ChapterDescriptor __tempChapterDescriptor = ChaptersManager.GetInstance ().chaptersList.chapters[MainMenuSceneManager.selectedChapter -1];
		for(int i = 0; i < __tempChapterDescriptor.stages.Count; i ++)
		{
			//Instantiate the prefab
			__temp = (GameObject)Instantiate(stageButtonPrefab);
			__temp.transform.parent = stageGrid.transform;
			__temp.transform.localScale = Vector3.one;
			__temp.name = (1+i).ToString();

			//Set the label
			if (__tempChapterDescriptor.stages[i].isUnlocked)
				__temp.GetComponentInChildren<UILabel>().text = __tempChapterDescriptor.stages[i].stageName;
			else
				__temp.GetComponentInChildren<UILabel>().text = "Locked!";

			//Add the sprite and the button to the lists
			stageSpritesList.Add(__temp.GetComponent<UISprite>());
			stageButtonsList.Add(__temp.GetComponent<GFSCustomButton>());
			__temp.GetComponent<GFSCustomButton>().onClick += StageButtonClicked;

			//Check Completed Flag
			if (__tempChapterDescriptor.stages[i].isCompleted && __tempChapterDescriptor.stages[i].isUnlocked)
				__temp.transform.FindChild("CompletedFlag").gameObject.SetActive(true);
			else
				__temp.transform.FindChild("CompletedFlag").gameObject.SetActive(false);
		}
		stageGrid.repositionNow = true;
	}
}
