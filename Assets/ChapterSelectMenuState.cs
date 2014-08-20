using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ChapterSelectMenuState : MonoBehaviour 
{
	public GFSCustomButton returnButton;
	public GameObject chapterButtonPrefab;
	public UIGrid chapterGrid;
	public int _chapterAmount = 4;

	public List<GFSCustomButton> chapterButtonsList;

	void Start () 
	{
	}
	void OnEnable()
	{
		returnButton.onClick += returnButtonClicked;

		CreateChapterButtons ();

	}
	void OnDisable()
	{
		returnButton.onClick -= returnButtonClicked;

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
	
	void returnButtonClicked (string p_name)
	{
		transform.parent.GetComponent<MainMenuSceneManager> ().ChangeToState (MainMenuSceneManager.MainMenuStates.TITLE);
	}
	void chapterButtonClicked(string p_name)
	{
		Debug.Log (p_name);
	}
	void CreateChapterButtons ()
	{
		GameObject __temp;
		for(int i = 0; i < _chapterAmount; i ++)
		{
			__temp = (GameObject)Instantiate(chapterButtonPrefab);
			__temp.transform.parent = chapterGrid.transform;
			__temp.transform.localScale = Vector3.one;
			__temp.name = "Chapter " + (1+i).ToString();
			__temp.GetComponentInChildren<UILabel>().text = "Chapter " + (1+i).ToString();
			chapterButtonsList.Add(__temp.GetComponent<GFSCustomButton>());
			__temp.GetComponent<GFSCustomButton>().onClick += chapterButtonClicked;
		}
		chapterGrid.repositionNow = true;
	}
}
