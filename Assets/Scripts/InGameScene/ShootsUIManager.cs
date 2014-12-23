using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ShootsUIManager : MonoBehaviour 
{
	//player reference
	public Player player;

	//Sprites names
	public List<string> buttonSpriteNamesList;

	//Buttons instance info
	public GameObject shootTypeButtonPrefab;
	public Transform shootTypeBaseTransform;
	public UIAnchor bottomLeftAnchor;
	public float xDistanceBetweenButtons;

	//Components lists
	public List<GameObject> 				shootTypeGOList;
	public List<UISprite> 					selectedButtonIconsList;
	public List<UILabel> 					ammoLabelsList;
	public List<UITexture>					shootTutorialFocusSpriteList;
	public List<GFSCustomButton> 			shootTypeButtonsList;
	
	//Shoots info
	public List<GlobalInfo.ShootTypes> 		shootTypesList;
	public List<int> 						shootAmmoList;
	public List<int> 						shootStartAmmoList;
	public List<bool>						shootInfiniteAmmoList;
	public List<int>						shootTutorialFocus;
	
	public int 								selectedButton = 0;
	public GlobalInfo.ShootTypes 			selectedShootType;

	public bool								onTutorialMode = false;

	void OnEnable()
	{
		player = LevelInfo.player;

		//player.shootsUIManager = this;

		//Load the ammo info from the ChapterManager
		if (Application.loadedLevelName == "LevelEditor")
		{
			StageDescriptor __tempDescriptor = ChaptersManager.GetInstance ().chaptersList.chapters [MainMenuSceneManager.selectedChapter -1].stages [MainMenuSceneManager.selectedStage -1];
			shootTypesList = __tempDescriptor.shootTypesList;
			shootAmmoList = __tempDescriptor.shootAmmoList;
			shootInfiniteAmmoList = __tempDescriptor.shootInfiniteAmmoList;
			shootTutorialFocus = __tempDescriptor.shootTutorialFocus;
			
			selectedShootType = shootTypesList [selectedButton];
		}

		//Update UI
		CreateShootTypeButtons ();
		UpdateSelectedIconSprites ();
		UpdateAmmoLabels ();
	}



	void CreateShootTypeButtons ()
	{
		foreach (GameObject go in shootTypeGOList)
			Destroy(go);

		shootTypeGOList.Clear ();
		ammoLabelsList.Clear ();
		shootTypeButtonsList.Clear ();
		selectedButtonIconsList.Clear ();
		shootTutorialFocusSpriteList.Clear ();

		GameObject __tempButton;
		for(int i = shootTypesList.Count; i >0; i--)
		{
			//Create the instance
			__tempButton = (GameObject)Instantiate(shootTypeButtonPrefab, Vector3.zero ,Quaternion.identity);
			__tempButton.transform.parent = bottomLeftAnchor.transform;
			__tempButton.transform.localScale = Vector3.one;
			__tempButton.transform.localPosition = new Vector3(shootTypeBaseTransform.localPosition.x + ((i-1) * -1 * xDistanceBetweenButtons), shootTypeBaseTransform.localPosition.y, shootTypeBaseTransform.localPosition.z);
			__tempButton.name = (shootTypesList.Count -i).ToString();

			//Change the button sprite
			__tempButton.GetComponent<UISprite>().spriteName = buttonSpriteNamesList[(int)shootTypesList[shootTypesList.Count -i]];

			//Add the components to the lists
			shootTypeGOList.Add(__tempButton);
			selectedButtonIconsList.Add((__tempButton.transform.FindChild("SelectedIconSprite")).GetComponent<UISprite>());
			ammoLabelsList.Add((__tempButton.transform.FindChild("AmmoLabel")).GetComponent<UILabel>());
			shootTutorialFocusSpriteList.Add((__tempButton.transform.FindChild("FocusTexture")).GetComponent<UITexture>());
			shootTypeButtonsList.Add(__tempButton.GetComponent<GFSCustomButton>());
			shootTypeButtonsList[shootTypesList.Count-i].onClick += ShootTypeButtonClicked;
		}
	}

	void UpdateSelectedIconSprites()
	{
		for(int i = 0; i < selectedButtonIconsList.Count; i ++)
		{
			if (i == selectedButton)
				selectedButtonIconsList[i].gameObject.SetActive(true);
			else
				selectedButtonIconsList[i].gameObject.SetActive(false);
		}
	}

	void UpdateAmmoLabels()
	{
		for (int i = 0; i < shootTypesList.Count; i++)
		{
			if (shootInfiniteAmmoList[i])
				ammoLabelsList[i].text = "-";
			else
				ammoLabelsList[i].text = shootAmmoList[i].ToString();
		}
	}

	void ShootTypeButtonClicked(string p_name)
	{
		if (onTutorialMode)
			return;

		selectedButton = int.Parse(p_name);
		selectedShootType = shootTypesList [selectedButton];
		UpdateSelectedIconSprites ();

		if (player == null)
			player = GameObject.Find ("Player").GetComponent<Player>();
		player.shootType = selectedShootType;
	}

	public void DescreaseAmmo(GlobalInfo.ShootTypes p_shootType)
	{
		foreach(GlobalInfo.ShootTypes type in shootTypesList)
		{
			if (type == p_shootType)
			{
				int __index = shootTypesList.IndexOf(type);
				if (!shootInfiniteAmmoList[__index])
				{
					shootAmmoList[__index]--;
					UpdateAmmoLabels();
				}
			}

		}
	}

	public int GetAmmo(GlobalInfo.ShootTypes p_shootType)
	{
		foreach(GlobalInfo.ShootTypes type in shootTypesList)
		{
			if (type == p_shootType)
			{
				if (shootInfiniteAmmoList[shootTypesList.IndexOf(type)])
					return 1;
				else
					return shootAmmoList[shootTypesList.IndexOf(type)];
			}
		}
		return 0;
	}

	public void RestartAmmo()
	{
		shootAmmoList.Clear ();
		shootAmmoList = new List<int> (shootStartAmmoList);
		UpdateAmmoLabels ();
	}
	public void TutorialMode(bool p_willEnterOnTutorial, int p_tutorialIndex)
	{
		onTutorialMode = p_willEnterOnTutorial;
		//Entering Tutorial Mode
		if (p_willEnterOnTutorial)
		{

			for(int i = 0; i < shootTutorialFocus.Count; i ++)
			{
				//Is on focus during tutorial
				if (p_tutorialIndex == shootTutorialFocus[i])
				{
					shootTypeButtonsList[i].GetComponent<UISprite>().color = Color.white;
					shootTutorialFocusSpriteList[i].gameObject.SetActive(true);
					
				}
				//Isn't in focus
				else
				{
					shootTypeButtonsList[i].GetComponent<UISprite>().color = Color.gray;
					shootTutorialFocusSpriteList[i].gameObject.SetActive(false);

				}
				selectedButtonIconsList[i].gameObject.SetActive(false);
			}
		}
		//Leaving Tutorial Mode
		else
		{
			for(int i = 0; i < shootTutorialFocus.Count; i ++)
			{
				shootTypeButtonsList[i].GetComponent<UISprite>().color = Color.white;
				shootTutorialFocusSpriteList[i].gameObject.SetActive(false);
			}
			UpdateSelectedIconSprites();
		}

		
	}
}








