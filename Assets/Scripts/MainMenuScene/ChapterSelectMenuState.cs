using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ChapterSelectMenuState : MonoBehaviour 
{
	public GFSCustomButton 				returnButton;
	public GFSCustomButton 				selectButton;
	public GameObject 					chapterButtonPrefab;
	public UIGrid 						chapterGrid;

	public List<GFSCustomButton> 		chapterButtonsList;
	public List<UISprite> 				chapterSpritesList;

	public Color 						selectedColor;
	public Color						unselectedColor;
	public Color						lockedColor;

	private int _selectChapter = 1;

	void Start () 
	{
	}
	void OnEnable()
	{
		returnButton.onClick += ReturnButtonClicked;
		selectButton.onClick += SelectButtonClicked;

		CreateChapterButtons ();
		UpdateChapterButtonSprites ();

	}
	void OnDisable()
	{
		returnButton.onClick -= ReturnButtonClicked;
		selectButton.onClick -= SelectButtonClicked;

		foreach(GFSCustomButton button in chapterButtonsList)
		{
			if (button != null)
				Destroy(button.gameObject);
		}
		chapterButtonsList.Clear ();
		chapterSpritesList.Clear ();
	}
	void Update () 
	{
	}

	void UpdateChapterButtonSprites()
	{
		List<ChapterDescriptor> __tempChapterList = ChaptersManager.GetInstance ().chaptersList.chapters;
		for(int i = 0; i < chapterSpritesList.Count; i++)
		{
			if (!__tempChapterList[i].isUnlocked)
				chapterSpritesList[i].color = lockedColor;
			else if (i == _selectChapter - 1)
				chapterSpritesList[i].color = selectedColor;
			else
				chapterSpritesList[i].color = unselectedColor;
		}
	}
	
	void ReturnButtonClicked (string p_name)
	{
		transform.parent.GetComponent<MainMenuSceneManager> ().ChangeToState (MainMenuSceneManager.MainMenuStates.TITLE);
	}
	void ChapterButtonClicked(string p_name)
	{
		_selectChapter = int.Parse (p_name);
		UpdateChapterButtonSprites ();
	}
	void SelectButtonClicked(string p_name)
	{
		if (ChaptersManager.GetInstance ().chaptersList.chapters[_selectChapter -1].isUnlocked)
		{
			MainMenuSceneManager.selectedChapter = _selectChapter;
			transform.parent.GetComponent<MainMenuSceneManager> ().ChangeToState (MainMenuSceneManager.MainMenuStates.STAGE_SELECT);
		}
		else
		{
			Debug.Log("Locked");
		}
	}
	void CreateChapterButtons ()
	{
		GameObject __temp;
		List<ChapterDescriptor> __tempChapterList = ChaptersManager.GetInstance ().chaptersList.chapters;
		for(int i = 0; i < __tempChapterList.Count; i ++)
		{
			//Create the instance
			__temp = (GameObject)Instantiate(chapterButtonPrefab);
			__temp.transform.parent = chapterGrid.transform;
			__temp.transform.localScale = Vector3.one;
			__temp.name = (1+i).ToString();
			//Change the label on the button
			if (__tempChapterList[i].isUnlocked)
				__temp.GetComponentInChildren<UILabel>().text = __tempChapterList[i].chapterName;
			else
				__temp.GetComponentInChildren<UILabel>().text = "Locked!";
			//Set the custom button
			chapterButtonsList.Add(__temp.GetComponent<GFSCustomButton>());
			__temp.GetComponent<GFSCustomButton>().onClick += ChapterButtonClicked;
			//Add the sprite on the lsit
			chapterSpritesList.Add(__temp.GetComponent<UISprite>());

			//Completed Flag
			if (__tempChapterList[i].isCompleted)
				__temp.transform.FindChild("CompletedFlag").gameObject.SetActive(true);
			else
				__temp.transform.FindChild("CompletedFlag").gameObject.SetActive(false);
		}
		chapterGrid.repositionNow = true;
	}
}
