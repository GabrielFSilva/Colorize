using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ShootsManager : MonoBehaviour 
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
	public List<GFSCustomButton> 			shootTypeButtonsList;
	
	//Shoots info
	public List<GlobalInfo.ShootTypes> 		shootTypesList;
	public List<int> 						shootAmmoList;
	public List<bool>						shootInfiniteAmmoList;
	
	public int 								selectedButton = 0;
	public GlobalInfo.ShootTypes 			selectedShootType;

	void OnEnable()
	{
		//Load the ammo info from the ChapterManager
		StageDescriptor __tempDescriptor = ChaptersManager.GetInstance ().chaptersList.chapters [MainMenuSceneManager.selectedChapter -1].stages [MainMenuSceneManager.selectedStage -1];
		shootTypesList = __tempDescriptor.shootTypesList;
		shootAmmoList = __tempDescriptor.shootAmmoList;
		shootInfiniteAmmoList = __tempDescriptor.shootInfiniteAmmoList;

		selectedShootType = shootTypesList [selectedButton];
		//Update UI
		CreateShootTypeButtons ();
		UpdateSelectedIconSprites ();
		UpdateAmmoLabels ();
	}

	void CreateShootTypeButtons ()
	{
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
		selectedButton = int.Parse(p_name);
		selectedShootType = shootTypesList [selectedButton];
		UpdateSelectedIconSprites ();

		if (player == null)
			player = GameObject.Find ("Player").GetComponent<Player>();
		player.shootType = selectedShootType;
	}
}








