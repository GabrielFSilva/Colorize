using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ChapterSelectMenuState : MonoBehaviour 
{
	public GFSCustomButton returnButton;
	public GFSCustomButton selectButton;
	public GameObject chapterButtonPrefab;
	public UIGrid chapterGrid;
	public int _chapterAmount = 4;

	public List<GFSCustomButton> chapterButtonsList;

	private int _selectChapter;

	void Start () 
	{
	}
	void OnEnable()
	{
		returnButton.onClick += ReturnButtonClicked;
		selectButton.onClick += SelectButtonClicked;

		CreateChapterButtons ();

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
	}
	void Update () 
	{
	}
	
	void ReturnButtonClicked (string p_name)
	{
		transform.parent.GetComponent<MainMenuSceneManager> ().ChangeToState (MainMenuSceneManager.MainMenuStates.TITLE);
	}
	void ChapterButtonClicked(string p_name)
	{
		_selectChapter = int.Parse (p_name);
	}
	void SelectButtonClicked(string p_name)
	{
		MainMenuSceneManager.selectedChapter = _selectChapter;
		transform.parent.GetComponent<MainMenuSceneManager> ().ChangeToState (MainMenuSceneManager.MainMenuStates.STAGE_SELECT);
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

			//Change the name
			__temp.name = (1+i).ToString();
			//Change the label on the button
			__temp.GetComponentInChildren<UILabel>().text = __tempChapterList[i].chapterName;
			//Set the custom button
			chapterButtonsList.Add(__temp.GetComponent<GFSCustomButton>());
			__temp.GetComponent<GFSCustomButton>().onClick += ChapterButtonClicked;

			//Completed Flag
			if (__tempChapterList[i].isCompleted)
				__temp.transform.FindChild("CompletedFlag").gameObject.SetActive(true);
			else
				__temp.transform.FindChild("CompletedFlag").gameObject.SetActive(false);
		}
		chapterGrid.repositionNow = true;
	}
}
